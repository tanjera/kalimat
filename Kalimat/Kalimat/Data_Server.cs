using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Kalimat
{
    class Data_Server
    {
        static string ScriptURL_Login = "http://www.tanjera.com/kalimat_scripts/login.php";
        static string ScriptURL_GetLanguages = "http://www.tanjera.com/kalimat_scripts/get-languages.php";
        static string ScriptURL_GetStackList = "http://www.tanjera.com/kalimat_scripts/get-stacklist.php";
        static string ScriptURL_GetStack = "http://www.tanjera.com/kalimat_scripts/get-stack.php";

        public bool Login (string Username, string Password)
        {
            string reqURL = String.Format("{0}?User={1}&Pass={2}&Act=Login", ScriptURL_Login, Username, Password);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string reqResponse = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            switch (reqResponse)
            {
                default:
                case "invalid":
                    return false;

                case "verified":
                    return true;
            }
        }

        public bool Register(string Username, string Password)
        {
            string reqURL = String.Format("{0}?User={1}&Pass={2}&Act=Register", ScriptURL_Login, Username, Password);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string reqResponse = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            switch (reqResponse)
            {
                default:
                case "username_taken":
                    return false;

                case "registration_complete":
                    return true;
            }
        }

        public List<string> Get_Languages()
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(ScriptURL_GetLanguages);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

            List<string> outList = new List<string>();
            string eachLine;
            while (!sr.EndOfStream)
            {
                eachLine = sr.ReadLine();
                if (eachLine.Trim() != String.Empty)
                    if (!outList.Contains(eachLine))
                        outList.Add(eachLine);
            }

            sr.Close();
            myResponse.Close();

            return outList;
        }

        public List<Stack> Get_StackList(string incLanguage)
        {
            string reqURL = String.Format("{0}?Language={1}", ScriptURL_GetStackList, incLanguage);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

            List<Stack> outList = new List<Stack>();
            while (!sr.EndOfStream)
            {
                Stack eachStack = new Stack();
                eachStack.UID = sr.ReadLine();
                eachStack.Title = sr.ReadLine();
                eachStack.Description = sr.ReadLine();
                eachStack.Price_Points = int.Parse(sr.ReadLine());
                eachStack.Price_Dollars = double.Parse(sr.ReadLine());
                outList.Add(eachStack);
            }

            sr.Close();
            myResponse.Close();

            return outList;
        }

        public Stack Get_Stack(string incUID)
        {
            string reqURL = String.Format("{0}?UID={1}", ScriptURL_GetStack, incUID);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

            Data instData = new Data();
            Stack outStack = instData.StackFromXML(sr.ReadToEnd());

            sr.Close();
            myResponse.Close();

            return outStack;
        }
    }
}
