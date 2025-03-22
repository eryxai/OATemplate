#region Using ...
using ClosedXML.Excel;
using System.Collections.Generic;
#endregion

namespace Framework.Core.Common
{
    public interface IExcelService
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="row"></param>
		/// <param name="cellsCount"></param>
		/// <returns></returns>		
		IXLWorksheet SetStyle(IXLWorksheet worksheet, int row, int cellsCount);
        IXLWorksheet MapToExcel<T>(List<T> models, string filePath, XLWorkbook workbook) where T : new();
        List<T> MapFromExcel<T>(IXLWorksheet worksheet) where T : new();
		void SetTableStyle(IXLTable table);
    }
}
