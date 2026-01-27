using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace RealEstateManagement_MinhNTSE196686.Pages.Contract
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.Entities.Fa25realEstateDbContext _context;

        public DetailsModel(DAL.Entities.Fa25realEstateDbContext context)
        {
            _context = context;
        }

        public DAL.Entities.Contract Contract { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FirstOrDefaultAsync(m => m.ContractId == id);

            if (contract is not null)
            {
                Contract = contract;

                return Page();
            }

            return NotFound();
        }
    }
}
