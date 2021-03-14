using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Core
{
    public class MenuItem
    {
        public MenuItem(string diplay, List<MenuItem> subMenuItem, MenuLink link, string[] accessLevel, string position = "left", string icon = null)
        {
            Diplay = diplay;
            this.SubMenuItem = subMenuItem;
            this.Link = link;
            AccessLevel = accessLevel;
            Position = position;
            Icon = icon;
        }
        public string Diplay { set; get; }
        public List<MenuItem> SubMenuItem { set; get; }
        public MenuLink Link { set; get; }
        public string Position { set; get; }
        public string[] AccessLevel { set; get; }
        public string Icon { set; get; }
    }

    public class MenuLink
    {
        public MenuLink(string area, string controller, string action)
        {
            Controller = controller;
            Action = action;
            Area = area;
        }

        public MenuLink(string area, string controller, string action, string param)
        {
            Controller = controller;
            Action = action;
            Area = area;
            Param = param;
        }
        public string Area { set; get; }
        public string Controller { set; get; }
        public string Action { set; get; }
        public string Param { set; get; } = "";
    }
}
