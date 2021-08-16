using RLTasksManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RLTasksManager.Client.Services
{

    public class SiteService : ISiteService
    {
        private readonly HttpClient http;

        public IList<Site> Sites { get; set; } = new List<Site>();

        public SiteService(HttpClient http)
        {
            this.http = http;
        }

        public async Task UpdateIsCheckingOut(int id, Site site)
        {
            site.IsCheckingOut = !site.IsCheckingOut;
            site.LastChecked = DateTime.Now;

            if (site.IsCheckingOut)
                site.IsVacant = false;

            await UpdateSite(id, site);
        }

        public async Task UpdateIsLateCheckout(int id, Site site)
        {
            site.IsLateCheckout = !site.IsLateCheckout;
            site.LastChecked = DateTime.Now;

            if (site.IsLateCheckout)
            {
                site.IsVacant = false;
                site.IsCheckingOut = true;
            }

            await UpdateSite(id, site);
        }

        public async Task UpdateIsVacant(int id, Site site)
        {
            site.IsVacant = !site.IsVacant;
            site.LastChecked = DateTime.Now;

            await UpdateSite(id, site);
        }

        public async Task UpdateSite(int id, Site site)
        {
            var res = await http.PutAsJsonAsync<Site>($"api/sites/{id}", site);
        }

        public async Task LoadSitesAsync()
        {
            if (Sites.Count == 0)
            {
                Sites = await http.GetFromJsonAsync<IList<Site>>("api/sites");
            }
        }

        public async Task ResetDefaults()
        {
            Sites = await http.GetFromJsonAsync<IList<Site>>("api/sites/updateall");
        }
    }
}
