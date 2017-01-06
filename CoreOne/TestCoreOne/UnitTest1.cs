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
            string email = "kuti1410@yahoo.com.vn";
            //string email = "toantran222@aperia.vn";
            //string email = "toan.tran@d2cgroup.com";
            //string email = "ash_foster@hotmail.com";
            var validator = new Codicode.EmailValidator();
            validator.Mail_From = "admin@monoprog.com";
            string msg = validator.Check_MailBox_Error(email);
            //bool result = validator.Check_MailBox(email);
            //result = validator.Check_SMTP(email);
            //validator.Dispose();

            //string filePath = @"D:\emails.txt";☺

            //int counter = 0;
            //string line;

            //// Read the file and display it line by line.
            //var fileReader = new StreamReader(filePath);
            //string msg = string.Empty;
            //var builder = new StringBuilder();
            //while ((line = fileReader.ReadLine()) != null)╟
            //{
            //    //Console.WriteLine(line);
            //    counter++;
            //    if (string.IsNullOrWhiteSpace(line) == false)
            //    {
            //        msg = btnCheck_Click(line.Trim());
            //        builder.AppendLine(msg + " " + line);
            //    }
            //}

            //fileReader.Close();

            //File.WriteAllText("D:\\result.txt", builder.ToString(), Encoding.UTF8);

            //var emails = new List<string>()
            //{
            //    "toan.tran@d2cgroup.com",
            //    "toantran@aperia.vn"
            //};



            //string msg = CheckMail(email, "smtp.mail.yahoo.com", 25);
            Assert.IsTrue(msg == "");
        }

        protected string CheckMail(string email, string smtp = "gmail-smtp-in.l.google.com", int port = 25)
        {

            //TcpClient tClient = new TcpClient(smtp, port);
            //string CRLF = "\r\n";
            //byte[] dataBuffer;
            //string ResponseString;
            //NetworkStream netStream = tClient.GetStream();
            //StreamReader reader = new StreamReader(netStream);
            //ResponseString = reader.ReadLine();
            ///* Perform HELO to SMTP Server and get Response */
            //dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
            //netStream.Write(dataBuffer, 0, dataBuffer.Length);
            //ResponseString = reader.ReadLine();
            //dataBuffer = BytesFromString("MAIL FROM:<test.email.greetratereal@gmail.com>" + CRLF);
            //netStream.Write(dataBuffer, 0, dataBuffer.Length);
            //ResponseString = reader.ReadLine();
            ///* Read Response of the RCPT TO Message to know from google if it exist or not */
            //dataBuffer = BytesFromString("RCPT TO:<" + email + ">" + CRLF);
            //netStream.Write(dataBuffer, 0, dataBuffer.Length);
            //ResponseString = reader.ReadLine();
            //string msg = "1";
            //if (GetResponseCode(ResponseString) == 550)
            //{
            //    //msg += ("Mai Address Does not Exist !<br/><br/>");
            //    //msg += ("<B><font color='red'>Original Error from Smtp Server :</font></b>" + ResponseString);
            //    msg = "0";
            //}
            ///* QUITE CONNECTION */
            //dataBuffer = BytesFromString("QUITE" + CRLF);
            //netStream.Write(dataBuffer, 0, dataBuffer.Length);
            //tClient.Close();
            //return msg;

            // change cursor into wait cursor
            // create server SMTP with port 25
            TcpClient SmtpServ = new TcpClient(smtp, 25);
            string Data;
            byte[] szData;
            string CRLF = "\r\n";
            List<string> LogList = new List<string>();

            try
            {
                // initialization
                NetworkStream NetStrm = SmtpServ.GetStream();
                StreamReader RdStrm = new StreamReader(SmtpServ.GetStream());
                LogList.Add(RdStrm.ReadLine());

                // say hello to server and send response into log report
                Data = "HELLO server " + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);
                LogList.Add(RdStrm.ReadLine());
                // send sender data
                Data = "MAIL FROM: " + "<test.email.greetratereal@gmail.com>" + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);
                LogList.Add(RdStrm.ReadLine());

                // send receiver data
                Data = "RCPT TO: " + "<" + email + ">" + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);
                LogList.Add(RdStrm.ReadLine());

                // send DATA
                Data = "DATA " + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);
                LogList.Add(RdStrm.ReadLine());

                // send content data
                Data = "SUBJECT: Hello";
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);
                LogList.Add(RdStrm.ReadLine());

                // quit from server SMTP
                Data = "QUIT " + CRLF;
                szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
                NetStrm.Write(szData, 0, szData.Length);
                LogList.Add(RdStrm.ReadLine());

                // close connection
                NetStrm.Close();
                RdStrm.Close();
                LogList.Add("Close connection");
                LogList.Add("Send mail successly..");

                // back to normal cursor
                //Cursor.Current = cr;
                return LogList.ToString();
            }
            catch (InvalidOperationException err)
            {
                LogList.Add("Error: " + err.ToString());
                return err.ToString();
            }
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
