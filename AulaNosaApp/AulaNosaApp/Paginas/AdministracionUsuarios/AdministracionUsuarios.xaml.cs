﻿using AulaNosaApp.Servicios.AdministracionUsuarios;
using AulaNosaApp.Util;
using AulaNosaApp.Ventanas;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AulaNosaApp.Paginas
{
    /// <summary>
    /// Lógica de interacción para UsuarioAdm.xaml
    /// </summary>
    public partial class UsuarioAdm : Page
    {

        RestClient client;
        RestRequest request;
        bool filtrosActivados = false;

        public UsuarioAdm()
        {
            InitializeComponent();
            cbbFiltroUsuario.SelectedIndex = 0;
            refrescarUsuarios();
        }

        // Boton refrescar pantalla
        private void btnRefrescarPantallaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            refrescarUsuarios();
        }

        // Crear usuario
        private void btnCrearNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            // Ventana de creacion de usuario
            UsuarioCrear usuarioCrearVentana = new UsuarioCrear();
            usuarioCrearVentana.Show();
        }

        // Editar usuario
        private void btnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
           // Obtener el Usuario seleccionado
           var usuarioSeleccionado = dgvUsuarios.SelectedItem as UsuarioDTO;
           // Almacenar el usuario seleccionado y abrir la ventana de modificacion de usuario
           Statics.usuarioSeleccionado = usuarioSeleccionado;
           UsuarioModificar usuarioModificarVentana = new UsuarioModificar();
           usuarioModificarVentana.Show();
           // Deseleccionar seleccion
           btnEditarUsuario.IsEnabled = false;
           btnEliminarUsuario.IsEnabled = false;
           dgvUsuarios.SelectedItem = null;
        }

        // Eliminar usuario
        private void btnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el Usuario seleccionado
            var usuarioSeleccionado = dgvUsuarios.SelectedItem as UsuarioDTO;
            // Mostrar ventana de confirmacion de si quiere eliminar el usuario
            var resultado = MessageBox.Show("¿Desea eliminar este usuario?", "Eliminar Usuario", MessageBoxButton.YesNo);
            if (resultado == MessageBoxResult.Yes)
            {
              // Eliminar usuario
              AdmUsuariosAPI.eliminarUsuario(usuarioSeleccionado.id);
              // Refrescar DataGrid de usuarios
              refrescarUsuarios();
              // Deseleccionar seleccion
              btnEditarUsuario.IsEnabled = false;
              btnEliminarUsuario.IsEnabled = false;
              dgvUsuarios.SelectedItem = null;
            }else
            {
             // Refrescar DataGrid de usuarios
             refrescarUsuarios();
            }
        }

        private void btnBuscarFiltroUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (tbxFiltroUsuario.Text.Length == 0)
            {
                // Error al hacer una busqueda vacia
                MessageBox.Show("Busqueda vacia", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (cbbFiltroUsuario.SelectedIndex == 0)
                {
                    // Recoger por ID
                    UsuarioDTO usuarioId = AdmUsuariosAPI.filtrarUsuarioId(tbxFiltroUsuario.Text);
                    List<UsuarioDTO> usuarioIdRetornado = new List<UsuarioDTO>();
                    if(usuarioId != null)
                    {
                        usuarioIdRetornado.Add(usuarioId);
                    }
                    // Mostrarlo en un DataGrid
                    dgvUsuarios.ItemsSource = null;
                    dgvUsuarios.Items.Clear();
                    dgvUsuarios.ItemsSource = usuarioIdRetornado;
                }
                else if (cbbFiltroUsuario.SelectedIndex == 1)
                {
                    // Recoger Nombre
                    List<UsuarioDTO> usuariosListaNombre = AdmUsuariosAPI.filtrarUsuarioNombre(tbxFiltroUsuario.Text);
                    // Mostrarlo en un DataGrid
                    dgvUsuarios.ItemsSource = null;
                    dgvUsuarios.Items.Clear();
                    dgvUsuarios.ItemsSource = usuariosListaNombre;
                }
                else if (cbbFiltroUsuario.SelectedIndex == 2)
                {
                    // Recoger Rol
                    List<UsuarioDTO> usuariosListaRol = AdmUsuariosAPI.filtrarUsuarioRol(tbxFiltroUsuario.Text);
                    // Mostrarlo en un DataGrid
                    dgvUsuarios.ItemsSource = null;
                    dgvUsuarios.Items.Clear();
                    dgvUsuarios.ItemsSource = usuariosListaRol;
                }
                else if (cbbFiltroUsuario.SelectedIndex == 3)
                {
                    // Recoger Rol
                    List<UsuarioDTO> usuariosListaEmail = AdmUsuariosAPI.filtrarUsuarioEmail(tbxFiltroUsuario.Text);
                    // Mostrarlo en un DataGrid
                    dgvUsuarios.ItemsSource = null;
                    dgvUsuarios.Items.Clear();
                    dgvUsuarios.ItemsSource = usuariosListaEmail;
                }
            }
        }

        // Habilitar los botones de editar y eliminar al clickear un registro del DataGrid
        private void dgvUsuarios_Selected(object sender, RoutedEventArgs e)
        {
            btnEditarUsuario.IsEnabled = true;
            btnEliminarUsuario.IsEnabled = true;
        }

        // Refresca el DataGrid de usuarios
        void refrescarUsuarios()
        {
            Statics.usuariosLista = AdmUsuariosAPI.listarUsuarios();
            dgvUsuarios.ItemsSource = null;
            dgvUsuarios.Items.Clear();
            dgvUsuarios.ItemsSource = Statics.usuariosLista;
            if (Statics.usuariosLista != null)
            {
                Statics.ultimoIdUsuario = Statics.usuariosLista[Statics.usuariosLista.Count - 1].id;
            }
        }

		private void btnConsultaUsuario_Click(object sender, RoutedEventArgs e)
		{
            if (!filtrosActivados) {
                cbbFiltroUsuario.Visibility = Visibility.Visible;
                tbxFiltroUsuario.Visibility = Visibility.Visible;
                btnBuscarFiltroUsuario.Visibility = Visibility.Visible;
                filtrosActivados = true;
            } else {
                cbbFiltroUsuario.Visibility = Visibility.Collapsed;
                tbxFiltroUsuario.Visibility = Visibility.Collapsed;
                btnBuscarFiltroUsuario.Visibility = Visibility.Collapsed;
                filtrosActivados = false;
            }
        }
	}
}
