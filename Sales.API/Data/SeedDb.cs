using Microsoft.EntityFrameworkCore;
using Sales.API.Services;
using Sales.Shared.Entities;
using Sales.Shared.Responses;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;

        public SeedDb(DataContext context, IApiService apiService)
        {
            _context= context;
            _apiService = apiService;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
           // await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
           
            if (!_context.Countries.Any())
            {
                Response responseCountries = await _apiService.GetListAsync<CountryResponse>("/v1", "/countries");
                if (responseCountries.IsSuccess)
                {
                    List<CountryResponse> countries = (List<CountryResponse>)responseCountries.Result!;
                    foreach (CountryResponse countryResponse in countries)
                    {
                        Country country = await _context.Countries!.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            Response responseStates = await _apiService.GetListAsync<StateResponse>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.IsSuccess)
                            {
                                List<StateResponse> states = (List<StateResponse>)responseStates.Result!;
                                foreach (StateResponse stateResponse in states!)
                                {
                                    State state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        Response responseCities = await _apiService.GetListAsync<CityResponse>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.IsSuccess)
                                        {
                                            List<CityResponse> cities = (List<CityResponse>)responseCities.Result!;
                                            foreach (CityResponse cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                City city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }

           

        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Baterias" });
                _context.Categories.Add(new Category { Name = "Violines" });
                _context.Categories.Add(new Category { Name = "Guitarras" });
                _context.Categories.Add(new Category { Name = "Cellos" });
                _context.Categories.Add(new Category { Name = "Contrabajos" });
                _context.Categories.Add(new Category { Name = "Violas" });
                _context.Categories.Add(new Category { Name = "Bajos" });
                _context.Categories.Add(new Category { Name = "Clarinetes" });
                _context.Categories.Add(new Category { Name = "Flautas" });
                _context.Categories.Add(new Category { Name = "Pianos" });
                _context.Categories.Add(new Category { Name = "Trompetas" });
                _context.Categories.Add(new Category { Name = "Saxofones" });
                _context.Categories.Add(new Category { Name = "Trombones" });
                _context.Categories.Add(new Category { Name = "Cornos" });
                _context.Categories.Add(new Category { Name = "Fagots" });
                _context.Categories.Add(new Category { Name = "Oboes" });
                _context.Categories.Add(new Category { Name = "Piccolos" });
                _context.Categories.Add(new Category { Name = "Panderetas" });
                _context.Categories.Add(new Category { Name = "Timbales" });
                _context.Categories.Add(new Category { Name = "Maracas" });
                _context.Categories.Add(new Category { Name = "Congas" });
                await _context.SaveChangesAsync();
            }

        }



    }
}

