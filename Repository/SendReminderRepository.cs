using CronReminder.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;

namespace CronReminder.Repository
{
    public class SendReminderRepository : ISendRemindrRepository
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IConfiguration _configuration;

        public SendReminderRepository(IReminderRepository reminderRepository, IConfiguration configuration)
        {
            _reminderRepository = reminderRepository;
            _configuration = configuration;
        }
        
        public string BuildMessageString()
        {
            int counter = 0;
            StringBuilder reminderMessages = new StringBuilder();
            reminderMessages.Append("Reminders that are are due today \n");

            var reminderList = _reminderRepository.GetRemindersByDate();

            foreach (var reminder in reminderList)
            {
                counter = counter + 1;
                reminderMessages.Append(counter.ToString() + " " + reminder.ReminderName + "\n");
            }

           var reminderMessage = reminderMessages.ToString();

            return reminderMessage;
        }


        // TODO: Update SendReminder method to send mutiple messages by user ID.
        public void SendReminder() 
        {
            var messageString = BuildMessageString();
            if(messageString == null)
            {
                return;
            }

            //var userName = _configuration.GetSection("EmailCredentials").GetSection("username").Value;
            //var password = _configuration.GetSection("EmailCredentials").GetSection("password").Value;
            var userName = _configuration["EmailCredentials:username"];
            var password = _configuration["EmailCredentials:password"];
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(userName+"@gmail.com"));
            email.To.Add(MailboxAddress.Parse("2053171420"+"@txt.att.net."));
            email.Subject = " Daily Reminder";
            email.Body = new TextPart(TextFormat.Plain) { Text = messageString };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(userName, password);
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
