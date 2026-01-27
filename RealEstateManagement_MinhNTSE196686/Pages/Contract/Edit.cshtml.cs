using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.AspNetCore.SignalR;
using BLL.Services;
using RealEstateManagement_MinhNTSE196686.Hubs;
using RealEstateManagement_MinhNTSE196686.Filter;

namespace RealEstateManagement_MinhNTSE196686.Pages.Contract
{
    [RequireRole(1, 2)]
    public class EditModel : PageModel
    {
        private readonly IHubContext<SignalRServer> _hubContext;
        private readonly GenericService<DAL.Entities.Contract> _contractService;
        private readonly GenericService<DAL.Entities.Broker> _brokerService;

        public EditModel(GenericService<Broker> brokerService, GenericService<DAL.Entities.Contract> contractService, IHubContext<SignalRServer> hubContext)
        {
            _brokerService = brokerService;
            _contractService = contractService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public DAL.Entities.Contract Contract { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = _contractService.GetById(id.Value);
            if (contract == null)
            {
                return NotFound();
            }
            Contract = contract;
            ViewData["BrokerId"] = new SelectList(_brokerService.GetAll(), "BrokerId", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Contract.Broker");
            if (!ModelState.IsValid)
            {
                ViewData["BrokerId"] = new SelectList(_brokerService.GetAll(), "BrokerId", "FullName");
                return Page();
            }

            try
            {
                _contractService.Update(Contract);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(Contract.ContractId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContractExists(int id)
        {
            return _contractService.GetById(id) != null;
        }
    }
}
