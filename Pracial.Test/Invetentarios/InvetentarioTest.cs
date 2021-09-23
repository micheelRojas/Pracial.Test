using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var producto = new ProductoSimple(nombre: "Gaseosa", costo: 2000, precio: 5000);
            #endregion
            #region CUANDO registre 3 gaseosa
            int cantidad = 3;
            string respuesta = producto.EntradaProductos(producto: producto, cantidad: cantidad);
            #endregion
            #region ENTONCES  el sistema registrara el producto en el inventario y adicionara la cantidad del mismo 
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
            var producto = new ProductoSimple(nombre: "Gaseosa", costo: 2000, precio: 5000);
            int cantidadEntrada = 3;

            producto.EntradaProductos(producto: producto, cantidad: cantidadEntrada);
            #endregion
            #region CUANDO se solicited la venta de una gaseosa por parte de un huespede
            var huespede = 1055;
            int cantidadSalida = 1;
            string respuesta = producto.SalidadeProductosSimple(producto: producto, cantidad: cantidadSalida, huespede: huespede);
            #endregion
            #region ENTONCES  el sistema registrara la salida del producto en el inventario y disminuira la cantidad del mismo 

            Assert.AreEqual("Su Nueva cantidad de Gaseosa es de 2", respuesta);
            #endregion

        }
        /*
         * HU1. SALIDA DE PRODUCTO (3.5)
        COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
        CRITERIOS DE ACEPTACIÓN
        1. La cantidad de la salida de debe ser mayor a 0
        3. En caso de un producto compuesto la cantidad de la salida se le disminuirá a la cantidad
        existente de cada uno de su ingrediente
        4. Cada salida debe registrar el costo del producto y el precio de la venta
        5. El costo de los productos compuestos corresponden al costo de sus ingredientes por la
        cantidad de estos
         */
        [Test]
        public void PuedoRegistrarProductosdeSalidadCompuesta()
        {
            /*
                         * un perro sencillo (ingredientes: un pan para perros, una salchicha, una lámina de queso)
            precio: 5.000. costo: calculado: 3.000, utilidad: precio - costo
             */

            #region DADO EL RESTAURANTE TIENE VENTA DE  PRODUCTOS DE VENTA INDIRECTA Que nesecitan transfotmacion 
            var panPerro = new Producto(nombre: "Salchica", costo: 1000, ventaDirecta: false);
            var salchicha = new Producto(nombre: "PanPerro", costo: 1000, ventaDirecta: false);
            var laminadequeso = new Producto(nombre: "LaminaQueso", costo: 1000, ventaDirecta: false);
            int cantidadEntrada = 3;
            panPerro.EntradaProductos(producto: laminadequeso, cantidad: cantidadEntrada);
            salchicha.EntradaProductos(producto: panPerro, cantidad: cantidadEntrada);
            laminadequeso.EntradaProductos(producto: salchicha, cantidad: cantidadEntrada);
            List<Producto> productos = new List<Producto>();
            productos.Add(panPerro);
            productos.Add(salchicha);
            productos.Add(laminadequeso);
            #endregion
            #region CUANDO se solicited la venta de un perro Sencillo
            var huespede = 1055;
            var perroSencillo = new ProductoCompuesto(nombre: "PerroSencillo", precio: 5000, productos);
            int cantidadSalida = 1;
            List<int> cantidades = new List<int>(){1,1,1};
            var productosPerro= perroSencillo.CrearProductoCompuesto(productos, cantidades);
            string respuesta = perroSencillo.SalidadeProductosCompuesto(producto: perroSencillo, cantidad: cantidadSalida, huespede: huespede, ingredientes: productosPerro);
            #endregion
            #region ENTONCES la cantidad de la salida se le disminuirá a la cantidad existente de cada uno de su ingrediente 
            Assert.AreEqual($"Su Nueva cantidad de PerroSencillo es de 0", respuesta);
            #endregion

        }
    }

    internal class VentaHuespede
    {
        public int Huespede { get; private set; }
        public decimal Venta { get; private set; }
        public Producto Producto { get; private set; }

        public VentaHuespede(Producto producto, int huespede, decimal venta)
        {
            Producto = producto;
            Huespede = huespede;
            Venta = venta;
        }
    }



    internal class Producto
    {
        public string Nombre { get; private set; }
        public decimal Costo { get; private set; }
        public bool VentaDirecta { get; private set; }
        public int Cantida { get; set; }
        protected List<VentaHuespede> _ventaHuespede;

        public Producto(string nombre, decimal costo, bool ventaDirecta)
        {
            Nombre = nombre;
            Costo = costo;
            VentaDirecta = ventaDirecta;
            _ventaHuespede = new List<VentaHuespede>();

        }

        public IReadOnlyCollection<VentaHuespede> VentaHuespedes => _ventaHuespede.AsReadOnly();
        internal virtual string EntradaProductos(Producto producto, int cantidad)
        {

            if (cantidad >= 0)
            {
                Cantida += cantidad;

                return $"Su Nueva cantidad de {Nombre} es de {Cantida}";
            }
            throw new NotImplementedException();
        }
    }
    internal class ProductoSimple : Producto
    {

        public decimal Precio { get; private set; }
        public decimal Utilidad { get => Precio - Costo; }


        public ProductoSimple(string nombre, decimal costo, decimal precio) : base(nombre, costo, true)
        {

            Precio = precio;
        }

        internal string SalidadeProductosSimple(ProductoSimple producto, int cantidad, int huespede)
        {
            if (cantidad >= 0)
            {

                Cantida -= cantidad;

                _ventaHuespede.Add(new VentaHuespede(producto: this, huespede: huespede, venta: producto.Precio * Cantida));
                return $"Su Nueva cantidad de {Nombre} es de {Cantida}";



            }
            throw new NotImplementedException();
        }

    }
    internal class ProductoCompuesto : Producto
    {
        public decimal Utilidad { get; private set; }
        public decimal Precio { get; private set; }
        public List<Producto> Productos { get; private set; }
        public ProductoCompuesto(string nombre, decimal precio, List<Producto> productos) : base(nombre, calcularCostos(productos), false)
        {
            Productos = productos;
            Precio = precio;
        }
        private static decimal calcularCostos(List<Producto> productos)
        {
            decimal sumaCostos = 0;
            for (int i = 0; i < productos.LongCount(); i++)
            {
                sumaCostos = sumaCostos + productos[i].Costo;
            }
            return sumaCostos;


        }
        internal List<Producto> CrearProductoCompuesto(List<Producto> productos, List<int> cantidades)
        {
            List<Producto> productosTemporales = new List<Producto>();
            Producto temporal;
            for (int i = 0; i < productos.LongCount(); i++)
            {
                temporal = productos[i];
                temporal.Cantida = cantidades[i];
                productosTemporales.Add(temporal);
            }
            Cantida++;
            return productosTemporales;
        }



        internal string SalidadeProductosCompuesto(ProductoCompuesto producto, int cantidad, int huespede, List<Producto> ingredientes)
        {
            if (cantidad >= 0)
            {

                for (int i = 0; i < producto.Productos.LongCount(); i++)
                {
                    SalidadeProductos(this.Productos[i], ingredientes[i].Cantida);
                }
                Cantida -= cantidad;
                _ventaHuespede.Add(new VentaHuespede(producto: this, huespede: huespede, venta: producto.Precio * cantidad));
                return $"Su Nueva cantidad de {Nombre} es de {Cantida}";



            }
            throw new NotImplementedException();
        }
        internal string SalidadeProductos(Producto producto, int cantidad)
        {
            if (cantidad >= 0)
            {

                producto.Cantida -= cantidad;
                return $"Su Nueva cantidad de {Nombre} es de {Cantida}";



            }
            throw new NotImplementedException();
        }

    }
}
