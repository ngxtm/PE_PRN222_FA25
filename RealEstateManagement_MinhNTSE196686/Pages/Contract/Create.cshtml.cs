using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using RealEstateManagement_MinhNTSE196686.Filter;
using RealEstateManagement_MinhNTSE196686.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManagement_MinhNTSE196686.Pages.Contract
{
    [RequireRole(1, 2)]
    public class CreateModel : PageModel
    {
        private readonly GenericService<DAL.Entities.Contract> _contractService;
        private readonly GenericService<Broker> _brokerService;
        private readonly IHubContext<SignalRServer> _hubContext;

        public CreateModel (GenericService<DAL.Entities.Contract> contractService, GenericService<Broker> brokerService, IHubContext<SignalRServer> hubContext)
        {
            _contractService = contractService;
            _brokerService = brokerService;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            ViewData["BrokerId"] = new SelectList(_brokerService.GetAll(), "BrokerId", "FullName");
            return Page();
        }

        [BindProperty]
        public DAL.Entities.Contract Contract { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Contract.Broker");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _contractService.Add(Contract);
            await _hubContext.Clients.All.SendAsync("LoadData");

            return RedirectToPage("./Index");
        }
    }
}
