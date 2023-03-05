using Sales.Shared.Entities;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        public SeedDb(DataContext context)
        {
            _context= context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if(!_context.Countries.Any())
            {
                _context.Countries.Add(new Country 
                { Name = "Colombia" ,
                  States= new List<State>
                  {
                      new State
                      {
                          Name = "Antioquia",
                          Cities= new List<City>
                          {
                              new City {Name = "Medellín"},
                              new City {Name = "Envigado"},
                              new City {Name = "Rionegro"},
                            },
                      },
                       new State
                      {
                          Name = "Meta",
                          Cities= new List<City>
                          {
                              new City {Name = "Villavicencio"},
                              new City {Name = "Este"},
                              new City {Name = "Penco"},
                            },
                      },
                  }
                });_context.Countries.Add(new Country 
                { Name = "Peru" ,
                  States= new List<State>
                  {
                      new State
                      {
                          Name = "Puno",
                          Cities= new List<City>
                          {
                              new City {Name = "Me"},
                              new City {Name = "La"},
                              new City {Name = "Chupas?"},
                            },
                      },
                       new State
                      {
                          Name = "Cusco",
                          Cities= new List<City>
                          {
                              new City {Name = "No"},
                              new City {Name = "por"},
                              new City {Name = "perra"},
                            },
                      },
                  }
                });_context.Countries.Add(new Country 
                { Name = "Ecuador" ,
                  States= new List<State>
                  {
                      new State
                      {
                          Name = "Quito",
                          Cities= new List<City>
                          {
                              new City {Name = "Freddy"},
                              new City {Name = "Mercury"},
                              new City {Name = "Molina"},
                            },
                      },
                       new State
                      {
                          Name = "Diomedes",
                          Cities= new List<City>
                          {
                              new City {Name = "Perry"},
                              new City {Name = "Cacorro"},
                              new City {Name = "Gonorrea"},
                            },
                      },
                  }
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
