using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace TestCoreOne
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEmail()
        {
            //string email = "toantran222@aperia.vn";
            string email = "toan.tran@d2cgroup.com";
            //var validator = new Codicode.EmailValidator();
            //validator.Mail_From = "admin@monoprog.com";
            //string msg = validator.Check_MailBox_Error(email);
            //bool result = validator.Check_MailBox(email);
            //result = validator.Check_SMTP(email);
            //validator.Dispose();

            var emails = new List<string>()
            {
                "toan.tran@d2cgroup.com",
                "toantran@aperia.vn"
            };

            string msg = btnCheck_Click(email);
            Assert.IsTrue(msg == "");
        }

        protected string btnCheck_Click(string email)
        {
            TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
            string CRLF = "\r\n";
            byte[] dataBuffer;
            string ResponseString;
            NetworkStream netStream = tClient.GetStream();
            StreamReader reader = new StreamReader(netStream);
            ResponseString = reader.ReadLine();
            /* Perform HELO to SMTP Server and get Response */
            dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            dataBuffer = BytesFromString("MAIL FROM:<admin@monoprog.com>" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            /* Read Response of the RCPT TO Message to know from google if it exist or not */
            dataBuffer = BytesFromString("RCPT TO:<" + email + ">" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            string msg = "";
            if (GetResponseCode(ResponseString) == 550)
            {
                msg += ("Mai Address Does not Exist !<br/><br/>");
                msg += ("<B><font color='red'>Original Error from Smtp Server :</font></b>" + ResponseString);
            }
            /* QUITE CONNECTION */
            dataBuffer = BytesFromString("QUITE" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            tClient.Close();
            return msg;
        }

        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }
    }
}
