using System;
using CronReminder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CronReminder.DataAccessLayer
{
    public static class DbInitalizer
    {
        public static void Intilaize(CronReminderContext context)
        {
            if (context.Reminders.Any())
            {
                return;
            }
            var reminders = new Reminder[]
            {
                new Reminder{ReminderName="Test1",ReminderDescription="Take test", ReminderDate = DateTime.Parse("2005-09-01")},
                new Reminder{ReminderName="Test2",ReminderDescription="Take test again", ReminderDate = DateTime.Parse("2006-10-02")}
            };
            foreach (Reminder r in reminders)
            {
                context.Reminders.Add(r);
            }
            context.SaveChanges();
        }
    }
}
