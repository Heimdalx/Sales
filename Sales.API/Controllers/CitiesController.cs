using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CitiesController(DataContext context)
        {
            _dataContext = context;
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(City cities)
        {
            try
            {
                _dataContext.Add(cities);
                await _dataContext.SaveChangesAsync();
                return Ok(cities);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest($"Ya existe una ciudad con el nombre: {cities.Name}");
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
            return Ok(await _dataContext.Cities.ToListAsync());
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var city = await _dataContext.Cities.FirstOrDefaultAsync(city => city.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(City city)
        {
            try
            {
                _dataContext.Update(city);
                await _dataContext.SaveChangesAsync();
                return Ok(city);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest($"Ya existe una ciudad con el nombre: {city.Name}");
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
            var city = await _dataContext.Cities.FirstOrDefaultAsync(city => city.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            _dataContext.Remove(city);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    }
}

