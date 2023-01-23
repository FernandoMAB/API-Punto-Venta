using API_Punto_Venta.Context;
using API_Punto_Venta.Controllers;
using API_Punto_Venta.Models;
using API_Punto_Venta.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_Punto_Venta_Tests;

public class UnitTest1
{
    private readonly ParametroController  _parametroController;
    private readonly IParametroService _parametroService;
    private PuntoVentaContext _context;


    public UnitTest1 (PuntoVentaContext context)
    {
        _context = context;
        _context.Parametrosgs.Add(new Parametrosg
        {
            ParId = 1,
            ParDescrip = "Par descrip",
            ParEstado = "V",
            ParNemonico = "TEST",
            ParValor = "10"
        
        });
        _parametroService = new ParametroService(_context);
        _parametroController = new ParametroController(_parametroService);
    }

    [Fact]
    public void Test1()
    {
        var result = _parametroController.GetAll();

        Assert.IsType<OkObjectResult>(result);
    }
}
