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
            var producto = new Producto(nombre: "Gaseosa", costo: 2000, precio: 5000, ventaDirecta: true);
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
        /*
         * HU1. SALIDA DE PRODUCTO (3.5)
        COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
        CRITERIOS DE ACEPTACIÓN
        1. La cantidad de la salida de debe ser mayor a 0
        2. En caso de un producto sencillo la cantidad de la salida se le disminuirá a la cantidad
        existente del producto.
        3. En caso de un producto compuesto la cantidad de la salida se le disminuirá a la cantidad
        existente de cada uno de su ingrediente
        4. Cada salida debe registrar el costo del producto y el precio de la venta
        5. El costo de los productos compuestos corresponden al costo de sus ingredientes por la
        cantidad de estos
         */
        [Test]
        public void PuedoRegistrarProductosdeSalidadSimple()
        {

            #region DADO EL RESTAURANTE TIENE VENTA DE  PRODUCTOS DE VENTA DIRECTA,COMO SE TIENEN REGISTRADO 3 GASEOSAS 
            var producto = new Producto(nombre: "Gaseosa", costo: 2000, precio: 5000, ventaDirecta: true);
            int cantidadEntrada = 3;
            var inventario = new Invetentario();
            List<Invetentario> invetentarios = new List<Invetentario>();
            invetentarios.Add(inventario);
            inventario.EntradaProductos(producto: producto, cantidad: cantidadEntrada, inventario: invetentarios);
            #endregion
            #region CUANDO se solicited la venta de una gaseosa
            int cantidadSalida = 1;
            string respuesta = inventario.SalidadeProductosSimple(producto: producto, cantidad: cantidadSalida, inventario: invetentarios);
            #endregion
            #region ENTONCES  el sistema registrara la salida del producto en el inventario y disminuira la cantidad del mismo 
            Assert.AreEqual(4000, inventario.Valor);
            Assert.AreEqual("Su Nueva cantidad de Gaseosa es de 2", respuesta);
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

        internal string SalidadeProductosSimple(Producto producto, int cantidad, List<Invetentario> inventario)
        {
            if (cantidad >= 0)
            {
                if (ExisteProducto(inventario, producto))
                {
                    Cantidad -= cantidad;
                    Valor = Producto.Costo*Cantidad;
                    return $"Su Nueva cantidad de {Producto.Nombre} es de {Cantidad}";
                }
               
                
            }
            throw new NotImplementedException();
        }
    }

    internal class Producto
    {
        public string Nombre { get; private set; }
        public decimal Costo { get; private set; }
        public decimal Precio { get; private set; }
        public decimal Utilidad { get => Precio - Costo; }

        public bool VentaDirecta { get; private set; }

        public Producto(string nombre, decimal costo, decimal precio, bool ventaDirecta)
        {
            Nombre = nombre;
            Costo = costo;
            Precio = precio;
            VentaDirecta = ventaDirecta;
        }
    }
}
