using BusinessObject;
using LAB01_ProductManagementAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.Implement;
using Repositories.Interface;
using System.Xml.Linq;

namespace LAB01_ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [EnableQuery(PageSize = 10)]
        [HttpGet("getall")]
        public ActionResult GetAllProducts() {
            try
            {
                var list = _productRepository.GetAll().AsQueryable();
                if (!list.Any()) {
                    return NotFound("No Data");
                }
                return Ok(list);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get/{id}")]
        public ActionResult GetProduct([FromRoute] int id)
        {
            try
            {
                var product = _productRepository.GetById(id);
                if (product == null)
                {
                    return NotFound("Product Not Found");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("create")]
        public ActionResult CreateProduct(ProductRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Category category = _categoryRepository.GetById(request.CategoryId)!;
                if (category == null)
                {
                    return BadRequest("Category Not Exist");
                }

                var existProduct = _productRepository.GetAll().
                    Where(prod => prod.ProductName!.Equals(request.ProductName, StringComparison.OrdinalIgnoreCase));
                if (existProduct.Any())
                {
                    return BadRequest("Name Existed");
                }
                var nextId = _productRepository.GetAll().Any()
                            ? _productRepository.GetAll().Max(cate => cate.CategoryId) + 1
                            : 1;

                Product newProduct = new Product
                {
                    ProductId = nextId,
                    ProductName = request.ProductName,
                    UnitPrice = request.UnitPrice,
                    UnitsInStock = request.UnitsInStock,
                    CategoryId = request.CategoryId,
                };
                _productRepository.Add(newProduct);
                _productRepository.Save();

                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public ActionResult UpdateProductById([FromRoute] int id, ProductRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var foundProduct = _productRepository.GetById(id);
                if (foundProduct == null)
                {
                    return BadRequest("Product Not Found");
                }

                if (!foundProduct.ProductName!.Equals(request.ProductName, StringComparison.OrdinalIgnoreCase))
                {
                    var existProduct = _productRepository.GetAll()
                        .Where(cate => foundProduct.ProductName!.Equals(request.ProductName, StringComparison.OrdinalIgnoreCase)).Any();
                    if (existProduct)
                    {
                        return BadRequest("Name existed");
                    }
                }

                foundProduct.ProductName = request.ProductName;
                foundProduct.CategoryId = request.CategoryId;
                foundProduct.UnitPrice = request.UnitPrice;
                foundProduct.UnitsInStock = request.UnitsInStock;
                _productRepository.Update(foundProduct);
                _productRepository.Save();

                return Ok(foundProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteProductById([FromRoute] int id)
        {
            try
            {
                var foundProduct = _categoryRepository.GetById(id);
                if (foundProduct == null)
                {
                    return BadRequest("Product Not Found");
                }

                _categoryRepository.Delete(foundProduct);
                _categoryRepository.Save();
                return Ok(foundProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
