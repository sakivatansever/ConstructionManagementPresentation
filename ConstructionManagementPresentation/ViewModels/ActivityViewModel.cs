using ConstructionManagementPresentation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConstructionManagementPresentation.ViewModels
{
    public class ActivityViewModel
    {
        public Activity Activity { get; set; }

        public List<SelectListItem> Workers { get; set; }
    }
}
