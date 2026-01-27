using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using BLL.Services;

namespace RealEstateManagement_MinhNTSE196686.Pages.Contract
{
    public class IndexModel : PageModel
    {
        private readonly GenericService<DAL.Entities.Contract> _contractService;

        public IndexModel(GenericService<DAL.Entities.Contract> contractService)
        {
            _contractService = contractService;
        }
        [BindProperty(SupportsGet = true)]
        public string? SearchTitle { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchType { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateOnly? SearchDate { get; set; }

        public int UserRole { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        public const int PageSize = 3;

        public IList<DAL.Entities.Contract> Contract { get;set; } = default!;

        public IActionResult OnGet(int? pageIndex)
        {
            int? userRole = HttpContext.Session.GetInt32("UserRole");

            if (userRole == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                UserRole = userRole.Value;
            }

            var query = _contractService.GetAll(includeProperties: "Broker").AsQueryable();

            if (!string.IsNullOrEmpty(SearchTitle) || !string.IsNullOrEmpty(SearchType) || SearchDate.HasValue)
            {
                query = query.Where(c =>
                    (!string.IsNullOrEmpty(SearchTitle) && c.ContractTitle.Contains(SearchTitle)) ||
                    (!string.IsNullOrEmpty(SearchType) && c.PropertyType.Contains(SearchType)) ||
                    (SearchDate.HasValue && c.SigningDate == SearchDate.Value)
                );
            }

            query = query.OrderBy(c => c.ContractTitle)
                     .ThenBy(c => c.PropertyType)
                     .ThenBy(c => c.SigningDate);


            int totalCount = query.Count();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
            PageIndex = pageIndex ?? 1;

            Contract = query.Skip((PageIndex - 1) * PageSize)
                            .Take(PageSize)
                            .ToList();

            return Page();
        }

        public IActionResult OnGetStart()
        {
            Contract = _contractService.GetAll(includeProperties: "Broker")
                                       .OrderBy(c => c.ContractTitle)
                                       .ThenBy(c => c.PropertyType)
                                       .ThenBy(c => c.SigningDate)
                                       .ToList();

            return Page();
        }
    }
}
