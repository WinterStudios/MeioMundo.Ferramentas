﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.ViewModels
{
    public class Setting
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public SettingType _Type { get; set; }
    }
}
