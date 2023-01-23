namespace API_Punto_Venta.Util;

public static class Constants
{
    public const int ROUND_VAL = 4;
    public const int ROUND_VAL_2 = 2;

    public const string ESTADO_ELIMINADO = "E";
    public const string ESTADO_VIGENTE = "V";


    public const string NOTFOUND = @"No se encontró registro";
    public const string NOTSTOCK = @"No se existe el stock necesario en el producto ";
    public const string NOTCANT = @"Debe enviar el valor de la cantidad de los productos ";
    public const string ROLENOTFOUND = @"Cliente no tiene dicho rol";
    public const string CAJANOTFOUND = @"Cliente no tiene dicha caja";
    public const string MULTIPLENOTFOUND = @"No se encontraron registros";
    public const string NEGATIVEBALANCE = @"El saldo no puede ser negativo";
    public const string NONAVAILABLEBALANCE = @"Saldo no disponible";
    public const string CLIENTEXISTS = @"El cliente ya está registrado";
    public const string CLIENTNOTEXISTS = @"El cliente no existe";
    public const string NONACCOUNT = @"El cliente no tiene cuentas con nosotros";
    public const string OBJECTISNULL = @"El objeto no puede ser nulo";
    public const string NONPERSON = @"No existe persona";
    public const string NONUSER = @"No existe usuario";
    public const string NONCATA = @"No existe catalogo";
    public const string NONFACT = @"No existe factura";
    public const string NONKARD = @"No existe kardex";
    public const string NONPROD = @"No existe producto";
    public const string NONPERM = @"No existe permiso";
    public const string NONEROL = @"No existe rol";
    public const string NONDOCU = @"No existe documento";
    public const string NONCALE = @"No existe calificación";
    public const string NONMATE = @"No existe materia";
    public const string USEREPE = @"Ya existe un usuario con este nombre de usuario";
    public const string COLREPE = @"Ya existe un colegio con este nombre";
    public const string NEMREPE = @"Ya existe un Nemonico con este nombre";
    public const string MATREPE = @"Ya existe una materia con este nombre";
    public const string NONREGIST = @"No existen registros";
    public const string UPDATEEX = @"Registro actualizado!";
    public const string DELREGEX = @"Registro eliminado!";
    public const string SOLDENEG = @"Solicitud Denegada";
    public const string ERROLOGI = @"Error en el Login";
    
    public const string CATAREPE = @"Ya existe un catálogo con el mismo código y nombre";
    public const string DOCUREPE = @"Ya existe un documento con el mismo nombre y extensión";
    public const string USERREPE = @"Ya existe un usuario con el mismo nombre de usuario";
    public const string ROLREPE = @"Ya existe un rol con la misma descripción";
    public const string PRODREPE = @"Ya existe un producto con el mismo código de barras";
    public const string CLIEREPE = @"Ya existe un cliente con el mismo tipo y número de identificación";
    
    public const string ERRNOTFU = @"El objeto no fue encontrado.";
    public const string ERRBUSEX = @"La operación presentó un error durante la ejecución.";
    public const string ERRPROBM = @"Se presentó un error durante el procesamiento de la solicitud.";
}
