using MongoWeb.Models;

namespace MongoWeb.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllProductAsync<T>();
        Task<T> GetAllProductByIdAsync<T>(int id);
        Task<T> CreateProductAsync<T>(ProductDto entity);
        Task<T> UpdateProductAsync<T>(ProductDto entity);
        Task<T> DeleteProductAsync<T>(int id);
    }
}