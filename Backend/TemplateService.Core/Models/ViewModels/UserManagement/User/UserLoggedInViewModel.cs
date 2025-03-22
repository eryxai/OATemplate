using System;

namespace TemplateService.Core.Models.ViewModels
{
    public class UserLoggedInViewModel : Base.BaseViewModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public long Id { get; set; }
        public string UserName { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public System.String ProfileImage { get; set; }


        public int? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationNameAr { get; set; }
        public string OrganizationNameEn { get; set; }
        public string OrganizationImage { get; set; }
        public string RoleName { get; set; }


        public DateTime? issued { get; set; }
        public DateTime? expires { get; set; }


        public bool IsFirstTimeLogin { get; set; }
        public bool IsSuperAdmin { get; set; }

        public Nullable<Guid> ReaderId { get; set; }
    }
}
