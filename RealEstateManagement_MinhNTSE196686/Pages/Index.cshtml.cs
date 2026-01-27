using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateManagement_MinhNTSE196686.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("UserRole") != null)
            {
                return RedirectToPage("/Contract/Index");
            }
            return RedirectToPage("/Login");
        }
    }
}
