using Microsoft.AspNetCore.Mvc;
using MehrServicesAPI.Models;
using MehrServicesAPI.Models.Dtos;
using MehrServicesAPI.Repository;

namespace MehrServicesAPI.Controllers
{
    [Route("api/products")]
    public class ProductApiController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository productRepository;

        public ProductApiController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await productRepository.GetProducts();
                _response.Result = productDtos;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };

            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                var result = await productRepository.GetProductById(id);
                _response.Result = result;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };

            }

            return Ok(_response);
        }

        [HttpPost]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                var result = await productRepository.CreateUpdateProduct(productDto);
                _response.Result = result;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };

            }

            return Ok(_response);
        }

        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                var result = await productRepository.CreateUpdateProduct(productDto);
                _response.Result = result;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };

            }

            return Ok(_response);
        }

        [HttpDelete]
        public async Task<object> Delete(int id)
        {
            try
            {
                var result = await productRepository.DeleteProduct(id);
                _response.Result = result;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };

            }

            return Ok(_response);
        }
    }
}
