using Framework.Core.Models;
using System.Diagnostics;

namespace TemplateService.Core.Models.ViewModels
{
    [DebuggerDisplay("Pagination={Pagination}, Sorting={Sorting}, Name={Name},IsActive={IsActive}")]
  public class RoleLookupSearchModel : BaseFilter
  {
    #region Constructors

    /// <summary>
    /// Initializes a new instance from type
    /// RoleSearchModel
    /// </summary>
    public RoleLookupSearchModel()
    {
    }

    #endregion Constructors


        public string Name { get; set; }
        public bool Paginated { get; set; }

    }
}