using CronReminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CronReminder.Repository
{
    public interface IReminderRepository
    {
        public void AddReminders(Reminder reminderdto);


        public Reminder GetReminder(int reminderId);


        public IEnumerable<Reminder> GetReminders();


        public void DeleteReminder(int Id);


        public void UpdateReminder(Reminder reminder);

        public IEnumerable<Reminder> GetRemindersByDate();

    }
}
