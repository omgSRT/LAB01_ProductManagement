using BusinessObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.Interface;
using System.Xml.Linq;

namespace LAB01_ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [EnableQuery(PageSize = 10)]
        [HttpGet("getall")]
        public ActionResult GetAllCategories()
        {
            try
            {
                var list = _categoryRepository.GetAll().AsQueryable();
                if(!list.Any())
                {
                    return NotFound("No Data");
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public ActionResult CreateCategory(string name)
        {
            try
            {
                if(name == null)
                {
                    return BadRequest("Name cannot be null");
                }

                var existCategory = _categoryRepository.GetAll()
                    .Where(cate => cate.CategoryName!.Equals(name, StringComparison.OrdinalIgnoreCase));
                if(existCategory.Any())
                {
                    return BadRequest("Name existed");
                }

                var nextId = _categoryRepository.GetAll().Any()
                            ? _categoryRepository.GetAll().Max(cate => cate.CategoryId) + 1
                            : 1;
                Category category = new Category
                {
                    CategoryName = name
                };
                _categoryRepository.Add(category);
                _categoryRepository.Save();
                
                return Ok(category);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public ActionResult UpdateCategoryById([FromRoute] int id, string name)
        {
            try
            {
                if (name == null)
                {
                    return BadRequest("Name cannot be null");
                }

                var foundCategory = _categoryRepository.GetById(id);
                if (foundCategory == null)
                {
                    return BadRequest("Category Not Found");
                }

                if(!foundCategory.CategoryName!.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    var existCategory = _categoryRepository.GetAll()
                    .Where(cate => cate.CategoryName!.Equals(name, StringComparison.OrdinalIgnoreCase)).Any();
                    if (existCategory)
                    {
                        return BadRequest("Name existed");
                    }
                }

                foundCategory.CategoryName = name;
                _categoryRepository.Update(foundCategory);
                _categoryRepository.Save();
                return Ok(foundCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteCategoryById([FromRoute] int id)
        {
            try
            { 
                var foundCategory = _categoryRepository.GetById(id);
                if (foundCategory == null)
                {
                    return BadRequest("Category Not Found");
                }

                _categoryRepository.Delete(foundCategory);
                _categoryRepository.Save();
                return Ok(foundCategory);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
