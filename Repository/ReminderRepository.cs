using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CronReminder.Models;
using CronReminder.DataAccessLayer;

namespace CronReminder.Repository
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly CronReminderContext _context;

        public ReminderRepository(CronReminderContext context)
        {
            _context = context;
        }

        public void AddReminders(Reminder reminderdto)
        {
            _context.Reminders.Add(reminderdto);
            _context.SaveChanges();

        }

        public Reminder GetReminder(int reminderId)
        {
            Reminder reminderDTO = new Reminder();
            var reminder = _context.Reminders.Where(r => r.ReminderId == reminderId).SingleOrDefault();
            reminderDTO.ReminderId = reminder.ReminderId;
            reminderDTO.ReminderName = reminder.ReminderName;
            reminderDTO.ReminderDescription = reminder.ReminderDescription;
            reminderDTO.CellNumber = reminder.CellNumber;
            reminderDTO.ReminderDate = reminder.ReminderDate;

            return reminderDTO;
        }

        public IEnumerable<Reminder> GetReminders()
        {
            var reminderList = _context.Reminders.ToList();

            return reminderList;
        }

        public void DeleteReminder(int Id)
        {
            var deletedReminder = _context.Reminders.Where(r => r.ReminderId == Id).FirstOrDefault();
            _context.Reminders.Remove(deletedReminder);
            _context.SaveChanges();
        }

        public void UpdateReminder(Reminder reminder)
        {
            var reminderUpdate = (from r in _context.Reminders
                                  where r.ReminderId == reminder.ReminderId
                                  select r).FirstOrDefault();
            reminderUpdate.ReminderDate = reminder.ReminderDate;
            reminderUpdate.ReminderDescription = reminder.ReminderDescription;
            reminderUpdate.ReminderName = reminder.ReminderName;
            reminderUpdate.CellNumber = reminder.CellNumber;

            _context.Reminders.Update(reminderUpdate);
            _context.SaveChanges();

        }

        public IEnumerable<Reminder> GetRemindersByDate()
        {
            var reminderList = _context.Reminders.Where(r => r.ReminderDate == DateTime.Now.Date).ToList();
            return reminderList;
        }

    }
}
