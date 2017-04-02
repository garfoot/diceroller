using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiceRoller.Web.Components
{
    [ViewComponent(Name = "navBar")]
    public class NavBar : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult((IViewComponentResult)View());
        }
    }
}