﻿using System;
using System.Collections.Generic;

namespace API_Punto_Venta.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Permisos = new HashSet<Permiso>();
            Usus = new HashSet<Usuario>();
        }

        public int RolId { get; set; }
        public string? RolDescrip { get; set; }
        public string? RolEstado { get; set; }

        public virtual ICollection<Permiso> Permisos { get; set; }

        public virtual ICollection<Usuario> Usus { get; set; }
    }
}
