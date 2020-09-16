using System;
using System.IO;
using System.Xml;
using Microsoft.VisualBasic.FileIO;
using ExcelDataReader;

namespace Test_E_dostavka.Tests
{
    class ReadConfigValue
    {
        public string searchPath = Environment.CurrentDirectory + "\\Constants\\Constants";
        public string[] exstensions = new string[] { ".csv", ".xml", ".xlsx" };
        public string tel = "";
        public string pass = "";
        public string fio = "";
        public string edostavkaURL = "";
        public string browserName = "";
        public string driverPath = "";
        public int timeWait = 0;

        public int FindeConfigFile()
        {
            foreach(string fileType in exstensions)
            {
                switch (fileType)
                {
                    case ".csv":
                        FileInfo filePathCSV = new FileInfo(searchPath + fileType);
                        if (filePathCSV.Exists)
                        {
                            searchPath += fileType;
                            GetValueFromCSV(searchPath);
                        }
                        break;

                    case ".xml":
                        FileInfo filePathXML = new FileInfo(searchPath + fileType);
                        if (filePathXML.Exists)
                        {
                            searchPath += fileType;
                            GetValueFromXML(searchPath);
                        }
                        break;

                    case ".xlsx":
                        FileInfo filePathXLSX = new FileInfo(searchPath + fileType);
                        if (filePathXLSX.Exists)
                        {
                            searchPath += fileType;
                            GetValueFromXLSX(searchPath);
                        }
                        break;
                }
            }
            return 1;
        }

        private void GetValueFromCSV(string searchPath)
        {
            using (TextFieldParser csvParser = new TextFieldParser(searchPath))
            {
                csvParser.SetDelimiters(";");
                csvParser.HasFieldsEnclosedInQuotes = false;
                csvParser.ReadLine();

                string[] fields = csvParser.ReadFields();
                tel = fields[0];
                pass = fields[1];
                fio = fields[2];
                fio = fio.Replace("\\r", "\r");
                fio = fio.Replace("\\n", "\n");
                edostavkaURL = fields[3];
                timeWait = int.Parse(fields[4]);
                browserName = fields[5];
                driverPath = fields[6];
            }
        }

        private void GetValueFromXML(string searchPath)
        {
            XmlDocument xConst = new XmlDocument();
            xConst.Load(searchPath);

            foreach (XmlNode constPair in xConst.DocumentElement.ChildNodes)
            {
                switch (constPair.Name)
                {
                    case "tel":
                        tel = constPair.InnerText;
                        break;

                    case "pass":
                        pass = constPair.InnerText;
                        break;

                    case "fio":
                        fio = constPair.InnerText;
                        fio = fio.Replace("\\r", "\r");
                        fio = fio.Replace("\\n", "\n");
                        break;

                    case "url":
                        edostavkaURL = constPair.InnerText;
                        break;

                    case "wait":
                        timeWait = int.Parse(constPair.InnerText);
                        break;

                    case "browser":
                        browserName = constPair.InnerText;
                        break;

                    case "path":
                        driverPath = constPair.InnerText;
                        break;
                }
            }
        }

        private void GetValueFromXLSX(string searchPath)
        {
            using (var stream = File.Open(searchPath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();
                    reader.Read();
                    tel = reader.GetString(0);
                    pass = reader.GetString(1);
                    fio = reader.GetString(2);
                    fio = fio.Replace("\\r", "\r");
                    fio = fio.Replace("\\n", "\n");
                    edostavkaURL = reader.GetString(3);
                    string wait = reader.GetString(4);
                    timeWait = int.Parse(wait);
                    browserName = reader.GetString(5);
                    driverPath = reader.GetString(6);
                }
            }
        }
    }
}
