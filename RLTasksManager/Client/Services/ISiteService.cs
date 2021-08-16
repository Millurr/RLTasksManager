using RLTasksManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RLTasksManager.Client.Services
{
    public interface ISiteService
    {

        IList<Site> Sites { get; set; }

        Task UpdateIsCheckingOut(int id, Site site);
        Task UpdateIsLateCheckout(int id, Site site);
        Task UpdateIsVacant(int id, Site site);
        Task ResetDefaults();
        Task LoadSitesAsync();
    }
}
