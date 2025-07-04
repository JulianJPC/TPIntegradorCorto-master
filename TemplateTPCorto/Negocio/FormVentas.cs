﻿using Datos;
using Datos.Ventas;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateTPCorto
{
    public partial class FormVentas : Form
    {
        private Credencial laCredencial { get; set; }
        public FormVentas(Credencial aCredencial)
        {
            InitializeComponent();
            laCredencial = aCredencial;
            this.Text = "Ventas";
        }
        /// <summary>
        /// Cuando carga obtiene los clientes, categorias y inicializa los totales
        /// </summary>
        private void FormVentas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarCategoriasProductos();
            IniciarTotales();
        }

        private void IniciarTotales()
        {
            lablSubTotal.Text = "0.00";
            lblTotal.Text = "0.00";
        }

        /// <summary>
        /// Obtiene las categoriasProductos y las añade al combo de categorias
        /// </summary>
        private void CargarCategoriasProductos()
        {
            
            VentasNegocio ventasNegocio = new VentasNegocio();

            List<CategoriaProductos> categoriaProductos = ventasNegocio.obtenerCategoriaProductos();

            foreach (CategoriaProductos categoriaProducto in categoriaProductos)
            {
                cboCategoriaProductos.Items.Add(categoriaProducto);
            }
        }

        /// <summary>
        /// Obtiene los clientes y los añade en el combo de clientes
        /// </summary>
        private void CargarClientes()
        {
            VentasNegocio ventasNegocio = new VentasNegocio();

            List<Cliente> listadoClientes = ventasNegocio.obtenerClientes();

            foreach (Cliente cliente in listadoClientes)
            {
                cmbClientes.Items.Add(cliente);
            }
        }

        /// <summary>
        /// Cuando se hace click en el boton de listar productos
        /// obtiene la categoria seleccionada del combo de categeorias y 
        /// trae todos los productos de la BD y los introduce en el listbox de productos
        /// </summary>
        private void btnListarProductos_Click(object sender, EventArgs e)
        {
            if(cboCategoriaProductos.SelectedItem is CategoriaProductos)
            {
                var categoria = (CategoriaProductos)cboCategoriaProductos.SelectedItem;
                ProductoNegocio prodNegocio = new ProductoNegocio();
                var losProductos = prodNegocio.obtenerProductosPorCategoria(categoria);
                lstProducto.DataSource = losProductos;
            }
            else
            {
                MessageBox.Show("No hay categoría seleccionada.");
            }
        }
        /// <summary>
        /// Al hacer click de agregar el producto seleccionado en la listbox de productos en el carrito
        /// Se fija si la cantidad introducida y el producto estan bien.
        /// Despues se fija si hay stock del producto dado la cantidad introducida y la cantidad en el carrito.
        /// Si pasa eso, crea un producto y le asigna valor a la cantidad elegida y guarda en el carrito.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var cantidad = 0;
            Producto unProducto;
            var messageError = "";
            if(Int32.TryParse(txtCantidad.Text, out cantidad) && lstProducto.SelectedItem is Producto)
            {
                unProducto = (Producto)lstProducto.SelectedItem;
                if(unProducto.Stock >= cantidad + getCantidadesEnCarrito(unProducto.Id))
                {
                    unProducto.CantidadElegida = cantidad;
                    lstCarrito.Items.Add(unProducto);
                    carritoHasChange();
                }
                else
                {
                    messageError = "No hay stock suficiente.";
                }
            }
            else
            {
                messageError = "No hay cantidad o no hay producto seleccionado.";
            }

            if( messageError != "")
            {
                MessageBox.Show(messageError);
            }
        }
        /// <summary>
        /// Dado un id de producto se fija en el carrito los productos del mismo id
        /// y devuelve la suma de las cantidades elegidas.
        /// </summary>
        /// <param name="idProducto">Id de producto valido</param>
        private int getCantidadesEnCarrito(Guid idProducto)
        {
            var response = 0;
            foreach(var item in lstCarrito.Items)
            {
                if(item is Producto)
                {
                    var productoItem = (Producto)item;
                    if(productoItem.Id == idProducto)
                    {
                        response += productoItem.CantidadElegida;
                    }
                }
            }
            return response;
        }
        /// <summary>
        /// Actualiza el subtotal y total dado los items del carrito.
        /// </summary>
        private void carritoHasChange()
        {
            var total = 0.00;
            foreach( var item in lstCarrito.Items)
            {
                if (item is Producto)
                {
                    var itemProducto = (Producto)item;
                    total += itemProducto.CantidadElegida * itemProducto.Precio;
                }
            }
            lablSubTotal.Text = total.ToString();
            if(total > 1000000)
            {
                lblTotal.Text = (total * 0.85).ToString();
            }
            else
            {
                lblTotal.Text = total.ToString();
            }
        }
        /// <summary>
        /// Elimina un item del carrito seleccionado
        /// </summary>
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (lstCarrito.SelectedItem is Producto)
            {
                lstCarrito.Items.Remove(lstCarrito.SelectedItem);
                carritoHasChange();
            }
            else
            {
                MessageBox.Show("No hay item en carrito seleccionado");
            }
        }
        /// <summary>
        /// Carga las ventas, primero obtiene el Cliente y los productos. Se fija alla productos y el cliente no sea nulo
        /// Despues crea una lista de objectos ventas y dada la info de los prod y clientes crea las ventas
        /// y almacena en la lista.
        /// Al final manda a que se introduzcan en la base de datos y devuelve el mensaje del resultado.
        /// </summary>
        private void btnCargar_Click(object sender, EventArgs e)
        {
            Cliente elCliente = null;
            List<Producto> listProductos = new List<Producto>();
            if(cmbClientes.SelectedItem is Cliente)
            {
                elCliente = cmbClientes.SelectedItem as Cliente;
            }
            foreach(var item in lstCarrito.Items)
            {
                if(item is Producto)
                {
                    var itemProducto = (Producto)item;
                    listProductos.Add(itemProducto);
                }
            }
            if(elCliente != null && listProductos.Count > 0)
            {
                var listVentas = new List<Venta>();
                foreach(var item in lstCarrito.Items)
                {
                    if(item is Producto)
                    {
                        var itemProducto = (Producto)item;
                        var newVenta = new Venta(elCliente.Id, itemProducto.Id, itemProducto.CantidadElegida);
                        
                        listVentas.Add(newVenta); 
                    }
                }
                var ventNegocio = new VentasNegocio();
                var msgResult = ventNegocio.createVenta(listVentas);
                MessageBox.Show(msgResult);
                lstCarrito.Items.Clear();
            }
            else
            {
                MessageBox.Show("Cliente no seleccionado o Carrito vacio.");
            }

        }
    }
}
