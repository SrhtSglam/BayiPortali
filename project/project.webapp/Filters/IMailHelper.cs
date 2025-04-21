using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using project.data.Abstract;
using project.entity;

namespace project.webapp.Filters
{
    public interface IMailHelper
    {
        public bool SendMail(string subject, string body);
    }

}

