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
using System.IO;
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
		public static readonly string PathArchivoConfiguracion=Environment.CurrentDirectory+System.IO.Path.AltDirectorySeparatorChar+"ASMStudio.config";
		
		ConfigurarPaso[] pasos;
		int pasoActual;
		public event EventHandler Fin;
		
		public ConfiguradorDeCaminos()
		{
			string textoPaso1="Selecciona la carpeta donde tienes las roms de pokémon.";
			string textoPaso2="Selecciona la carpeta donde tienes pensado tener el código asm hecho.";
			
			pasoActual=-1;
			InitializeComponent();
			//pongo los pasos y los configuro
			pasos=new ConfigurarPaso[]{new ConfigurarPaso(textoPaso1,ASMStudio.Resources.GbaFolder,GetActualPathGBA()),new ConfigurarPaso(textoPaso2,ASMStudio.Resources.AsmFolder,GetActualPathASM())};
			PonSiguientePaso();
		}
		void BtnContinuar_Click(object sender, RoutedEventArgs e)
		{
			PonSiguientePaso();
		}

		void PonSiguientePaso()
		{
			switch(pasoActual)
			{
				case 0://gba path
					SetActualGBAPath(pasos[0].SelectedPath);
					break;
				case 1://asm path
					SetActualASMPath(pasos[1].SelectedPath);
					break;
			}
			pasoActual++;
			if(pasoActual<pasos.Length){
				gControlActual.Children.Clear();
				gControlActual.Children.Add(pasos[pasoActual]);}else{
				if(Fin!=null)
					Fin(new object(),new EventArgs());
			}
		}

		public static string GetActualPathGBA()
		{
			return GetCommun("GBA",0);
		}
		static string GetCommun(string defaultPath,int pos)
		{
					string path;
					bool aux;
			if(!File.Exists(PathArchivoConfiguracion))
			{
				CreateFileConfig();
			}
			
			try{
				path= File.ReadAllLines(PathArchivoConfiguracion)[pos].Split('=')[1];
		     	aux= new DirectoryInfo(path).Exists;
			}catch{
				path=defaultPath;
			}
			return path;
		}

		public static string GetActualPathASM()
		{
			return GetCommun("ASM",1);
		}

		public static void SetActualGBAPath(string actualPathGBA)
		{
			if(!File.Exists(PathArchivoConfiguracion))
			{
				CreateFileConfig();
			}
			if(actualPathGBA.Contains(Environment.CurrentDirectory))
				actualPathGBA=actualPathGBA.Remove(0,Environment.CurrentDirectory.Length);//asi es relativo
			File.WriteAllLines(PathArchivoConfiguracion,new string[]{"GBA="+actualPathGBA,"ASM="+GetActualPathASM()});
		}

		public static void SetActualASMPath(string actualPathASM)
		{
			if(!File.Exists(PathArchivoConfiguracion))
			{
				CreateFileConfig();
			}
			
			if(actualPathASM.Contains(Environment.CurrentDirectory))
				actualPathASM=actualPathASM.Remove(0,Environment.CurrentDirectory.Length);//asi es relativo
			File.WriteAllLines(PathArchivoConfiguracion,new string[]{"GBA="+GetActualPathGBA(),"ASM="+actualPathASM});
		}

		static void CreateFileConfig()
		{
			File.WriteAllLines(PathArchivoConfiguracion,new string[]{"GBA=GBA","ASM=ASM"});
		}
	}
}