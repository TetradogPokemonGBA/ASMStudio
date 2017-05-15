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
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Gabriel.Cat.Extension;

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
			if(MessageBox.Show("Esta app esta desarrollada bajo licencia GPL GNU V3\nDesarrollador: Pikachu240\nCréditos:\nAndrea por el compilador.\nJavi4315 por sus conocimientos y ayuda.\nDedicado a Wahack una gran comunidad.\n\n¿Desa ver la pagina web donde esta el codigo fuente?","Sobre la aplicación",MessageBoxButton.YesNo,MessageBoxImage.Information)==MessageBoxResult.Yes)
				System.Diagnostics.Process.Start("https://github.com/TetradogPokemonGBA/ASMStudio");
		}
		void MiLogin_Click(object sender, RoutedEventArgs e)
		{
			LoginOnWahack login;
			MenuItem miSalir;
			if(miLogin.Header.ToString().Contains("Login"))
			{
				login=new LoginOnWahack();
				//espero a que este descargada la web
				
				login.ShowDialog();
				
				if(login.IsConnected)
				{
					imgPerfil.Source=login.ImgPerfil.Source;
					miLogin.Header=login.User;
					miSalir=new MenuItem();
					miSalir.Header="Salir";
					miSalir.Click+=(s,m)=>
					{	
						LoginOnWahack logout=(LoginOnWahack)miLogin.Tag;
						
						logout.Salir();
						if(!logout.IsConnected)
						{
							imgPerfil.SetImage(new Bitmap(1,1));
							miLogin.Header="Login";
							miLogin.Items.Clear();
							miLogin.Click+=MiLogin_Click;
						}
					
					};
					miLogin.Tag=login;
					miLogin.Items.Add(miSalir);
					miLogin.Click-=MiLogin_Click;
				}
			}
		}
	}
}