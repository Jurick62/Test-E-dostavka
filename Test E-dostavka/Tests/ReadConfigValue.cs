using System.IO;
using System.Xml;
using Microsoft.VisualBasic.FileIO;
using ExcelDataReader;

namespace Test_E_dostavka.Tests
{
    class ReadConfigValue
    {
        public string searchPath = "D:/Jurick/Coding/TestProject/Test E-dostavka/Test E-dostavka/Tests/Constants";
        public string[] exstension = new string[3] { ".csv", ".xml", ".xlsx" };
        public string tel = "", pass = "", fio = "", edostavkaURL = "",browserName = "", driverPath = "";
        public int timeWait = 0;

        public bool FindeConfigFile()
        {
            foreach(string fileType in exstension)
            {
                switch (fileType)
                {
                    case ".csv":
                        FileInfo filePathCSV = new FileInfo(searchPath + fileType);
                        if (filePathCSV.Exists)
                        {
                            searchPath = searchPath + fileType;
                            GetValueFromCSV(searchPath);
                            return true;
                        }
                        break;

                    case ".xml":
                        FileInfo filePathXML = new FileInfo(searchPath + fileType);
                        if (filePathXML.Exists)
                        {
                            searchPath = searchPath + fileType;
                            GetValueFromXML(searchPath);
                            return true;
                        }
                        break;

                    case ".xlsx":
                        FileInfo filePathXLSX = new FileInfo(searchPath + fileType);
                        if (filePathXLSX.Exists)
                        {
                            searchPath = searchPath + fileType;
                            GetValueFromXLSX(searchPath);
                            return true;
                        }
                        break;
                }
            }
            return true;
        }

        private void GetValueFromCSV(string searchPath)
        {
            using (TextFieldParser csvParser = new TextFieldParser(searchPath))
            {
                csvParser.SetDelimiters(new string[] { ";" });
                csvParser.HasFieldsEnclosedInQuotes = false;
                csvParser.ReadLine();

                string[] fields = csvParser.ReadFields();
                tel = fields[0];
                pass  = fields[1];
                fio  = fields[2];
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
                    this.tel = reader.GetString(0);
                    this.pass = reader.GetString(1);
                    this.fio = reader.GetString(2);
                    this.fio = fio.Replace("\\r", "\r");
                    this.fio = fio.Replace("\\n", "\n");
                    this.edostavkaURL = reader.GetString(3);
                    string wait = reader.GetString(4);
                    this.timeWait = int.Parse(wait);
                    this.browserName = reader.GetString(5);
                    this.driverPath = reader.GetString(6);
                }
            }
        }
    }
}
