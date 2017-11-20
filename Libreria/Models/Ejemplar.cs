using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Libreria.Models
{
    [Bind(Exclude = "EjemplarId")]
    public class Ejemplar
    {
        [ScaffoldColumn(false)]
        public int EjemplarId { get; set; }

        [DisplayName("Genero")]
        public int GeneroId { get; set; }

        [DisplayName("Autor")]
        public int AutorId { get; set; }

        [Required(ErrorMessage = "Se requiere titulo de ejemplar")]
        [StringLength(160)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Se requiere un precio")]
        [Range(0.01, 1000.00,
            ErrorMessage = "El precio debe estar entre 0.01 y 1000.00")]
        public decimal Precio { get; set; }

        [DisplayName("URL de arte de tapa")]
        [StringLength(1024)]
        public string ArtedetapaEjemplar { get; set; }

        public virtual Genero Genero { get; set; }
        public virtual Autor Autor { get; set; }
        public virtual List<DetalleOrden> DetallesOrden { get; set; }
    }
}