using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Vml.Office;
using Framework.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemplateService.Common.CustomAttributes;
using TemplateService.Entity.Entities;

namespace TemplateService.DataAccess.Helper
{
    public class AuditSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Temporary list to hold audit info between SavingChanges and SavedChanges
        private readonly List<TempAuditEntry> _tempAuditEntries = new();

        public AuditSaveChangesInterceptor(
            ICurrentUserService currentUserService,
            IHttpContextAccessor httpContextAccessor)
        {
            _currentUserService = currentUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region SavingChanges / SavingChangesAsync
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var context = eventData.Context;
            if (context == null)
                return base.SavingChanges(eventData, result);

            // Clear any stale entries from previous SaveChanges
            _tempAuditEntries.Clear();

            // Capture all relevant changes (Added, Modified, Deleted)
            CaptureAuditEntries(context);

            // Do NOT insert AuditLogs here anymore—IDs aren’t final yet.
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
                return await base.SavingChangesAsync(eventData, result, cancellationToken);

            _tempAuditEntries.Clear();
            CaptureAuditEntries(context);

            // Do NOT insert AuditLogs here—IDs aren’t final yet.
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        #endregion

        #region SavedChanges / SavedChangesAsync
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            var context = eventData.Context;
            if (context == null)
                return base.SavedChanges(eventData, result);

            // Now the database-generated IDs are set on the entities
            if (_tempAuditEntries.Count > 0)
            {
                var finalAuditLogs = BuildFinalAuditLogs(_tempAuditEntries);
                if (finalAuditLogs.Count > 0)
                {
                    context.Set<UserAuditLog>().AddRange(finalAuditLogs);
                    // Optionally do another SaveChanges or rely on the next SaveChanges call
                    context.SaveChanges();
                }
            }

            return base.SavedChanges(eventData, result);
        }

        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
                return await base.SavedChangesAsync(eventData, result, cancellationToken);

