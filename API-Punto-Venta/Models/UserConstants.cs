using System.Collections.Generic;
namespace API_Punto_Venta.Models
{
    public class UserConstants{
        public static List<Usuario> Users = new List<Usuario>()
        {
            new Usuario(){
                UsuId = 1, 
                UsuPNombre = "Admin",
                UsuNumeroIden = "12312321",
                UsuContrasena = "admin",
                UsuEstado = "A",
                UsuEmail = "fer@fer.com"
            },
            new Usuario(){
                UsuId = 2, 
                UsuPNombre = "Fer",
                UsuNumeroIden = "0897687",
                UsuContrasena = "admin",
                UsuEstado = "E",
                UsuEmail = "fer@fer.com"
            }
        };
    }
}