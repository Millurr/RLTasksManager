using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLTasksManager.Shared
{
    public class Site
    {
        public int Id { get; set; }
        public int SiteNumber { get; set; }
        public bool IsVacant { get; set; } = true;
        public bool IsCheckingOut { get; set; } = false;
        public bool IsLateCheckout { get; set; } = false;
        public string Note { get; set; }
        public DateTime LastChecked { get; set; } = DateTime.Now;
    }
}
