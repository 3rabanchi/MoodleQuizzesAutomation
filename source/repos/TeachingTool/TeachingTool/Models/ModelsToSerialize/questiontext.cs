using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TeachingTool.Models.ModelsToSerialize
{
    public class questiontext
    {
        [XmlAttribute]
        public string format { get; set; }

        [XmlIgnore]
        public string text { get; set; }

        [XmlElement("text")]
        public XmlCDataSection textCData
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(text);
            }
            set
            {
                text = value.Value;
            }
        }
    }
}
