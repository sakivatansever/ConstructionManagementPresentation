using ConstructionManagementPresentation.Models.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConstructionManagementPresentation.Extantions
{
    public  static class HtmlHelperExtantions
    {
        public static string IsSelected(this IHtmlHelper htmlHelper, Role role, Role selectedRole)
        {
            return role == selectedRole ? "selected" : "";
        }
    }
}
