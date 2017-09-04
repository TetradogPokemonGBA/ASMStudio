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
		RomHackingWebConnector.WahackConnector login;
		public event EventHandler AbrirConfiguracion;
		RowDefinition rowUser;
		public Main()
		{
			InitializeComponent();
			
			login = new RomHackingWebConnector.WahackConnector();
			login.Conectado += Conectado;
			login.Desconectado += Desconectado;
			//es el ultimo
			gMain.Children.Add(login);
			rowUser = gParent.RowDefinitions[2];
			gParent.RowDefinitions.Remove(rowUser);
			
		}

		void MiSalir_Click(object sender, RoutedEventArgs e)
		{
			Desconectado();
		}
		void Conectado(object sender = null, EventArgs e = null)
		{
			
			if (login.EstaConectado) {
				tbDeveloper.Text = "Desarrollador:\t" + login.User;
				imgPerfil.SetImage(login.ImagenPerfil);
				miLogin.Header = "Salir";
				miLogin.Click -= MiLogin_Click;
				miLogin.Click += MiSalir_Click;
				gParent.RowDefinitions.Add(rowUser);
				gUser.Visibility = Visibility.Visible;
			}
		}

		void Desconectado(object sender = null, EventArgs e = null)
		{
			if (!miLogin.Header.ToString().Contains("Login")) {
				login.Salir();
				if (!login.EstaConectado) {
					miLogin.Header = "Login";
					login.Hide();
					miLogin.Click += MiLogin_Click;
					miLogin.Click -= MiSalir_Click;
					tbDeveloper.Text = "";
					imgPerfil.SetImage(new Bitmap(1, 1));
					gUser.Visibility = Visibility.Hidden;
					gParent.RowDefinitions.Remove(rowUser);
				}
			}//else seria si el autologin devuelve false asi que no me interesa molestar al usuario con algo que hago además
		}
		void MiConfiguracion_Click(object sender, RoutedEventArgs e)
		{
			if (AbrirConfiguracion != null)
				AbrirConfiguracion(new object(), new EventArgs());
		}
		void MiSobre_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Esta app esta desarrollada bajo licencia GPL GNU V3\nDesarrollador: Pikachu240\nCréditos:\nAndrea por el compilador.\nJavi4315 por sus conocimientos y ayuda.\nDedicado a Wahack una gran comunidad.\n\n¿Desa ver la pagina web donde esta el codigo fuente?", "Sobre la aplicación", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
				System.Diagnostics.Process.Start("https://github.com/TetradogPokemonGBA/ASMStudio");
		}
		void MiLogin_Click(object sender, RoutedEventArgs e)
		{
			login.Entrar();
			if (login.EstaConectado) {
				Conectado();
			}
		}
	}
}