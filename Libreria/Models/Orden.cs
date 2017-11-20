using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Libreria.Models
{
    [Bind(Exclude = "OrdenId")]
    public class Orden
    {
        [ScaffoldColumn(false)]
        public int OrdenId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime FechaOrden { get; set; }

        [ScaffoldColumn(false)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Se requiere nombre")]
        [DisplayName("Nombre")]
        [StringLength(160)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Se requiere apellido")]
        [DisplayName("Apellido")]
        [StringLength(160)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Se requiere domicilio")]
        [StringLength(70)]
        public string Domicilio { get; set; }

        [Required(ErrorMessage = "Se requiere ciudad")]
        [StringLength(40)]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Se requiere provincia")]
        [StringLength(40)]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Se requiere codigo postal")]
        [DisplayName("Codigo Postal")]
        [StringLength(10)]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "Se requiere pais")]
        [StringLength(40)]
        public string Pais { get; set; }

        [Required(ErrorMessage = "Se requiere telefono")]
        [StringLength(24)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Se requiere Email")]
        [DisplayName("Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email no valido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<DetalleOrden> DetallesOrden { get; set; }
    }
}