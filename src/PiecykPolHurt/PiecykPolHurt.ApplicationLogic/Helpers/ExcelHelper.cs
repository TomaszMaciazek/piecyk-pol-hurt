using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace PiecykPolHurt.ApplicationLogic.Helpers
{
    public static class ExcelHelper
    {
        public static byte[] GetGeneratorExportData(this List<string[]> dataToExport, List<string> columns)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Projects");
            package.Workbook.Properties.Created = DateTime.Now;
            ws.Cells.Style.Font.Size = 11;
            ws.Cells.Style.Font.Name = "Calibri";

            for (int i = 0; i < columns.Count; i++)
            {
                ws.Cells[1, i + 1].Value = columns[i];
            }
            for (int i = 0; i < dataToExport.Count; i++)
            {
                for (int j = 0; j < dataToExport[i].Length; j++)
                {
                    ws.Cells[i + 2, j + 1].Value = dataToExport[i][j];
                }
            }

            var header = ws.Cells[1, 1, 1, ws.Dimension.End.Column];
            header.Style.Font.Bold = true;
            header.Style.Fill.PatternType = ExcelFillStyle.Solid;
            header.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#BFBFBF"));

            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }

    }
}
