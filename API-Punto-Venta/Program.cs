using System.Reflection;
using System.Text;
using API_Punto_Venta.Context;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conPuntoVenta = $"Data Source={Environment.GetEnvironmentVariable("DataSource")}" +
    $";Initial Catalog={Environment.GetEnvironmentVariable("InitialC")}; " +
    $"user id={Environment.GetEnvironmentVariable("User")};" +
    $"password={Environment.GetEnvironmentVariable("Pass")}";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Punto de Venta",
        Description = "La idea central del proyecto es la de ayudar a los negocios a optimizar sus procesos de ventas, " +
        "brindándoles un sistema de punto de venta que cuente con herramientas que les permitan mejorar sus principales " +
        "actividades al momento de manejar el negocio. Esto se refleja principalmente en la experiencia que tiene el usuario " +
        "en una compra, lo cual es crucial para el negocio, por esta razón los tiempos entre transacciones deben ser cortos. " +
        "Este programa busca reducir el tiempo que se emplea en los procesos de captura de datos o en cálculos que apliquen a la " +
        "compra como el IVA o algún descuento definido para uno de los productos, además de eso, permite mantener un control del " +
        "stock de los productos, de esta manera el dueño del negocio puede saber cuando es tiempo de abastecerse nuevamente de producto.",
        //TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Mateo Cordova, Bruno Dueñas y Fernando Mejía",
            Email = "fernando.mejia@udla.edu.ec"
            
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSqlServer<PuntoVentaContext>(conPuntoVenta);
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IClienteService,  ClienteService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ICajaService,     CajaService>();
builder.Services.AddScoped<IUsuarioService,  UsuarioService>();
builder.Services.AddScoped<IRolService,      RolService>();
builder.Services.AddScoped<IPermisoService,  PermisoService>();
builder.Services.AddScoped<ICategoriaService,CategoriaService>();
builder.Services.AddScoped<IParametroService,ParametroService>();
builder.Services.AddScoped<ICatalogoService, CatalogoService>();
builder.Services.AddScoped<IDocumentoService,DocumentoService>();
builder.Services.AddTransient<IFacturaService, FacturaService>();
builder.Services.AddTransient<IKardexService, KardexService>();
builder.Services.AddTransient<IReportService, ReportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.InjectJavascript("/swagger-ui/custom.js");
        c.InjectJavascript("/swagger-ui/rapipdf-min.js");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
