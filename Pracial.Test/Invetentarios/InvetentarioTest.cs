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
            var producto = new Producto(nombre: "Gaseosa", costo: 2000, precio: 5000, ventaDirecta:false);
            #endregion
            #region CUANDO registre 3 gaseosa
            int cantidad = 3;
            var inventario = new Invetentario();
            string respuesta = inventario.EntradaProductos(producto: producto, cantidad:cantidad);
            #endregion
            #region ENTONCES  el sistema registrara el producto en el inventario y adicionara la cantidad del mismo 
            Assert.AreEqual(6000, inventario.valor);
            Assert.AreEqual("Su Nueva cantidad de Gaseosa es de 3", respuesta);
            #endregion

        }
    }

    internal class Invetentario
    {
        internal double valor;

        public Invetentario()
        {
        }

        internal string EntradaProductos(Producto producto, int cantidad)
        {
            throw new NotImplementedException();
        }
    }

    internal class Producto
    {
        private string nombre;
        private int costo;
        private int precio;
        private bool ventaDirecta;

        public Producto(string nombre, int costo, int precio, bool ventaDirecta)
        {
            this.nombre = nombre;
            this.costo = costo;
            this.precio = precio;
            this.ventaDirecta = ventaDirecta;
        }
    }
}
