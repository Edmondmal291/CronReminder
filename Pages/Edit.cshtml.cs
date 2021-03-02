using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CronReminder.Models;
using CronReminder.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CronReminder.Pages
{
    public class EditModel : PageModel
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly ISendRemindrRepository _sendRemindrRepository;


        [BindProperty]
        public Reminder reminder { get; set; }


        public EditModel(IReminderRepository reminderRepository, ISendRemindrRepository sendRemindrRepository)
        {
            _reminderRepository = reminderRepository;
            _sendRemindrRepository = sendRemindrRepository;
        }


        // Add the add update and delete handlers for the 
        public IActionResult OnGet(int reminderId)
        {
            if (reminderId == 0)
            {
                reminder = new Reminder();
                reminder.ReminderDate = DateTime.Now;
                

                return Page();
            }
            else
            {
                //var intRemindeId = reminderId.GetValueOrDefault();
                reminder = _reminderRepository.GetReminder(reminderId);
                return Page();
            }
        }

        public IActionResult OnPostAddReminder(Reminder reminder)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _reminderRepository.AddReminders(reminder);
            return RedirectToPage("/Index","OnGet");
         
        }

        public IActionResult OnGetReminder(int reminderId)
        {
            // return the reminder object values to th view
            reminder = _reminderRepository.GetReminder(reminderId);

            return RedirectToPage("/Index");
        }

        public IActionResult OnPostDeleteReminder(int reminderId)
        {
            _reminderRepository.DeleteReminder(reminderId);

            return RedirectToPage("/Index");
           
        }

        public IActionResult OnPostUpdateReminder(Reminder reminder)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _reminderRepository.UpdateReminder(reminder);

            return RedirectToPage("/Index");
        }
    }
}
