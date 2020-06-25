using System;
using System.Xml;

namespace WriteReadClassLib
{
    public static class XMLReader
    {
        public static UserConfig Read(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);

            // Получим корневой элемент.
            XmlElement xRoot = xDoc.DocumentElement;

            UserConfig user = new UserConfig();
            
            // Обход всех узлов в корневом элементе.
            foreach (XmlNode xnode in xRoot)
            {
                // Обходим все дочерние узлы элемента UserConfig.
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    switch (childnode.Name)
                    {
                        case "FileProcessingThreadsCount":
                            try
                            {
                                user.FileProcessingThreadsCount = int.Parse(childnode.InnerText);
                            }
                            catch (FormatException e) 
                            { 
                                throw e; 
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return user;
        }
    }
}
