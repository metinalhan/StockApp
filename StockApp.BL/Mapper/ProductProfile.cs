using AutoMapper;
using StockApp.BL.Dtos;
using StockApp.Entities.Models;

namespace StockApp.BL.Mapper
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            // ProductDTO -> Product
            CreateMap<ProductExcelDto, Product>();
        }
    }
}
