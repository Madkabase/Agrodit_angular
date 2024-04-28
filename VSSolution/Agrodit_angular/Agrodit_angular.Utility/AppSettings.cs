using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Utility
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int TokenValidityDay { get; set; }
        public string DefaultTokenUsername { get; set; }
        public string DefaultTokenPassword { get; set; }
    }
}

