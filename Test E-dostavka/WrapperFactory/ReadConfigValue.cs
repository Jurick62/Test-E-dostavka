using System;
using System.IO;
using System.Xml;
using Microsoft.VisualBasic.FileIO;
using ExcelDataReader;

namespace Test_E_dostavka.Tests
{
    class ReadConfigValue
    {
        public int TimeWait { get; set; }
        public string Tel { get; private set; }
        public string Pass { get; private set; }
        public string Fio { get; private set; }
        public string EdostavkaURL { get; set; }
        public string BrowserName { get; set; }

        public string SearchPath
        {
            get
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                return path.Substring(0, path.IndexOf("bin")) + "Constants\\Constants";
            }
        }

        public string[] Exstensions
        {
            get
            {
                return new string[] { ".csv", ".xml", ".xlsx" };
            }
        }

        public void FindeConfigFile()
        {
            foreach(string fileType in Exstensions)
            {
                string searchPath = SearchPath + fileType;
                FileInfo filePath = new FileInfo(searchPath);
                switch (fileType)
                {
                    case ".csv":
                        ;
                        if (filePath.Exists)
                        {
                            GetValueFromCSV(searchPath);
                        }
                        break;

                    case ".xml":
                        if (filePath.Exists)
                        {
                            GetValueFromXML(searchPath);
                        }
                        break;

                    case ".xlsx":
                        if (filePath.Exists)
                        {
                            GetValueFromXLSX(searchPath);
                        }
                        break;
                }
                break;
            }
        }

        private void GetValueFromCSV(string SearchPath)
        {
            using (TextFieldParser csvParser = new TextFieldParser(SearchPath))
            {
                csvParser.SetDelimiters(";");
                csvParser.HasFieldsEnclosedInQuotes = false;
                csvParser.ReadLine();

                string[] fields = csvParser.ReadFields();
                Tel = fields[0];
                Pass = fields[1];
                Fio = fields[2];
                Fio = Fio.Replace("\\r", "\r");
                Fio = Fio.Replace("\\n", "\n");
                EdostavkaURL = fields[3];
                TimeWait = int.Parse(fields[4]);
                BrowserName = fields[5];
            }
        }

        private void GetValueFromXML(string SearchPath)
        {
            XmlDocument xConst = new XmlDocument();
            xConst.Load(SearchPath);

            foreach (XmlNode constPair in xConst.DocumentElement.ChildNodes)
            {
                switch (constPair.Name)
                {
                    case "tel":
                        Tel = constPair.InnerText;
                        break;

                    case "pass":
                        Pass = constPair.InnerText;
                        break;

                    case "fio":
                        Fio = constPair.InnerText;
                        Fio = Fio.Replace("\\r", "\r");
                        Fio = Fio.Replace("\\n", "\n");
                        break;

                    case "url":
                        EdostavkaURL = constPair.InnerText;
                        break;

                    case "wait":
                        TimeWait = int.Parse(constPair.InnerText);
                        break;

                    case "browser":
                        BrowserName = constPair.InnerText;
                        break;
                }
            }
        }

        private void GetValueFromXLSX(string SearchPath)
        {
            using (FileStream stream = File.Open(SearchPath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();
                    reader.Read();
                    Tel = reader.GetString(0);
                    Pass = reader.GetString(1);
                    Fio = reader.GetString(2);
                    Fio = Fio.Replace("\\r", "\r");
                    Fio = Fio.Replace("\\n", "\n");
                    EdostavkaURL = reader.GetString(3);
                    string wait = reader.GetString(4);
                    TimeWait = int.Parse(wait);
                    BrowserName = reader.GetString(5);
                }
            }
        }
    }
}
