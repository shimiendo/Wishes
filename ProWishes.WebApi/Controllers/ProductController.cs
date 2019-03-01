using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProWishes.Domain;
using ProWishes.Repository;

namespace ProWishes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryProduct _repo;

        public ProductController(IRepositoryProduct repo)
        {
            _repo = repo;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllProductAsync(true);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }            
        }

        [HttpGet("{ProductId}")]
        public async Task<IActionResult> Get(int productId)
        {
            try
            {
                var results = await _repo.GetProductAsyncByIdAsync(productId, true);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }            
        }     

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var results = await _repo.GetAllProductAsyncByNameAsync(name, true);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product model)
        {            
            try
            {
                _repo.Add(model);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/product/{model.Id}", model);
                }                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }   

            return BadRequest();         
        }     

        [HttpPut("{ProductId}")]
        public async Task<IActionResult> Put(int productId, Product model)
        {
            try
            {
                var product = await _repo.GetProductAsyncByIdAsync(productId, false);
                if(product == null) return NotFound();

                _repo.Update(model);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/product/{model.Id}", model);
                }                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }   

            return BadRequest();         
        }                       

        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                var product = await _repo.GetProductAsyncByIdAsync(productId, false);
                if(product == null) return NotFound();

                _repo.Delete(product);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
                }                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }   

            return BadRequest();         
        } 
    }
}