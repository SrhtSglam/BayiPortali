using Microsoft.AspNetCore.Mvc;
using project.data.Abstract;

namespace project.webapp.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.GetAll().Data;
            
            // if (RouteData.Values["category"] != null)
            //     ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(categories);

        }
    }
}
