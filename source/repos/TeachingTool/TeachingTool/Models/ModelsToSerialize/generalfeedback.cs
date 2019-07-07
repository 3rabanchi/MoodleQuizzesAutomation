using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TeachingTool.Models.ModelsToSerialize
{
    public class generalfeedback
    {
        [XmlAttribute]
        public string format { get; set; }

        public string text { get; set; }
    }
}
