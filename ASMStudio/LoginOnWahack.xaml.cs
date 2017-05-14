/*
 * Creado por SharpDevelop.
 * Usuario: tetra
 * Fecha: 13/05/2017
 * Hora: 21:02
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
//using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using Gabriel.Cat.Extension;
using Gabriel.Cat.Seguretat;

namespace ASMStudio
{
	/// <summary>
	/// Interaction logic for LoginOnWahack.xaml
	/// </summary>
	public partial class LoginOnWahack : Window
	{

		public const string WEB = "https://wahackforo.com";

		System.Windows.Forms.WebBrowser wbWahack;
		System.Windows.Forms.HtmlDocument htmlWahackHomePage;
		
		string user;
		Image imgPerfil;
		
		public LoginOnWahack()
		{
			wbWahack = new System.Windows.Forms.WebBrowser();
			wbWahack.ScriptErrorsSuppressed=true;
			
			InitializeComponent();
		    RecargarPaginaWahack();
		}

		public bool IsConnected {
			get;
			private	set;
		}

		public string User {
			get {
				return user;
			}
			private set{
				user=value;
			}
		}
		
		public Image ImgPerfil {
			get {
				System.Windows.Forms.WebBrowser webPerfilUser;
				System.Windows.Forms.HtmlElementCollection linksHomePage;
				System.Windows.Forms.HtmlElementCollection imgsPerfil;
				Uri uriPerfil = null;
				Uri uriImagenPerfil;
				if (imgPerfil == null) {
					linksHomePage = htmlWahackHomePage.GetElementsByTagName("a");
					//cojo el link del perfil
					for (int i = 0; i < linksHomePage.Count && uriPerfil == null; i++) {
						if (linksHomePage[i].InnerText.Contains(User))
							uriPerfil = new Uri(linksHomePage[i].GetAttribute("href"));
					}
					if (uriPerfil != null) {
						webPerfilUser = new System.Windows.Forms.WebBrowser();
						webPerfilUser.Navigate(uriPerfil);
						webPerfilUser.DocumentCompleted += (s, e) => {
							
							imgsPerfil = webPerfilUser.Document.GetElementsByTagName("img");
							for (int i = 0; i < imgsPerfil.Count && imgPerfil == null; i++) {
								
								if (imgsPerfil[i].GetAttribute("class") == "profilepic") {
									uriImagenPerfil = new Uri(linksHomePage[i].GetAttribute("src"));
									imgPerfil=new Image();
									imgPerfil.SetImage(uriImagenPerfil);
								}
							}
						};
						
						while (imgPerfil == null)
							System.Threading.Thread.Sleep(250);
						
					}
					
				}
				return imgPerfil;
			}
		}

		public void LogOut()
		{
			System.Windows.Forms.HtmlElement htmlElementoSalir;
			try{
				RecargarPaginaWahack();
				htmlElementoSalir=htmlWahackHomePage.GetElementById("salir");
				htmlElementoSalir.InvokeMember("click");
				//recargo
				RecargarPaginaWahack();
			}catch{
				throw new Exception("No se ha hecho login!!");
				
			}
		}
		void RecargarPaginaWahack()
		{
			btnLogin.IsEnabled=false;
			wbWahack.Navigate(WEB);
			wbWahack.DocumentCompleted += (s, pagina) => {
				Action act;
				htmlWahackHomePage = wbWahack.Document;
				
				act=()=>btnLogin.IsEnabled = true;
				Dispatcher.BeginInvoke(act);
			};
			while(!btnLogin.IsEnabled&&IsVisible)
				System.Threading.Thread.Sleep(250);
		}
		void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.HtmlDocument htmlLogin;
			System.Windows.Forms.HtmlElementCollection inputs;
			System.Windows.Forms.HtmlElement elementoSubmit=null;
			htmlLogin=htmlWahackHomePage;
			if (!String.IsNullOrEmpty(tbxUsuario.Text) && !String.IsNullOrEmpty(pbxPassword.Password)) {
				User = tbxUsuario.Text;
		
				htmlLogin.GetElementById("navbar_username").InnerText=User;
				htmlLogin.GetElementById("navbar_password").InnerText=pbxPassword.Password;
				inputs=htmlLogin.GetElementsByTagName("input");
				for(int i=0;i<inputs.Count&&elementoSubmit==null;i++)
					if(inputs[i].GetAttribute("value")=="Iniciar Sesión")
						elementoSubmit=inputs[i];
				elementoSubmit.InvokeMember("click");
				RecargarPaginaWahack();
				IsConnected=htmlWahackHomePage.GetElementsByTagName("body")[0].InnerHtml.Contains(User);
				if (IsConnected)
					this.Close();
				else
					MessageBox.Show("No se ha hecho login, mira si hay algun error en los campos y vuelve a intentarlo");
			} else {
				MessageBox.Show("Faltan campos por rellenar!");
				
			}
		}
	}
}