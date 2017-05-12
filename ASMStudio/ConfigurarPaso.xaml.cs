/*
 * Creado por SharpDevelop.
 * Usuario: tetra
 * Fecha: 12/05/2017
 * Hora: 3:26
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Gabriel.Cat.Extension;

namespace ASMStudio
{
	/// <summary>
	/// Interaction logic for ConfigurarPaso.xaml
	/// </summary>
	public partial class ConfigurarPaso :System.Windows.Controls.UserControl
	{
		
		public ConfigurarPaso(string texto,Bitmap bmpPaso,string pathAnterior="")
		{
			InitializeComponent();
			tbxDescripcionPaso.Text=texto;
			imgPaso.SetImage(bmpPaso);
			tbxRutaSeleccionada.Text=pathAnterior;
			
		}
		public string SelectedPath{
			
			get{return tbxRutaSeleccionada.Text;}

		}
		void BtnBuscarRuta_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog fbdBuscarRuta=new FolderBrowserDialog();
			if(fbdBuscarRuta.ShowDialog()==DialogResult.OK)
			{
				tbxRutaSeleccionada.Text=fbdBuscarRuta.SelectedPath;
			}
		}
	}
}