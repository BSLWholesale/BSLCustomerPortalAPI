﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace BSLCustomerPortalAPI.Models
{
    public class Generic
    {
        private const string SecurityKey = "ComplexKeyHere_12121";
        public static string EncryptText(string PlainText)
        {
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            objTripleDESCryptoService.Key = securityKeyArray;
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            objTripleDESCryptoService.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string DecryptText(string CipherText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(CipherText);
            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            objTripleDESCryptoService.Key = securityKeyArray;
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            objTripleDESCryptoService.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public bool TriggerEmailNoAttachment(string html, string subject, string toEmail, string strEC)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(toEmail);
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"]);
                mailMessage.CC.Add(ConfigurationManager.AppSettings["Email"]);
                mailMessage.Subject = subject;
                mailMessage.Body = html + "<br/>" + strEC;
                mailMessage.BodyEncoding = Encoding.GetEncoding("utf-8");
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = ConfigurationManager.AppSettings["Server"];
                smtpClient.EnableSsl = true;
                NetworkCredential credential = new NetworkCredential();
                credential.UserName = ConfigurationManager.AppSettings["Email"];
                credential.Password = ConfigurationManager.AppSettings["EPwd"];
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credential;
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
    }


    public static class Logger
    {
        public static void WriteLog(string FunctionName, string message, StackTrace stStrack)
        {
            string LogPath = ConfigurationManager.AppSettings["logPath"] + System.DateTime.Today.ToString("dd-MM-yyyy") + "." + "txt";
            FileInfo LogFileInfo = new FileInfo(LogPath);
            DirectoryInfo LogDirInfo = new DirectoryInfo(LogFileInfo.DirectoryName);
            if (!LogDirInfo.Exists) LogDirInfo.Create();
            using (FileStream fileStream = new FileStream(LogPath, FileMode.Append))
            {
                using (StreamWriter log = new StreamWriter(fileStream))
                {
                    StackFrame frame = null;
                    int LineNumber = 0;
                    for (int i = 0; i < stStrack.FrameCount; i++)
                    {
                        frame = stStrack.GetFrame(i);
                        if (frame.GetFileLineNumber() > 0)
                        {
                            LineNumber = frame.GetFileLineNumber();
                            break;
                        }
                    }
                    log.WriteLine($"{DateTime.Now} : {FunctionName} {LineNumber} {message}");
                }
            }
        }
    }
}