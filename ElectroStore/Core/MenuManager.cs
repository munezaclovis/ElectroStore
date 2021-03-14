using ElectroStore.Data;
using ElectroStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Core
{
    public class MenuManager
    {
        private readonly ApplicationDbContext _context;

        public MenuManager(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<MenuItem> getMenu()
        {
            List<MenuItem> MenuList = new List<MenuItem>();

            var categories = _context.Categories.ToList();
            var categoriesList = new List<MenuItem>();
            if (categories != null)
            {
                foreach (var item in categories)
                {
                    if (item.Icon == "") item.Icon = "dripicons-dot";
                    categoriesList.Add(new MenuItem(item.Name, null, new MenuLink("", "Categories", "Index", ""), new string[] { "Guest", "Manager", "Employee", "Customer" }, null, item.Icon));
                }
            }
            else
            {
                categoriesList = null;
            }

            List<MenuItem> managementSubMenu = new List<MenuItem>
            {
                new MenuItem("Brands", new List<MenuItem>{
                    new MenuItem("Add", null, new MenuLink("Management", "Brands", "Add"), new string[] { "" }, null, ""),
                    new MenuItem("List", null, new MenuLink("Management", "Brands", "Index"), new string[] { "" }, null, "")
                }, new MenuLink("Management", "Brands", "Index"), new string[] { "Manager", "Employee" }, null, "fas fa-building"),

                new MenuItem("Categories", new List<MenuItem>{
                    new MenuItem("Add", null, new MenuLink("Management", "Categories", "Add"), new string[] { "" }, null, ""),
                    new MenuItem("List", null, new MenuLink("Management", "Categories", "Index"), new string[] { "" }, null, "")
                }, new MenuLink("Management", "Categories", "Index"), new string[] { "Manager", "Employee" }, null, "dripicons-suitcase"),

                new MenuItem("Products", new List<MenuItem>{
                    new MenuItem("Add", null, new MenuLink("Management", "Products", "Add"), new string[] { "" }, null, ""),
                    new MenuItem("List", null, new MenuLink("Management", "Products", "Index"), new string[] { "" }, null, "")
                }, new MenuLink("Management", "Products", "Index"), new string[] { "Manager", "Employee" }, null, "fas fa-building"),

                new MenuItem("Users", new List<MenuItem>{
                    new MenuItem("Add", null, new MenuLink("Management", "Users", "Add"), new string[] { "" }, null, ""),
                    new MenuItem("List", null, new MenuLink("Management", "Users", "Index"), new string[] { "" }, null, "")
                }, new MenuLink("Management", "Users", "Index"), new string[] { "Manager" }, null, "dripicons-user-group")
            };
            
            MenuList.Add(new MenuItem("Home", null, new MenuLink("", "Home", "Index"), new string[] {"Guest", "Manager", "Employee", "Customer" }, null, "dripicons-home"));
            MenuList.Add(new MenuItem("Categories", null, new MenuLink("", "Categories", "Index"), new string[] { "Guest", "Manager", "Employee", "Customer" }, null, "dripicons-view-apps"));
            MenuList.Add(new MenuItem("Brands", null, new MenuLink("", "Brands", "Index"), new string[] { "Guest", "Manager", "Employee", "Customer" }, null, "fas fa-building"));
            MenuList.Add(new MenuItem("About Us", null, new MenuLink("", "Home", "AboutUs"), new string[] { "Guest", "Manager", "Employee", "Customer" }, null, "dripicons-suitcase"));
            MenuList.Add(new MenuItem("Contact Us", null, new MenuLink("", "Home", "Contact"), new string[] { "Guest", "Manager", "Employee", "Customer" }, null, "dripicons-document"));
            MenuList.Add(new MenuItem("Management", managementSubMenu, new MenuLink("Management", "Dashboard", "Index"), new string[] { "Manager", "Employee" }, null, "dripicons-stack"));

            return MenuList;
        }
    }
}
