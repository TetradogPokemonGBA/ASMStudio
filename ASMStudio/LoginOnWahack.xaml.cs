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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
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
		const string ENCONTRARNOMBREUSUARIO = "/u-";
		const string ENCONTRARIMGPERFIL = "profilepic";
		
		
		bool cargada;
		Uri perfilUser;
		Image imgPerfil;
		
		public LoginOnWahack()
		{
			InitializeComponent();
			wbLogin.DocumentCompleted += PaginaCargada;
			wbLogin.ScriptErrorsSuppressed = true;
			wbLogin.Navigate(WEB);
			this.WindowState = WindowState.Minimized;
		}

		public bool IsConnected {
			get;
			private	set;
		}

		public string User {
			get {
				System.Windows.Forms.HtmlElementCollection linksCollection;
				if (perfilUser == null && IsConnected) {
					linksCollection = wbLogin.Document.GetElementById("blackbar").GetElementsByTagName("a");
					for (int i = 0; i < linksCollection.Count && perfilUser == null; i++)
						if (linksCollection[i].GetAttribute("href").Contains(ENCONTRARNOMBREUSUARIO))
							perfilUser = new Uri(new Uri(WEB), linksCollection[i].GetAttribute("href"));
					
				}
				return perfilUser.Segments[perfilUser.Segments.Length - 1];
			}
			
		}
		
		public Image ImgPerfil {
			get {
				WebClient wcPerfil;
				System.Windows.Forms.HtmlDocument htmlDoc;
				System.Windows.Forms.HtmlElementCollection linksCollection;
				string paginaPerfil;
				if (imgPerfil == null && User != null) {
					wcPerfil = new WebClient();
					paginaPerfil = wcPerfil.DownloadString(perfilUser);
					htmlDoc = GetHtmlDocument(paginaPerfil);
					linksCollection = htmlDoc.GetElementById("collapseobj_stats_mini").GetElementsByTagName("img");
					for (int i = 0; i < linksCollection.Count && imgPerfil == null; i++)
						if (linksCollection[i].OuterHtml.Contains(ENCONTRARIMGPERFIL)) {
							imgPerfil = new Image();
							imgPerfil.SetImage(new Uri(linksCollection[i].GetAttribute("href")));
						}
					
				}
				return imgPerfil;
			}
		}

		public System.Windows.Forms.HtmlDocument GetHtmlDocument(string html)
		{//sacado de http://stackoverflow.com/questions/4935446/string-to-htmldocument
			System.Windows.Forms.WebBrowser browser = new System.Windows.Forms.WebBrowser();
			browser.ScriptErrorsSuppressed = true;
			browser.DocumentText = html;
			browser.Document.OpenNew(true);
			browser.Document.Write(html);
			browser.Refresh();
			return browser.Document;
		}
		

		void PaginaCargada(Object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
		{
			IsConnected = wbLogin.Document.GetElementById("memberbar").OuterHtml.Contains("salir");
			try {
				if (IsConnected)
					this.Close();
				else
					this.WindowState = WindowState.Normal;
			} catch {
			} finally {
				cargada = true;
			}
		}

	}
}