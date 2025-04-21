using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using project.data.Abstract;
using project.entity;

namespace project.webapp.Filters
{
    public class MailHelper : IMailHelper
    {
        public readonly ISMTPRepository _smtpRepository;
        public MailHelper(ISMTPRepository smtpRepository){
            _smtpRepository = smtpRepository;
        }

        public bool SendMail(string subject, string body)
        {
            subject = "Sipariş Onay";
            body = "Siparişiniz Onaylanmıştır İyi Günler Dileriz!";
            try
            {
                var smtp = _smtpRepository.GetSMTPConfiguration();
                var fromAddress = new MailAddress(smtp.Sender_Address, smtp.Sender_Name);
                var toAddress = new MailAddress("serhat.saglam@dtr.kyocera.com");
                // var toAddress = new MailAddress(WebLoginUser.EMail);
                string fromPassword = smtp.Password;

                var smtpClient = new SmtpClient
                {
                    Host = smtp.SMTP_Server,
                    Port = smtp.SMTP_Server_Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    string filePath = "wwwroot/Files/Test.pdf";
                    if (File.Exists(filePath))
                    {
                        Attachment attachment = new Attachment(filePath);
                        message.Attachments.Add(attachment);
                    }
                    else
                    {
                        Console.WriteLine("PDF dosyası bulunamadı: " + filePath);
                    }

                    smtpClient.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hatalı SMTP");
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }

}

