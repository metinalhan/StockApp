using OfficeOpenXml;
using StockApp.BL.Abstract;
using StockApp.BL.Dtos;
using System.IO;

namespace StockApp.BL.Services
{
    public class ExcelService : IExcelService
    {
        public ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        public async Task<List<ProductExcelDto>> ReadExcelAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                var rows = new List<ProductExcelDto>();

                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                   
                    for (int row = 2; row <= rowCount; row++)
                    {
                        string name = worksheet.Cells[row, 1].Text; 
                        string amount = worksheet.Cells[row, 2].Text; 
                        string barcode = worksheet.Cells[row, 3].Text; 
                        string price = worksheet.Cells[row, 4].Text; 

                        rows.Add(new ProductExcelDto
                        {
                            Name = name,
                            Stock = Convert.ToInt32(amount),
                            Barcode = barcode,
                            Price = Convert.ToDecimal(price)
                        });
                    }
                }

                return rows;
            });
        }
    }
}
