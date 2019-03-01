using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProWishes.Domain;
using ProWishes.Repository;

namespace ProWishes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryUser _repo;

        public UserController(IRepositoryUser repo)
        {
            _repo = repo;            
        }        
       
       [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllUserAsync(true);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }            
        }

       [HttpGet("pages/{page}")]
        public async Task<IActionResult> Get(int? page)
        {
            try
            {
                var results = await _repo.GetAllUserAsyncPagesAsync(true, page);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }            
        }        

        [HttpGet("{UserId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                var results = await _repo.GetUserAsyncById(userId, true);
                
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
                var results = await _repo.GetAllUserAsyncByName(name, true);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }            
        }  

        [HttpPost]
        public async Task<IActionResult> Post(User model)
        {            
            try
            {
                _repo.Add(model);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/user/{model.Id}", model);
                }                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }   

            return BadRequest();         
        }  

        [HttpPut("{UserId}")]
        public async Task<IActionResult> Put(int userId, User model)
        {
            try
            {
                var user = await _repo.GetUserAsyncById(userId, false);
                if(user == null) return NotFound();

                _repo.Update(model);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/user/{model.Id}", model);
                }                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }   

            return BadRequest();         
        }                       

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                var user = await _repo.GetUserAsyncById(userId, false);
                if(user == null) return NotFound();

                _repo.Delete(user);
                
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