using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement_MinhNTSE196686.Filter;
using RealEstateManagement_MinhNTSE196686.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManagement_MinhNTSE196686.Pages.Contract
{
    [RequireRole(1, 2)]
    public class DeleteModel : PageModel
    {

        private readonly GenericService<DAL.Entities.Contract> _contractService;
        private readonly IHubContext<SignalRServer> _hubContext;
        public DeleteModel(GenericService<DAL.Entities.Contract> contractService, IHubContext<SignalRServer> hubContext)
        {
            _contractService = contractService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public DAL.Entities.Contract Contract { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = _contractService.GetById(id.Value);

            if (contract is not null)
            {
                Contract = contract;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractToDelete = _contractService.GetById(id.Value);
            if (contractToDelete != null)
            {
                _contractService.Delete(id.Value);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }

            return RedirectToPage("./Index");
        }
    }
}
