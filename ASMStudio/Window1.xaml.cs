/*
 * Creado por SharpDevelop.
 * Usuario: tetra
 * Fecha: 12/05/2017
 * Hora: 2:52
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
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		ConfiguradorDeCaminos configuradorPath;
		Main main;
		public Window1()
		{
			bool mostrarConfiguracion=true;//!System.IO.File.Exists(ConfiguradorDeCaminos.PathArchivoConfiguracion);
			main=new Main();
			main.AbrirConfiguracion+=(s,e)=>	gMain.Children.Add(configuradorPath);
			configuradorPath=new ConfiguradorDeCaminos();
			configuradorPath.Fin+=(s,e)=>{
				gMain.Children.Remove(configuradorPath);
			};
			InitializeComponent();
			
		    gMain.Children.Add(main);
			if(mostrarConfiguracion)
			{
				//Se tiene que configurar
				gMain.Children.Add(configuradorPath);
				
			}
			
			
		}
	}
}