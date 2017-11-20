using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Libreria.Models
{
    
        public partial class CarritodeCompras
        {
            Contexto storeDB = new Contexto();

            string CarritodeComprasId { get; set; }

            public const string CartSessionKey = "CarritoId";

            public static CarritodeCompras GetCart(HttpContextBase context)
            {
                var cart = new CarritodeCompras();
                cart.CarritodeComprasId = cart.GetCartId(context);
                return cart;
            }

            // Helper method to simplify shopping cart calls
            public static CarritodeCompras GetCart(Controller controller)
            {
                return GetCart(controller.HttpContext);
            }

            public void AddToCart(Ejemplar ejemplar)
            {
                // Get the matching cart and album instances
                var cartItem = storeDB.Carritos.SingleOrDefault(
    c => c.CarritoId == CarritodeComprasId
    && c.EjemplarId == ejemplar.EjemplarId);

                if (cartItem == null)
                {
                    // Create a new cart item if no cart item exists
                    cartItem = new Carrito
                    {
                        EjemplarId = ejemplar.EjemplarId,
                        CarritoId = CarritodeComprasId,
                        Conteo = 1,
                        FechaCreado = DateTime.Now
                    };

                    storeDB.Carritos.Add(cartItem);
                }
                else
                {
                    // If the item does exist in the cart, then add one to the quantity
                    cartItem.Conteo++;
                }

                // Save changes
                storeDB.SaveChanges();
            }

            public int RemoveFromCart(int id)
            {
                // Get the cart
                var cartItem = storeDB.Carritos.Single(
    cart => cart.CarritoId == CarritodeComprasId
    && cart.ArticuloId == id);

                int itemCount = 0;

                if (cartItem != null)
                {
                    if (cartItem.Conteo > 1)
                    {
                        cartItem.Conteo--;
                        itemCount = cartItem.Conteo;
                    }
                    else
                    {
                        storeDB.Carritos.Remove(cartItem);
                    }

                    // Save changes
                    storeDB.SaveChanges();
                }

                return itemCount;
            }

            public void EmptyCart()
            {
                var cartItems = storeDB.Carritos.Where(cart => cart.CarritoId == CarritodeComprasId);

                foreach (var cartItem in cartItems)
                {
                    storeDB.Carritos.Remove(cartItem);
                }

                // Save changes
                storeDB.SaveChanges();
            }

            public List<Carrito> GetCartItems()
            {
                return storeDB.Carritos.Where(cart => cart.CarritoId == CarritodeComprasId).ToList();
            }

            public int GetCount()
            {
                // Get the count of each item in the cart and sum them up
                int? count = (from cartItems in storeDB.Carritos
                              where cartItems.CarritoId == CarritodeComprasId
                              select (int?)cartItems.Conteo).Sum();

                // Return 0 if all entries are null
                return count ?? 0;
            }

            public decimal GetTotal()
            {
                // Multiply album price by count of that album to get 
                // the current price for each of those albums in the cart
                // sum all album price totals to get the cart total
                decimal? total = (from cartItems in storeDB.Carritos
                                  where cartItems.CarritoId == CarritodeComprasId
                                  select (int?)cartItems.Conteo * cartItems.Ejemplar.Precio).Sum();
                return total ?? decimal.Zero;
            }

            public int CreateOrder(Orden orden)
            {
                decimal orderTotal = 0;

                var cartItems = GetCartItems();

                // Iterate over the items in the cart, adding the order details for each
                foreach (var item in cartItems)
                {
                    var orderDetail = new DetalleOrden
                    {
                        EjemplarId = item.EjemplarId,
                        OrdenId = orden.OrdenId,
                        PrecioUnitario = item.Ejemplar.Precio,
                        Cantidad = item.Conteo
                    };

                    // Set the order total of the shopping cart
                    orderTotal += (item.Conteo * item.Ejemplar.Precio);

                    storeDB.DetallesOrdenes.Add(orderDetail);

                }

                // Set the order's total to the orderTotal count
                orden.Total = orderTotal;

                // Save the order
                storeDB.SaveChanges();

                // Empty the shopping cart
                EmptyCart();

                // Return the OrderId as the confirmation number
                return orden.OrdenId;
            }

            // We're using HttpContextBase to allow access to cookies.
            public string GetCartId(HttpContextBase context)
            {
                if (context.Session[CartSessionKey] == null)
                {
                    if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                    {
                        context.Session[CartSessionKey] = context.User.Identity.Name;
                    }
                    else
                    {
                        // Generate a new random GUID using System.Guid class
                        Guid tempCartId = Guid.NewGuid();

                        // Send tempCartId back to client as a cookie
                        context.Session[CartSessionKey] = tempCartId.ToString();
                    }
                }

                return context.Session[CartSessionKey].ToString();
            }

            // When a user has logged in, migrate their shopping cart to
            // be associated with their username
            public void MigrateCart(string userName)
            {
                var shoppingCart = storeDB.Carritos.Where(c => c.CarritoId == CarritodeComprasId);

                foreach (Carrito item in shoppingCart)
                {
                    item.CarritoId = userName;
                }
                storeDB.SaveChanges();
            }
        }
    }
