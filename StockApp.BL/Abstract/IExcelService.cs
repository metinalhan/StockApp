using StockApp.BL.Dtos;

namespace StockApp.BL.Abstract
{
    public interface IExcelService
    {
        Task<List<ProductExcelDto>> ReadExcelAsync(string filePath); 
    }
}
