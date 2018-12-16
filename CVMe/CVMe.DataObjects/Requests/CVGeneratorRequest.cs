using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMe.DataObjects.Requests
{
    public class CVGeneratorRequest
    {
        public string Name { get; set; }

        public string Xml { get; set; }

        public string TemplateName { get; set; }
    }
}
