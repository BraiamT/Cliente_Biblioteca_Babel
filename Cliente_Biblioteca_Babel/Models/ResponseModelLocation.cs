﻿using Cliente_Biblioteca_Babel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliente_Biblioteca_Babel.Models
{
    public class ResponseModelLocation
    {
        public int resultado { get; set; }
        public List<LocalizacionModel> data { get; set; }
        public string mensaje { get; set; }
    }
}