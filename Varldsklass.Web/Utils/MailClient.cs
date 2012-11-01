using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories.Abstract;
using Varldsklass.Domain.Entities;
using Varldsklass.Web.ViewModels;
using Varldsklass.Domain.Repositories;

namespace Varldsklass.Web.Utils
{
    public class MailClient
    {
        private static readonly SmtpClient Client;

        static MailClient()
        {
            Client = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["SmtpServer"],
                Port = Convert.ToInt32(
                ConfigurationManager.AppSettings["SmtpPort"]),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            Client.UseDefaultCredentials = false;
            Client.Credentials = new NetworkCredential(
            ConfigurationManager.AppSettings["SmtpUser"],
            ConfigurationManager.AppSettings["SmtpPass"]);
        }

        private static bool SendMessage(string subject, Event bookedEvent, string bookedAttendants) 
        {
            
            MailMessage mm = null;
            bool isSent = false;
            try
            {
                mm = new MailMessage(subject, bookedAttendants);
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                Client.Send(mm);
                isSent = true;
            }
            catch (Exception e)
            {
                var eMsg = e.Message;
            }

           
            return isSent;
        }

        public static bool SendBooking(string email, Event bookedEvent, string bookedAttendants)
        {
            email = ConfigurationManager.AppSettings["adminEmail"];
            //Body is where we add Booking ID, Attendants Name, Event Title
            
            string subject = "Bokning";
            return SendMessage(subject, bookedEvent, bookedAttendants);
        }            
    }
}