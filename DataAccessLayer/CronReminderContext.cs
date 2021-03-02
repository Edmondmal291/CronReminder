using Microsoft.EntityFrameworkCore;
using CronReminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CronReminder.DataAccessLayer
{
    public class CronReminderContext:DbContext
    {
        public CronReminderContext(DbContextOptions<CronReminderContext> options) : base(options)
        {

        }

        public DbSet<Reminder> Reminders { get; set; }
    }
}
