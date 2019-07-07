using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TeachingTool.Models.ModelsToSerialize
{
    public class question
    {
        public name name { get; set; }
        public questiontext questiontext { get; set; }
        public generalfeedback generalfeedback { get; set; }
        public string penalty { get; set; }
        public string hidden { get; set; }
        public string idnumber { get; set; }

        [XmlAttribute]
        public string type { get; set; }
    }
}
