﻿using AulaNosaApp.Servicios;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AulaNosaApp.Paginas.InfoPFC
{
    /// <summary>
    /// Lógica de interacción para InfoPFC.xaml
    /// </summary>
    public partial class InfoPFC : Page
    {
        public InfoPFC()
        {
            InitializeComponent();
            tbkTextoRubrica.Text = InfoPFCApi.mostrarInformacion();
        }
    }
}