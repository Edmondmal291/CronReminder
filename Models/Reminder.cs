using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CronReminder.Models
{
    public class Reminder
    {
        public int ReminderId { get; set; }
        [Required]
        [StringLength(25,ErrorMessage ="Reminder name should not be less than 3 character or more than 25 character", MinimumLength = 3)]
        public string ReminderName { get; set; }
        [StringLength(60, ErrorMessage = "Reminder Description should not be less than 3 character or more than 60 character", MinimumLength = 3)]
        public string ReminderDescription { get; set; }
        [Required]
        public string CellNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReminderDate { get; set; }
        
    }
}
