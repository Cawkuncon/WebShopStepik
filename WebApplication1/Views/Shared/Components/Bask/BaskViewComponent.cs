using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Views.Shared.Components.BaskComponents
{
    public class BaskViewComponent : ViewComponent
    {
        private readonly IBaskRepository bask;
        public BaskViewComponent(IBaskRepository bask)
        {
            this.bask = bask;
        }
        public IViewComponentResult Invoke()
        {
            int count = bask.GetCount();
            return View("Bask", count);
        }
    }
}
