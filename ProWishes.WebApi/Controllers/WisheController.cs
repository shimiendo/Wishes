using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProWishes.Domain;
using ProWishes.Repository;

namespace ProWishes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WisheController : ControllerBase
    {
        private readonly IRepositoryWishe _repo;

        public WisheController(IRepositoryWishe repo)
        {
            _repo = repo;            
        }  

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllWisheAsync(true, true);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }            
        }

        [HttpGet("{WisheId}")]
        public async Task<IActionResult> Get(int wisheId)
        {
            try
            {
                var results = await _repo.GetWisheAsyncByIdAsync(wisheId, true, true);
                
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
                var results = await _repo.GetAllWisheAsyncByNameAsync(name, true, true);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }            
        }    

        [HttpPost]
        public async Task<IActionResult> Post(Wishe model)
        {            
            try
            {
                _repo.Add(model);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/wishe/{model.Id}", model);
                }                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }   

            return BadRequest();         
        }  

        [HttpPut("{WisheId}")]
        public async Task<IActionResult> Put(int wisheId, Wishe model)
        {
            try
            {
                var wishe = await _repo.GetWisheAsyncByIdAsync(wisheId, false, false);
                if(wishe == null) return NotFound();

                _repo.Update(model);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/wishe/{model.Id}", model);
                }                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }   

            return BadRequest();         
        }                       
          
        [HttpDelete("{WisheId}")]
        public async Task<IActionResult> Delete(int wishetId)
        {
            try
            {
                var wishe = await _repo.GetWisheAsyncByIdAsync(wishetId, false, false);
                if(wishe == null) return NotFound();

                _repo.Delete(wishe);
                
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