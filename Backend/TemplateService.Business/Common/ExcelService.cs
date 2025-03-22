#region Using ...
using TemplateService.Common.CustomAttributes;
using ClosedXML.Excel;
using Framework.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#endregion

namespace TemplateService.Business.Common
{
    public class ExcelService : IExcelService
    {

        public IXLWorksheet SetStyle(IXLWorksheet worksheet, int row, int cellsCount)
        {
            if (worksheet != null)
            {
                for (int i = 1; i < cellsCount + 1; i++)
                {
                    worksheet.Cell(row, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Cell(row, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(row, i).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    worksheet.Cell(row, i).Style.Fill.PatternColor = XLColor.White;
                }
            }
            return worksheet;
        }
        public void SetTableStyle(IXLTable table)
        {
            table.HeadersRow().Style.Fill.BackgroundColor = XLColor.FromHtml("#DCE6F1");
            table.Style.Font.FontColor = XLColor.FromHtml("#304C7D");
            table.HeadersRow().Style.Font.Bold = true;
            table.HeadersRow().Style.Font.Underline = XLFontUnderlineValues.Single;
            table.HeadersRow().Style.Alignment.WrapText = true;
        }
            public IXLWorksheet MapToExcel<T>(List<T> models, string SheetName, XLWorkbook workbook) where T : new()
        {
            if(!workbook.Worksheets.Contains(SheetName))
            {
                workbook.Worksheets.Add(SheetName);
            }
            var worksheet = workbook.Worksheet(SheetName);

            var properties = typeof(T).GetProperties().Where(e => e.CustomAttributes.Any(x => x.AttributeType == typeof(ExcelCustomAttribute))).Order(new ExcelSorter()).ToList();

            // Add headers
            for (int i = 0; i < properties.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = (string)properties[i].CustomAttributes.Where(e => e.AttributeType == typeof(ExcelCustomAttribute)).First().ConstructorArguments.First().Value;
            }

            // Add data rows
            for (int rowIndex = 0; rowIndex < models.Count; rowIndex++)
            {
                var model = models[rowIndex];
                for (int colIndex = 0; colIndex < properties.Count; colIndex++)
                {
                    var value = properties[colIndex].GetValue(model);
                    worksheet.Cell(rowIndex + 2, colIndex + 1).Value = value?.ToString();
                }
            }

            return worksheet;


        }

        public List<T> MapFromExcel<T>(IXLWorksheet worksheet) where T : new()
        {
            var models = new List<T>();


            var properties = typeof(T).GetProperties().Where(e => e.CustomAttributes.Any(x => x.AttributeType == typeof(ExcelCustomAttribute)));
            var columnMapping = new Dictionary<int, PropertyInfo>();

            // Map columns to properties
            var headerRow = worksheet.Row(1);
            for (int col = 1; col <= headerRow.LastCellUsed().Address.ColumnNumber; col++)
            {
                var header = headerRow.Cell(col).GetValue<string>().Trim();
                PropertyInfo? property = properties.FirstOrDefault(p => ((string)p.CustomAttributes.First().ConstructorArguments.First().Value).Equals(header, StringComparison.OrdinalIgnoreCase));
                if (property != null)
                {
                    columnMapping[col] = property;
                }
            }

            // Read data rows
            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                var model = new T();
                var flag = false;
                foreach (var colMap in columnMapping)
                {
                    var cellValue = row.Cell(colMap.Key).GetValue<string>();
                    if (string.IsNullOrEmpty(cellValue))
                    {
                        flag = true;
                        break;
                    }

                    var property = colMap.Value;
                    object convertedValue = Convert.ChangeType(cellValue, property.PropertyType);
                    property.SetValue(model, convertedValue);
                }
                if (flag)
                {
                    continue;
                }
                models.Add(model);
            }


            return models;
        }
    }
    public class ExcelSorter : IComparer<PropertyInfo>
    {
        public int Compare(PropertyInfo x, PropertyInfo y)
        {
            var xOrder = (int)x.CustomAttributes.First().ConstructorArguments.Last().Value;
            var yOrder = (int)y.CustomAttributes.First().ConstructorArguments.Last().Value;
            return xOrder.CompareTo(yOrder);
        }
    }
}
