﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Network
{
    public class AccessFiles
    {
        private static int Attemptes { get => 2; }


        public static string ReadJsonFile(string path) => ReadJsonFile(path, null, null);
        public static string ReadJsonFile(string path, string serverPath = null, string username = null, string password = null)
        {
            int count = 0;
            bool failLoadWithoutCredenciais = false;
            string json = string.Empty;
            while (count < Attemptes)
            {
                try
                {
                    if (System.IO.File.Exists(path))
                        json = System.IO.File.ReadAllText(path);
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    failLoadWithoutCredenciais = true; 
                    count++;
                }
            }
            if (failLoadWithoutCredenciais)
            {
                count = 0;
                while (count < Attemptes)
                {
                    try
                    {
                        using (Network.NetworkShareAccesser.Access(serverPath, username, password))
                            if (System.IO.File.Exists(path))
                                json = System.IO.File.ReadAllText(path);
                            else
                                throw new Exception();
                    }
                    catch(Exception ex)
                    {
                        count++;
                    }   
                }
            }
            return json;
        }
    }
}
