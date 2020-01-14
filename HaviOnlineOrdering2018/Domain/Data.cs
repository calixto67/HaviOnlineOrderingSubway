using HaviOnlineOrdering2018.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaviOnlineOrdering2018.Domain
{
    public class Data
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            menu.Add(new Navbar { Id = 17, nameOption = "Back To iSolution", action = System.Web.Configuration.WebConfigurationManager.AppSettings["iSolution"] + "/Portal.aspx", isExternalLink = true, imageClass = "fa fa-mail-reply", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 18, nameOption = "Online Ordering", controller = "Home", action = "OnlineOrdering", imageClass = "fa fa-info-circle", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 19, nameOption = "Shopping Cart", controller = "Home", action = "ShoppingCart", imageClass = "glyphicon glyphicon-shopping-cart", status = true, isParent = false, parentId = 18 });
            menu.Add(new Navbar { Id = 20, nameOption = "Order Status", controller = "Home", action = "OrderStatus", imageClass = "fa fa-history", status = true, isParent = false, parentId = 18 });
            menu.Add(new Navbar { Id = 21, nameOption = "Order History", controller = "Home", action = "OrderHistory", imageClass = "fa fa-history", status = true, isParent = false, parentId = 18 });
            menu.Add(new Navbar { Id = 22, nameOption = "Order By Category", controller = "Home", action = "OrderByCategory", imageClass = "fa fa-object-group", status = true, isParent = false, parentId = 18 });
            menu.Add(new Navbar { Id = 23, nameOption = "Input Item", controller = "Home", action = "InputItemCode", imageClass = "fa fa-terminal", status = true, isParent = false, parentId = 18 });
            //menu.Add(new Navbar { Id = 23, nameOption = "Upload SMS File", controller = "Home", action = "UploadSMSFile", imageClass = "fa fa-cloud-upload", status = true, isParent = false, parentId = 18 });
            menu.Add(new Navbar { Id = 24, nameOption = "Show All Items", controller = "Home", action = "Products", imageClass = "fa-allproducts", status = true, isParent = false, parentId = 18 });
            //menu.Add(new Navbar { Id = 25, nameOption = "Delivery Receipts", action = System.Web.Configuration.WebConfigurationManager.AppSettings["OldOrdering"] + "/drct_main.aspx?store_no=" + ((HaviOnlineOrdering2018.BLL.CustomerBLL)HttpContext.Current.Session["_Customer"]).StoreNo, isExternalLink = true, imageClass = "fa fa-external-link", status = true, isParent = false, parentId = 18 });


            menu.Add(new Navbar { Id = 27, nameOption = "Information", action = System.Web.Configuration.WebConfigurationManager.AppSettings["OldOrdering"] + "/info.asp", isExternalLink = true, imageClass = "fa fa-external-link", status = true, isParent = false, parentId = 0 });
            //menu.Add(new Navbar { Id = 28, nameOption = "Stock Recovery Notice", action = HaviOnlineOrdering2018.Properties.Settings.Default["OldOrdering"] + "/stockrecovery.aspx?store_no=" + ((HaviOnlineOrdering2018.BLL.CustomerBLL)HttpContext.Current.Session["_Customer"]).StoreNo , isExternalLink = true, imageClass = "fa fa-external-link", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 28, nameOption = "Product Complaint", action = System.Web.Configuration.WebConfigurationManager.AppSettings["OldOrdering"] + "/transfer.asp", isExternalLink = true, imageClass = "fa fa-external-link", status = true, isParent = false, parentId = 0 });
            //menu.Add(new Navbar { Id = 29, nameOption = "Change Password", action = System.Web.Configuration.WebConfigurationManager.AppSettings["OldOrdering"] + "/change.asp", isExternalLink = true, imageClass = "fa fa-external-link", status = true, isParent = false, parentId = 0 });

            //menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
            //menu.Add(new Navbar { Id = 2, nameOption = "Charts", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = true, parentId = 0 });
            //menu.Add(new Navbar { Id = 3, nameOption = "Flot Charts", controller = "Home", action = "FlotCharts", status = true, isParent = false, parentId = 2 });
            //menu.Add(new Navbar { Id = 4, nameOption = "Morris.js Charts", controller = "Home", action = "MorrisCharts", status = true, isParent = false, parentId = 2 });
            //menu.Add(new Navbar { Id = 5, nameOption = "Tables", controller = "Home", action = "Tables", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            //menu.Add(new Navbar { Id = 6, nameOption = "Forms", controller = "Home", action = "Forms", imageClass = "fa fa-edit fa-fw", status = true, isParent = false, parentId = 0 });
            //menu.Add(new Navbar { Id = 7, nameOption = "UI Elements", imageClass = "fa fa-wrench fa-fw", status = true, isParent = true, parentId = 0 });
            //menu.Add(new Navbar { Id = 8, nameOption = "Panels and Wells", controller = "Home", action = "Panels", status = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 9, nameOption = "Buttons", controller = "Home", action = "Buttons", status = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 10, nameOption = "Notifications", controller = "Home", action = "Notifications", status = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 11, nameOption = "Typography", controller = "Home", action = "Typography", status = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 12, nameOption = "Icons", controller = "Home", action = "Icons", status = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 13, nameOption = "Grid", controller = "Home", action = "Grid", status = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 14, nameOption = "Multi-Level Dropdown", imageClass = "fa fa-sitemap fa-fw", status = true, isParent = true, parentId = 0 });
            //menu.Add(new Navbar { Id = 15, nameOption = "Second Level Item", status = true, isParent = false, parentId = 14 });
            //menu.Add(new Navbar { Id = 16, nameOption = "Sample Pages", imageClass = "fa fa-files-o fa-fw", status = true, isParent = true, parentId = 0 });
            //menu.Add(new Navbar { Id = 17, nameOption = "Blank Page", controller = "Home", action = "Blank", status = true, isParent = false, parentId = 16 });
            //menu.Add(new Navbar { Id = 18, nameOption = "Login Page", controller = "Home", action = "Login", status = true, isParent = false, parentId = 16 });

            return menu.ToList();
        }

        
    }
}