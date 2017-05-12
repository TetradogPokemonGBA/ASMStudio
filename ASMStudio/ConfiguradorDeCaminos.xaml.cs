/*
 * Creado por SharpDevelop.
 * Usuario: tetra
 * Fecha: 05/12/2017
 * Hora: 03:05
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
	/// Interaction logic for ConfiguradorDeCaminos.xaml
	/// </summary>
	public partial class ConfiguradorDeCaminos : UserControl
	{
		
		UserControl[] pasos;
		int pasoActual;
		public event EventHandler Fin;
		public ConfiguradorDeCaminos()
		{
			pasoActual=-1;
			InitializeComponent();
			//pongo los pasos y los configuro
			PonSiguientePaso();
		}
		void BtnContinuar_Click(object sender, RoutedEventArgs e)
		{
			PonSiguientePaso();
		}

		void PonSiguientePaso()
		{
			pasoActual++;
			if(pasoActual<pasos.Length){
			gControlActual.Children.Clear();
			gControlActual.Children.Add(pasos[pasoActual]);}else{
				if(Fin!=null)
					Fin(new object(),new EventArgs());
			}
		}
	}
}