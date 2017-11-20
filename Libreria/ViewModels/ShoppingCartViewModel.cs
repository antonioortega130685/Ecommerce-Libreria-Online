using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria.Models;


namespace Libreria.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Carrito> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}