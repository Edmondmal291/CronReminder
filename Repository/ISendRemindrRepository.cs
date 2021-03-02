using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CronReminder.Repository
{
    public interface ISendRemindrRepository
    {
        public void SendReminder();
        public string BuildMessageString();
    }
}
