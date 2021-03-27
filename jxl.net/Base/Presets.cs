using jxlNET.Encoder.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace jxlNET
{
    public static class Presets
    {

        public static void Save(string FileName, List<Parameter> Parameter)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Parameter>));
                TextWriter writer = new StreamWriter(FileName);

                serializer.Serialize(writer, Parameter);
                writer.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine("Error saving Preset: " + FileName + Environment.NewLine + error.ToString());
            }
        }

        public static void Save(string FileName, List<Parameter> Parameter, string NameSpace)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Parameter>), NameSpace);
                TextWriter writer = new StreamWriter(FileName);

                serializer.Serialize(writer, Parameter);
                writer.Close();
            }
            catch(Exception error)
            {
                Console.WriteLine("Error saving Preset: " + FileName + Environment.NewLine + error.ToString());
            }
        }


        public static List<Parameter> Load(string FileName)
        {
            List<Parameter> result = new List<Parameter>();

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Parameter>));
                /* If the XML document has been altered with unknown
                nodes or attributes, handle them with the
                UnknownNode and UnknownAttribute events.*/
                serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

                FileStream fs = new FileStream(FileName, FileMode.Open);

                List<Parameter> loadedData;

                loadedData = (List<Parameter>)serializer.Deserialize(fs);

                if (loadedData != null) result = loadedData;

                foreach (var p in loadedData)
                {
                    Console.WriteLine("Param: " + p.ToString() + " Type: " + p.GetType());
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error loading Preset: " + FileName + Environment.NewLine + error.ToString());
            }

            return result;
        }

        public static List<Parameter> Load(string FileName, string NameSpace)
        {
            List<Parameter> result = new List<Parameter>();

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Parameter>), NameSpace);
                /* If the XML document has been altered with unknown
                nodes or attributes, handle them with the
                UnknownNode and UnknownAttribute events.*/
                serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

                FileStream fs = new FileStream(FileName, FileMode.Open);

                List<Parameter> loadedData;

                loadedData = (List<Parameter>)serializer.Deserialize(fs);

                if (loadedData != null) result = loadedData;

                foreach (var p in loadedData)
                {
                    Console.WriteLine("Param: " + p.ToString() + " Type: " + p.GetType());
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error loading Preset: " + FileName + Environment.NewLine + error.ToString());
            }

            return result;
        }
            

        private static void serializer_UnknownNode (object sender, XmlNodeEventArgs e)
        {
            if (e.Name == "xsi:type") {}
            else
                Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        private static void serializer_UnknownAttribute (object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            if (attr.Name == "xsi:type") { }
            else
                Console.WriteLine("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
        }


    }
}
