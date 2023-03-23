using AulaNosaApp.DTO;
using AulaNosaApp.DTO.AdministracionCursos;
using AulaNosaApp.Servicios;
using AulaNosaApp.Servicios.AdministracionCursos;
using AulaNosaApp.Util;
using Org.BouncyCastle.Asn1.Ocsp;
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

namespace AulaNosaApp
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CursoDTO> listaCursos = CursosApi.listarCursos();
        List<EstudioDTO> listaEstudios = EstudioApi.ListarEstudios();
        public MainWindow()
        {
            InitializeComponent();
            // Ocultar paneles de gestion para mostrar solo el menu de acceso
            spnMenuIzqda.Visibility = Visibility.Collapsed;
            grdMenuSuperior.Visibility = Visibility.Collapsed;
            tbxUsuario.Text = string.Empty;
            pbxContrasena.Password = string.Empty;

            List<String> listaNombresCursos = new List<String>();
            foreach (var curso in listaCursos)
            {
                listaNombresCursos.Add(curso.nombre);
            }
            cbbCursos.ItemsSource = listaNombresCursos;

            List<String> listaNombresEstudios = new List<String>();
            foreach (var estudio in listaEstudios)
            {
                listaNombresEstudios.Add(estudio.nombre);
            }
            cbbEstudios.ItemsSource = listaNombresEstudios;
        }

        // Accion del boton de iniciar sesion
        private void iniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(Constantes.client);
            var request = new RestRequest("/api/usuario/nombreEs/" + tbxUsuario.Text, Method.Get);
            RestResponse respuesta = client.Execute(request);

            UsuarioDTO usuario = client.Execute<UsuarioDTO>(request).Data;

            if (usuario == null)
            {
                // No existe el usuario
                MessageBox.Show("Usuario no existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (usuario.nombre == tbxUsuario.Text && usuario.password == pbxContrasena.Password)
            {
                if (usuario.rol == "ALUMNO")
                {
                    // No permite entrar si es rol de alumno
                    MessageBox.Show("Tipo de usuario no apto para entrar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // Ocultar menu de acceso
                    spnAcceso.Visibility = Visibility.Hidden;
                    // Mostrar paneles de gestion
                    spnMenuIzqda.Visibility = Visibility.Visible;
                    grdMenuSuperior.Visibility = Visibility.Visible;
                    frmPrincipal.Visibility = Visibility.Visible;
                    frmPrincipal.Source = null;
                    // Pestaña de informacion usuario/cerrar sesion
                    cbbUsuario.SelectedIndex = 0;
                    // Mostrar nombre y rol del usuario
                    txbNombreUsuarioLogueado.Text = usuario.nombre;
                    txbRolUsuarioLogueado.Text = usuario.rol;
                    // Ocultar paneles en funcion del rol que tenga
                    if (usuario.rol == "ADMIN")
                    {
                        // Si es Admin
                    }
                    else
                    {
                        // Si es editor/profesor
                    }
                }
            }
            else
            {
                // No permite entrar si los datos estan mal
                MessageBox.Show("Tipo de usuario no apto para entrar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Accion del boton de cerrar sesion
        private void btnCerrarSesion_Selected(object sender, RoutedEventArgs e)
        {
            spnAcceso.Visibility = Visibility.Visible;
            spnMenuIzqda.Visibility = Visibility.Collapsed;
            grdMenuSuperior.Visibility = Visibility.Collapsed;
            frmPrincipal.Visibility = Visibility.Hidden;
            cbbUsuario.SelectedIndex = 0;
            tbxUsuario.Text = string.Empty;
            pbxContrasena.Password = string.Empty;
        }
        // Panel de usuarios
        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(new Uri("/Paginas/AdministracionUsuarios/AdministracionUsuarios.xaml", UriKind.Relative));
        }

        // Panel de cursos
        private void btnCursos_Click(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(new Uri("/Paginas/AdministracionCursos/AdministracionCursos.xaml", UriKind.Relative));
        }

        // Panel de estudios
        private void btnEstudios_Click(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(new Uri("/Paginas/AdministracionEstudios/AdministracionEstudios.xaml", UriKind.Relative));
        }

        // Panel del calendario FCT
        private void btnCalendarioFct_Click(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(new Uri("/Paginas/Calendario/investigacionCalendario.xaml", UriKind.Relative));
        }

        // Panel del calendario PEXT
        private void btnCalendarioPext_Click(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(new Uri("/Paginas/Calendario/investigacionCalendario.xaml", UriKind.Relative));
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(new Uri("/Paginas/InfoPFC/InfoPFC.xaml", UriKind.Relative));
        }

        private void btnAlumnadoExterno_Click(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(new Uri("/Paginas/GestionAlumnadoExterno/GestionAlumnadoExterno.xaml", UriKind.Relative));
        }

        private void btnAlumnoEmpresa_Click(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(new Uri("/Paginas/AlumnoEmpresa/AlumEmpResumen.xaml", UriKind.Relative));
        }

        private void btnEmpresaAlumnos_Click(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(new Uri("/Paginas/EmpresaAlumnos/EmpAlumResumen.xaml", UriKind.Relative));
        }

        private void cbbEstudios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String cursoSeleccionado = cbbCursos.Text;
            foreach (CursoDTO curso in listaCursos)
            {
                if (curso.nombre == cursoSeleccionado)
                {
                    Statics.idCursoElegido = curso.id;
                }
            }
        }

        private void cbbCursos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String estudioSeleccionado = cbbEstudios.Text;
            foreach (EstudioDTO estudio in listaEstudios)
            {
                if (estudio.nombre == estudioSeleccionado)
                {
                    Statics.idEstudioElegido = estudio.id;
                }
            }
        }

        private void btnEmpresas_Click(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(new Uri("/Paginas/InfEmpresas/InfEmpresas.xaml", UriKind.Relative));
        }
    }
}
