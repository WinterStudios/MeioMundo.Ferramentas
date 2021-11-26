using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Network
{
    public class AccessFiles
    {
        private static int Attemptes { get => 2; }
        private static string ServerPath { get => @"srvmm"; }


        public static string ReadJsonFile(string path, string username = null, string password = null)
        {
            string json = "";
            try
            {
                if (System.IO.File.Exists(path))
                    json = System.IO.File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                return ReadJsonFileCredencials(path, ServerPath, username, password);
            }
            return json;
        }
        public static FileStream ReadFile(string path, string username = null, string password = null)
        {
            FileStream fs = null;
            try
            {
                if (System.IO.File.Exists(path))
                    fs = System.IO.File.Open(path, FileMode.Open);
                else
                    throw new Exception("Not Found");
            }
            catch (Exception ex)
            {
                return ReadFileCredencials(path, ServerPath, username, password);
            }
            return fs;
        }

        private static FileStream ReadFileCredencials(string path, string serverPath, string username, string password)
        {
            try
            {


                using (Network.NetworkShareAccesser.Access(serverPath, username, password))
                    if (System.IO.File.Exists(path))
                        return System.IO.File.Open(path, FileMode.Open);
                    else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

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
                        return System.IO.File.ReadAllText(path);
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
                                return System.IO.File.ReadAllText(path);
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

        private static string ReadJsonFileCredencials(string path, string serverPath, string username, string password)
        {
            using (Network.NetworkShareAccesser.Access(serverPath, username, password))
                if (System.IO.File.Exists(path))
                    return System.IO.File.ReadAllText(path);
                else return "";
        }

    }
}
