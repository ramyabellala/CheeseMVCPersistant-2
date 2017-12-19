using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Menu> menus = context.Menus.ToList();
            
            return View(menus);
        }

        public IActionResult Add()
        {
            AddMenuViewModel newMenu = new AddMenuViewModel();
            return View(newMenu);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addCheeseMenuModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addCheeseMenuModel.Name,
                };
                context.Menus.Add(newMenu);
                context.SaveChanges();
                return Redirect("/Menu");
            }
            return View(addCheeseMenuModel);
        }
        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = context.CheeseMenus.Include(item => item.Cheese).Where(cm => cm.MenuID == id).ToList();
            Menu menu = context.Menus.Single(m => m.ID == id);
            ViewMenuViewModel viewModel = new ViewMenuViewModel { Menu = menu, Items = items };
            return View(viewModel);
         }
         public IActionResult AddItem(int id)
        {
            Menu Menu = context.Menus.Single(m => m.ID == id);
            List<Cheese> Cheeses = context.Cheeses.ToList();
            return View(new AddMenuItemViewModel(Menu, Cheeses));
        }
        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var cheeseID = addMenuItemViewModel.cheeseID;
                var menuID = addMenuItemViewModel.menuID;
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == cheeseID)
                    .Where(cm => cm.MenuID == menuID).ToList();
                if(existingItems.Count ==0)
                {
                    CheeseMenu menuItem = new CheeseMenu
                    {
                        Cheese = context.Cheeses.Single(c => c.ID == cheeseID),
                        Menu = context.Menus.Single(m => m.ID == menuID)
                    };
                    context.CheeseMenus.Add(menuItem);
                    context.SaveChanges();
                }
                return Redirect(string.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel.menuID));
            }
            return View(addMenuItemViewModel);
        }
    }
}
