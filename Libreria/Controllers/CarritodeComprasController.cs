using System.Linq;
using System.Web.Mvc;
using Libreria.Models;
using Libreria.ViewModels;

namespace Libreria.Controllers
{
    public class CarritodeComprasController : Controller
    {
        Contexto storeDB = new Contexto();

        //
        // GET: /CarritodeCompras/

        public ActionResult Index()
        {
            var cart = CarritodeCompras.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        //
        // GET: /Tienda/AddToCart/5

        public ActionResult AddToCart(int id)
        {

            // Retrieve the album from the database
            var addedEjemplar = storeDB.Ejemplares
                .Single(ejemplar => ejemplar.EjemplarId == id);

            // Add it to the shopping cart
            var cart = CarritodeCompras.GetCart(this.HttpContext);

            cart.AddToCart(addedEjemplar);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }


        //public void Eliminar(int id)
        //{

        //    var MyVenta = Session["MyVenta"] as ShoppingCartViewModel;

        //    var itemABorrar = MyVenta.CartItems.Single(r => r.ArticuloId == id);
        //    if (itemABorrar != null)
        //        MyVenta.CartItems.Remove(itemABorrar);
        //}

        //

        


        // AJAX: /CarritodeCompras/RemoveFromCart/5

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = CarritodeCompras.GetCart(this.HttpContext);

            //Get the name of the album to display confirmation
            string albumName = storeDB.Carritos
                .Single(item => item.ArticuloId == id).Ejemplar.Titulo;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                    " a sido removido del carrito de compras.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        //
        // GET: /CarritodeCompras/CartSummary

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = CarritodeCompras.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }
    }
}