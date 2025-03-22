using Framework.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TemplateService.WebAPI.ExternalResources
{
    public class ReadersAPIs
    {
        private readonly IConfiguration Configuration;

        public ReadersAPIs(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<IList<T>> Post<T, S>(string pathUri, S body, IHeaderDictionary headers)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress =
                        new Uri(Configuration["TemplateReaderService-API"]);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    foreach (var header in headers)
                    {
                        try
                        {
                            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    HttpResponseMessage response = httpClient.PostAsJsonAsyncTemplate(pathUri, body).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        List<T> resultContent = JsonConvert.DeserializeObject<GenericResult<List<T>>>(result).Collection;
                        return resultContent;
                    }
                    else
                    {
                        return new List<T>();
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public T Get<T>(string pathUri, string langCode = "ar")
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress =
                        new Uri(Configuration["TemplateReaderService-API"]);

                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    //httpClient.DefaultRequestHeaders.Add("user-id", "1");
                    //httpClient.DefaultRequestHeaders.Add("language-code", langCode);

                    HttpResponseMessage response = httpClient.GetAsync(pathUri).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        var result = response.Content.ReadAsStringAsync().Result;
                        T resultContent = JsonConvert.DeserializeObject<T>(result);

                        return resultContent;
                    }
                    else
                    {
                        return default(T) ;
                    }
                }

            }
            catch (Exception ex)
            {
                return default(T);
            }


        }

        public DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            if (list == null)
                return dataTable;

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
