using CronReminder.Models;
using CronReminder.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CronReminder.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private  readonly IReminderRepository _reminderRespository;

        [BindProperty]
         public IEnumerable<Reminder> reminderList { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IReminderRepository reminderRepository)
        {
            _logger = logger;
            _reminderRespository = reminderRepository;
        }

        public void OnGet()
        {
            reminderList =_reminderRespository.GetReminders();
        }

    }
}
