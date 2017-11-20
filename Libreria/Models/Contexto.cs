using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Libreria.Models
{
    public class Contexto: DbContext
    {
        public DbSet<Ejemplar> Ejemplares { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetallesOrdenes { get; set; }

    }
}