﻿using System;
using System.Collections.Generic;
using System.Text;
using U_System.Api.Plugin;

namespace MeioMundo.Ferramentas
{
    public class WelcomePage : IPlugin
    {
        public string Name { get => "Meio Mundo"; }
        public string Description { get => "App"; }

        public PluginType PluginType => PluginType.Window;
        public Type Type => typeof(TestWindows);

        public static string GetInfo() => "It Work";
    }
}
