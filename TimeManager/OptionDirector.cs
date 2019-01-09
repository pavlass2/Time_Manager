using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TimeManager
{
    public class OptionDirector
    {
        private string path = "options.xml";
        public Option option;

        public OptionDirector()
        {
            Load();
        }

        /// <summary>
        /// Save options
        /// </summary>
        /// <param name="option"></param>
        public void Save(Option option)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter xw = XmlWriter.Create(path, settings))
            {
                xw.WriteStartDocument();
                xw.WriteStartElement("option");
                xw.WriteAttributeString("eventLength", option.EventLength.ToString());
                xw.WriteAttributeString("breakLength", option.BreakLength.ToString());
                xw.WriteEndElement();
            }
            Load();
        }

        /// <summary>
        /// Load options
        /// </summary>
        public void Load()
        {
            option = new Option();
            if (File.Exists(path))
            {
                using (XmlReader xr = XmlReader.Create(path))
                {
                    int eventLength = 0;
                    int breakLength = 0;
                    string element = "";

                    while (xr.Read())
                    {
                        // načítáme element
                        if (xr.NodeType == XmlNodeType.Element)
                        {
                            element = xr.Name; // název aktuálního elementu
                            if (element == "option")
                            {
                                eventLength = int.Parse(xr.GetAttribute("eventLength"));
                                breakLength = int.Parse(xr.GetAttribute("breakLength"));
                            }
                        }
                    }
                    option = new Option(eventLength, breakLength);
                }
            }
            else option = new Option(30, 10);
        }
    }
}