            if (_tempAuditEntries.Count > 0)
            {
                var finalAuditLogs = BuildFinalAuditLogs(_tempAuditEntries);
                if (finalAuditLogs.Count > 0)
                {
                    await context.Set<UserAuditLog>()
                                 .AddRangeAsync(finalAuditLogs, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        #endregion

        #region Capture & Build Logic
        /// <summary>
        /// Collect changes but don't insert any AuditLogs yet.
        /// </summary>
        private void CaptureAuditEntries(DbContext context)
        {
            var userId = _currentUserService.CurrentUserId;
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var endPoint = _httpContextAccessor.HttpContext?.Request.Path;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                // Skip logging UserAuditLog itself, or entries without [AuditLogAttribute],
                // or entities that are not changed.
                if (entry.Entity is UserAuditLog ||
                    !entry.Entity.GetType().GetCustomAttributes(true).Any(e => e is AuditLogAttribute) ||
                    entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                {
                    continue;
                }
                var attr = entry.Entity.GetType().GetCustomAttributes(true).FirstOrDefault(e => e is AuditLogAttribute) as AuditLogAttribute;
                var action = !string.IsNullOrEmpty(attr?.LogAction);
                var checkProperties = attr?.CheckProperties ?? false;
                List<string> jsonProperties = new List<string>();
                if (checkProperties)
                {
                    var properties = entry.Entity.GetType().GetProperties().Where(e=>e.GetCustomAttributes(true).Any(x=>x  is AuditLogPropertyAttribute)&& ((entry.Property(e.Name).IsModified
                    && (e.GetCustomAttributes(true).FirstOrDefault(e => e is AuditLogPropertyAttribute) as AuditLogPropertyAttribute).LogProperty == true)||(entry.State==EntityState.Added&& (e.GetCustomAttributes(true).FirstOrDefault(e => e is AuditLogPropertyAttribute) as AuditLogPropertyAttribute).LogProperty == true))
                    );
                    if(properties.Count()==0) continue;
                    if(!attr.LogAllProperties)
                    foreach (var prop in properties)
                    {
                            jsonProperties.Add(prop.Name);
                        
                     
                    }
                }

                var tableName = entry.Metadata.GetTableName();
                var temp = new TempAuditEntry
                {
                    EntityEntry = entry,
                    TableName = tableName,
                    UserId = userId,
                    IpAddress = ipAddress,
                    EndPoint = endPoint,
                    AuditDate = DateTime.UtcNow,
                    EntityState = entry.State
                };

                // Capture old/new values
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (checkProperties)
                        {
                            var dict = new Dictionary<string, object>();
                            foreach (var prop in entry.Properties)
                            {
                                if (jsonProperties.Contains(prop.Metadata.Name))
                                {
                                    dict[prop.Metadata.Name] = prop.CurrentValue;
                                }
                            }
                            temp.NewValues = JsonConvert.SerializeObject(dict);
                        }
                        else
                        temp.NewValues = SerializeValues(entry.CurrentValues);
                        break;

                    case EntityState.Deleted:
                        if (checkProperties)
                        {
                            var dict = new Dictionary<string, object>();
                            foreach (var prop in entry.Properties)
                            {
                                if (jsonProperties.Contains(prop.Metadata.Name))
                                {
                                    dict[prop.Metadata.Name] = prop.OriginalValue;
                                }
                            }
                            temp.OldValues = JsonConvert.SerializeObject(dict);
                        }
                        else
                        temp.OldValues = SerializeValues(entry.OriginalValues);
                        break;

                    case EntityState.Modified:
                        if (checkProperties)
                        {
                            var oldDict = new Dictionary<string, object>();
                            var newDict = new Dictionary<string, object>();
                            foreach (var prop in entry.Properties)
                            {
                                if (jsonProperties.Contains(prop.Metadata.Name)&&(prop.IsModified||attr.LogAllProperties))
                                {
                                    oldDict[prop.Metadata.Name] = prop.OriginalValue;
                                    newDict[prop.Metadata.Name] = prop.CurrentValue;
                                }
                            }
                            temp.OldValues = JsonConvert.SerializeObject(oldDict);
                            temp.NewValues = JsonConvert.SerializeObject(newDict);
                        }
                        else
                        {
                            var oldDict = new Dictionary<string, object>();
                            var newDict = new Dictionary<string, object>();
                            foreach (var prop in entry.Properties)
                            {
                                if (prop.IsModified || attr.LogAllProperties)
                                {
                                    oldDict[prop.Metadata.Name] = prop.OriginalValue;
                                    newDict[prop.Metadata.Name] = prop.CurrentValue;
                                }
                            }
                            temp.OldValues = JsonConvert.SerializeObject(oldDict);
                            temp.NewValues = JsonConvert.SerializeObject(newDict);
                            //temp.OldValues = SerializeValues(entry.OriginalValues);
                            //temp.NewValues = SerializeValues(entry.CurrentValues);
                        }
                        //var oldDict = new Dictionary<string, object>();
                        //var newDict = new Dictionary<string, object>();
                        //foreach (var prop in entry.Properties)
                        //{
                        //    if (prop.IsModified)
                        //    {
                        //        oldDict[prop.Metadata.Name] = prop.OriginalValue;
                        //        newDict[prop.Metadata.Name] = prop.CurrentValue;
                        //    }
                        //}
                        //temp.OldValues = JsonConvert.SerializeObject(oldDict);
                        //temp.NewValues = JsonConvert.SerializeObject(newDict);
                        break;
                }

                _tempAuditEntries.Add(temp);
            }
        }

        /// <summary>
        /// Build final AuditLogs once the real DB IDs are known.
        /// </summary>
        private List<UserAuditLog> BuildFinalAuditLogs(IEnumerable<TempAuditEntry> tempEntries)
        {
            var logs = new List<UserAuditLog>();

            foreach (var temp in tempEntries)
            {
                var attr = temp.EntityEntry.Entity.GetType().GetCustomAttributes(true).FirstOrDefault(e => e is AuditLogAttribute) as AuditLogAttribute;
                var isAction = !string.IsNullOrEmpty(attr?.LogAction);
                // The final primary key value is available now that SaveChanges has completed
                var keyValueJson = GetPrimaryKey(temp.EntityEntry);
                
                var action = temp.EntityState switch
                {
                    EntityState.Added => "INSERT",
                    EntityState.Deleted => "DELETE",
                    EntityState.Modified => "UPDATE",
                    _ => null
                };
                if (isAction)
                {
                    action = temp.EntityEntry.Entity.GetType().GetMethod(attr.LogAction).Invoke(null, new object[] { temp.EntityState, temp.OldValues, temp.NewValues }) as string;
                }
                
                if (action == null) continue;

                logs.Add(new UserAuditLog
                {
                    TableName = temp.TableName,
                    Action = action,
                    KeyValues = keyValueJson,
                    OldValues = temp.OldValues,
                    NewValues = temp.NewValues,
                    UserId = temp.UserId,
                    IpAddress = temp.IpAddress,
                    CreationDate = temp.AuditDate,
                    EndPoint = temp.EndPoint
                });
            }

            return logs;
        }
        #endregion

        #region Helper Methods
        private string SerializeValues(PropertyValues values)
        {
            var dict = new Dictionary<string, object>();
            foreach (var prop in values.Properties)
            {
                dict[prop.Name] = values[prop];
            }
            return JsonConvert.SerializeObject(dict);
        }

        private string GetPrimaryKey(EntityEntry entry)
        {
            var key = entry.Metadata.FindPrimaryKey();
            if (key == null) return null;

            var dict = new Dictionary<string, object>();
            foreach (var prop in key.Properties)
            {
                dict[prop.Name] = entry.Property(prop.Name).CurrentValue;
            }
            return JsonConvert.SerializeObject(dict);
        }
        #endregion

        #region TempAuditEntry Class
        /// <summary>
        /// Temporary container for audit info between SavingChanges and SavedChanges.
        /// </summary>
        private class TempAuditEntry
        {
            public EntityEntry EntityEntry { get; set; }
            public string TableName { get; set; }
            public long? UserId { get; set; }
            public string IpAddress { get; set; }
            public string EndPoint { get; set; }
            public DateTime AuditDate { get; set; }
            public EntityState EntityState { get; set; }
            public string OldValues { get; set; }
            public string NewValues { get; set; }
        }
        #endregion
    }
}
