using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pracial.Test.Invetentarios
{
   class InvetentarioTest
    {


        /*
         HU1. ENTRADA DE PRODUCTO (1.5)
        COMO USUARIO QUIERO REGISTRAR LA ENTRADA PRODUCTOS
        CRITERIOS DE ACEPTACIÓN
         1. La cantidad de la entrada de debe ser mayor a 0
         2. La cantidad de la entrada se le aumentará a la cantidad existente del producto
         */
        [Test]
        public void PuedoRegistrarProductosdeEntrada()
        {

            #region DADO EL RESTAURANTE TIENE PRODUCTO DE GASEODA DE LITRO CON UN PRECIO DE 5000 Y UN COSTO 2000 Y NO NECESITA PREPARACION
            var producto = new Producto(nombre: "Gaseosa", costo: 2000, precio: 5000, ventaDirecta: false);
            #endregion
            #region CUANDO registre 3 gaseosa
            int cantidad = 3;
            var inventario = new Invetentario();
            List<Invetentario> invetentarios= new List<Invetentario>();
            invetentarios.Add(inventario);
            string respuesta = inventario.EntradaProductos(producto: producto, cantidad:cantidad, inventario: invetentarios);
            #endregion
            #region ENTONCES  el sistema registrara el producto en el inventario y adicionara la cantidad del mismo 
            Assert.AreEqual(6000, inventario.Valor);
            Assert.AreEqual("Su Nueva cantidad de Gaseosa es de 3", respuesta);
            #endregion

        }
    }

    internal class Invetentario
    {
        public decimal Valor { get; private set; }
        public int Cantidad { get; private set; }
        public Producto Producto { get; private set; }
        public Invetentario()
        {
        }

        internal string EntradaProductos(Producto producto, int cantidad, List<Invetentario> inventario)
        {
           
            if (cantidad >= 0) {
                if (ExisteProducto(inventario, producto))
                {  
                }
                else {
                    Producto = producto;
                }
                Cantidad += cantidad;
                Valor = producto.Costo * Cantidad;

                return $"Su Nueva cantidad de {Producto.Nombre} es de {Cantidad}";
            }
            throw new NotImplementedException();
        }

        internal bool ExisteProducto(List<Invetentario> inventario, Producto producto) {
            if (inventario.FirstOrDefault(t => t.Producto == producto) != null) {
                return true;
            }
            return false;


        }
       
    }

    internal class Producto
    {
        public string Nombre { get; private set; }
        public int Costo { get; private set; }
        public int Precio { get; private set; }
        public bool VentaDirecta { get; private set; }

        public Producto(string nombre, int costo, int precio, bool ventaDirecta)
        {
            Nombre = nombre;
            Costo = costo;
            Precio = precio;
            VentaDirecta = ventaDirecta;
        }
    }
}
