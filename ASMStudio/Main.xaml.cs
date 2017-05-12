/*
 * Creado por SharpDevelop.
 * Usuario: tetra
 * Fecha: 12/05/2017
 * Hora: 5:55
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ASMStudio
{
	/// <summary>
	/// Interaction logic for Main.xaml
	/// </summary>
	public partial class Main : UserControl
	{
		public event EventHandler AbrirConfiguracion;
		public Main()
		{
			InitializeComponent();
		}
		void MiConfiguracion_Click(object sender, RoutedEventArgs e)
		{
			if(AbrirConfiguracion!=null)
				AbrirConfiguracion(new object(),new EventArgs());
		}
		void MiSobre_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}