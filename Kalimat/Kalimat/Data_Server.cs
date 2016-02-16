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
    }
}
