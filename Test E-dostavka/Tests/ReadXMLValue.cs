using System.Xml;

namespace Test_E_dostavka.Tests
{
    class ReadXMLValue
    {
        const string SEARCH_PATH = "D:/Jurick/Coding/TestProject/Test E-dostavka/Test E-dostavka/Tests/Constants.xml";
        public string tel = "";
        public string pass = "";
        public string fio = "";
        public string edostavkaURL = "";
        public int timeWait = 0;

        public void GetValueFromXML()
        {
            XmlDocument xConst = new XmlDocument();
            xConst.Load(SEARCH_PATH);

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
                }

            }

        }
    }
}
