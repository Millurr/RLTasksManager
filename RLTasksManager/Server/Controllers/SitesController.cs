using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RLTasksManager.Server.Data;
using RLTasksManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RLTasksManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly DataContext context;

        public SitesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSites()
        {
            var sites = await context.Sites.ToListAsync();
            return Ok(sites);
        }

        // Will wrap with Authorization tag later
        [HttpPost]
        public async Task<IActionResult> CreateSite(Site site)
        {
            context.Sites.Add(site);
            await context.SaveChangesAsync();
            return Ok(await context.Sites.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSite(int id, Site site)
        {
            var dbSite = await context.Sites.FirstOrDefaultAsync(s => s.Id == id);

            if (dbSite == null)
            {
                return NotFound("Site with this id was not found");
            }

            dbSite.IsCheckingOut = site.IsCheckingOut;
            dbSite.IsLateCheckout = site.IsLateCheckout;
            dbSite.IsVacant = site.IsVacant;
            dbSite.Note = site.Note;
            dbSite.LastChecked = DateTime.Now;

            await context.SaveChangesAsync();

            return Ok(dbSite);
        }

        [HttpPut("updateall")]
        public async Task<IActionResult> EndOfDayUpdate()
        {
            var sites = await context.Sites.ToListAsync();

            foreach (var site in sites)
            {
                var dbSite = await context.Sites.FirstOrDefaultAsync(s => s.Id == site.Id);
                dbSite.IsCheckingOut = false;
                dbSite.IsLateCheckout = false;
                dbSite.IsVacant = site.IsVacant;
                dbSite.Note = site.Note;
                dbSite.LastChecked = DateTime.Now;

                await context.SaveChangesAsync();
            }

            return Ok(await context.Sites.ToListAsync());
        }

        [HttpPost("createall/{count}")]
        public async Task<IActionResult> CreateFilledDB(int count)
        {
            List<Site> sites = new List<Site>();

            for (int i = 1; i <= count; i++)
            {
                context.Add(new Site { SiteNumber = i, IsCheckingOut = false, IsLateCheckout = false, IsVacant = true });
            }

            await context.SaveChangesAsync();

            return Ok(await context.Sites.ToListAsync());
        }
    }
}
