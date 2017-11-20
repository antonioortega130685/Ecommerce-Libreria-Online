using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Libreria.Models
{
    public class Carrito
    {
        [Key]
        public int ArticuloId { get; set; }
        public string CarritoId { get; set; }
        public int EjemplarId { get; set; }
        public int Conteo { get; set; }
        public System.DateTime FechaCreado { get; set; }

        public virtual Ejemplar Ejemplar { get; set; }
    }
}