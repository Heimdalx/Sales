using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sales.WEB;
using Sales.WEB.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Tres formas de inyectar dependencias .NET 
/**AddScope  Nueva instancia del objeto cada vez que se inyecta
 * AddTranscient Solo se crea una vez, una sola inyeccion, por ej base de datos
 * AddSingleton Primer vez crea objeto y reutiliza cada vez que se inyecta, cuidaod con seguridad
 */
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7070/") });
//Se debe hacer la inyeccion de la clase con la interfaz que implementa
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
