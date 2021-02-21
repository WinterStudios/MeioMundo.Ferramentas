﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Escola
{
    public class ManuaisSystem
    {
        //public static ObservableCollection<Internal.Escola> Escolas { get; set; }
        //public static ObservableCollection<Internal.Livro> Livros { get; set; }

        public static List<Internal.Escola> Escolas { get; set; }
        public static List<Internal.Livro> Livros { get; set; }

        private static string DataLocationFolder { get {
                string pluginAssemblyDirectory = AppDomain.CurrentDomain.BaseDirectory.Replace("\\","/");
                string pluginDataDirectory = pluginAssemblyDirectory;
                if (!System.IO.Directory.Exists(pluginDataDirectory + "/Data/MeioMundo.Ferramentas/Escola"))
                    System.IO.Directory.CreateDirectory(pluginDataDirectory + "/Data/MeioMundo.Ferramentas/Escola");
                return pluginDataDirectory + "/Data/MeioMundo.Ferramentas/Escola/";
            } }

        public static void Inicialize()
        {
            //Escolas = new ObservableCollection<Internal.Escola>();
            //Livros = new ObservableCollection<Internal.Livro>();
            LoadLivros();
            LoadEscolas();
        }

        #region Escolas

        //public static Internal.Escola AddEscola()
        //{
        //    Internal.Escola escola = new Internal.Escola();
        //    escola.Name = "Escola ...";
        //    escola.ID = Escolas.Count;
        //    escola.Anos = new List<Internal.Ano>();

        //    Escolas.Add(escola);
        //    return escola;
        //}

        public static void LoadEscolas()
        {
            string escolaFile = DataLocationFolder + "Escolas.json";
            if (System.IO.File.Exists(escolaFile))
            {
                string json = System.IO.File.ReadAllText(escolaFile);
                //Escolas = new ObservableCollection<Internal.Escola>(System.Text.Json.JsonSerializer.Deserialize<Internal.Escola[]>(json).ToList());
            }

        }
        public static void SaveEscola()
        {
            string escolaFile = DataLocationFolder + "Escolas.json";
            System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions() { WriteIndented = true };
            //string json = System.Text.Json.JsonSerializer.Serialize(Escolas.ToArray(), options);
            //System.IO.File.WriteAllText(escolaFile, json);
        }




        #endregion

        #region Livros
        public static void LoadLivros()
        {
            string livrosFile = DataLocationFolder + "Livros.json";
            if (System.IO.File.Exists(livrosFile))
            {
                string json = System.IO.File.ReadAllText(livrosFile);
                Livros = System.Text.Json.JsonSerializer.Deserialize<Internal.Livro[]>(json).ToList();
            }
        }

        #endregion
    }
}