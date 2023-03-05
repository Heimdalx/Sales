using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/states")]
    public class StatesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public StatesController(DataContext context)
        {
            _dataContext= context;
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(State state)
        {
            try
            {
                _dataContext.Add(state);
                await _dataContext.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest($"Ya existe un estado/departamento con el nombre: {state.Name}");
                }
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _dataContext.States
                .Include(state => state.Cities!)
                .ToListAsync());
        }

    
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var state = await _dataContext.States
                .Include(state => state.Cities!)
                .FirstOrDefaultAsync(state => state.Id == id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(State state)
        {
            try
            {
                _dataContext.Update(state);
                await _dataContext.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest($"Ya existe un estado/departamento con el nombre: {state.Name}");
                }
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var state = await _dataContext.States.FirstOrDefaultAsync(state => state.Id == id);
            if (state == null)
            {
                return NotFound();
            }
            _dataContext.Remove(state);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    }


}
