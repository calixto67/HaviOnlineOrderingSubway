using HaviOnlineOrdering2018.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using HaviOnlineOrdering2018.BLL;
using System.Web.UI;


namespace HaviOnlineOrdering2018.Controllers
{
    public class HomeController : Controller
    {

#region VARIABLES

        HAVIEntities dbContext = new HAVIEntities();

        CustomerBLL Customer = CustomerBLL.Customer;
      
        string _iSolutionURI = System.Web.Configuration.WebConfigurationManager.AppSettings["iSolution"] + "/index.aspx";

        private string[] _DDSpec = new string[] { "302S1", "303S1" };
        private string[] _ItemsWithStandingOrder = new string[] { "88109", "0071", "0072", "0073", "0074", "1026", "86655", "87268", "85764", "89155", "89087", "0013", "89091" };


#endregion

        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Login()
        {
            if (Customer.EntityID != null)
            {

                return View("Login");
            }
            else
            {
                return Redirect(_iSolutionURI);
            }
        }
        public ActionResult OnlineOrdering()
        {
            if (Customer.EntityID != null)
            {

                return View("OnlineOrdering");
            }
            else
            {
                return Redirect(_iSolutionURI);
            }
        }

        public ActionResult SubmitAction()
        {
            try
            {
                StoreSession();    
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                if (Customer.EntityID != null)
                {
                    tbl_store Store = dbContext.tbl_store.Where(x => x.store_no == Customer.StoreNo).Single();

                    if (Store != null)
                    {
                        Customer.ownerid = Store.owner_id;
                        return Redirect("~/Home");
                    }
                    new UtilityBLL(this).ThrowError("Invalid Store");
                    return View("SessionExpires");
                }
                else
                {
                    return View("SessionExpires");
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                return View();
            }
            
        }

        public ActionResult SessionExpires()
        {
            Session.RemoveAll();
            ViewData["Message"] = "Your session has been ended.";
            return View("SessionExpires");
        }
                
        public ActionResult OrderByCategory()
        {
            try
            {
                if (Customer.EntityID != null)
                {
                    List<tbl_Category> lstcat = dbContext.tbl_Category.Where(x => x.owner_id == Customer.ownerid).ToList();
                    List<vw_AllowedItemsOnStore> AllowedItems = new List<vw_AllowedItemsOnStore>();
                    AllowedItems = dbContext.vw_AllowedItemsOnStore.Where(x => x.store_no.Trim() == Customer.StoreNo).ToList();

                    lstcat = lstcat.Where(x => AllowedItems.ToList().Where(y => y.category_id == x.category_id).Count() > 0).ToList();

                    return View(lstcat);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                throw;
            }
            
        }

        public ActionResult InputItemCode()
        { 
            try 
	        {
                if (Customer.EntityID != null)
                {
                    Customer.Orders.RemoveListOrderedItemQty();
                    return View(Customer);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
               
	        }
	        catch (Exception ex)
	        {
		        new UtilityBLL(this).ThrowError(ex.Message);
                throw;
	        }
            
        }

        public ActionResult UploadExcelFile()
        {
            try
            {
                if (Customer.EntityID != null)
                {
                    Customer.Orders.RemoveListOrderedItemQty();
                    return View(Customer);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }

            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                throw;
            }

        }

        public ActionResult UploadSMSFile()
        {
            try
            {
                if (Customer.EntityID != null)
                {
                    Customer.Orders.RemoveListOrderedItemQty();
                    return View(Customer);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }

            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                throw;
            }

        }


        [HttpPost]
        public ActionResult SubmitFile(HttpPostedFileBase file,bool SMSFile) 
        {
            try
            {
               
                if (Customer.EntityID != null)
                {
                    if (file != null)
                    {
                        file.InputStream.Position = 0;
                        if (SMSFile)
                        {

                            var mItemCodeQtyList = string.Empty;

                            //read data from input stream
                            using (var csvReader = new System.IO.StreamReader(file.InputStream))
                            {
                                string inputLine = "";
                                var x = 0;
                               
                                //read each line
                                while ((inputLine = csvReader.ReadLine()) != null)
                                {
                                    x += 1;
                                    //get lines values
                                    string[] values = inputLine.Split(new char[] { ',' });

                                    if (values.Length > 2)
                                    {
                                        var wrin = values[3].ToString().PadLeft(4, '0'); ;
                                        var qty = values[8] == null ? "1" : values[8];
                                        
                                        //if (StoreItemsAllowed.Where(y => y.wrin == wrin).Count() > 0)
                                        //{
                                            //Customer.Orders.ListOrderedItemQty.Add(new OrdersBLL.OrderedItemQty() { ItemCode = wrin, Qty = Convert.ToInt32(qty) });
                                            mItemCodeQtyList += wrin + "," + qty + "\n";    
                                        //}
                                        //else
                                        //{
                                        //    mItemCodeNotExist += "[<span style='color:red;font-weight:bold;'>" + wrin + "</span>] ";
                                        //}
                                    }
                                }
                                string[] stringSeparators = new string[] { "\n" };
                                string[] arr = mItemCodeQtyList.Split(stringSeparators, StringSplitOptions.None);

                                Array.Sort(arr);
                                ViewData["FileContent"] = string.Join("\n",arr);
                                //ViewData["ItemsNotExist"] = mItemCodeNotExist.Trim() == string.Empty ? "" : "<span>Note : Item code " + mItemCodeNotExist + " does not exist. </span>";
                                csvReader.Close();
                            }
                        }
                        else
                        {

                        }
                    }
                    return View("InputItemCode",Customer);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }

            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult SubmittedInputItems(FormCollection frm)
        {
            Customer.Orders.RemoveListOrderedItemQty();
            string[] inputitemcode = null;
            string[] stringSeparators = new string[] { "\r\n" };
            List<OrdersBLL.Cart> StoreItemsAllowed = Customer.AllowedItemsOnStore(null);
            var mItemCodeQtyList = string.Empty;
            var mItemCodeNotExist = string.Empty;


            try
            {
                if (frm["ta"] != null)
                {
                    inputitemcode = frm["ta"].Split(stringSeparators, StringSplitOptions.None);

                    Array.Reverse(inputitemcode);
                    foreach (var item in inputitemcode)
                    {
                        if (new UtilityBLL(null).ConvertToString(item.Split(',')[0]) != string.Empty)
                        {
                            var wrin = item.Split(',')[0].ToString().Trim();

                            if (StoreItemsAllowed.Where(y => y.wrin == wrin).Count() > 0)
                            {
                                int mQty = item.Split(',').Count() > 1 ? Convert.ToInt32(item.Split(',')[1]) : 1;

                                Customer.Orders.ListOrderedItemQty.Add(new OrdersBLL.OrderedItemQty() { ItemCode = item.Split(',')[0].ToString(), Qty = mQty });
                                mItemCodeQtyList += wrin + "," + mQty + "\n";
                            }
                            else
                            {
                                mItemCodeNotExist += "[<span style='color:red;font-weight:bold;'>" + wrin + "</span>] ";
                            }
                        }
                    }

                    string[] arr = mItemCodeQtyList.Split(new string[] { "\n" }, StringSplitOptions.None);

                    Array.Sort(arr);

                    ViewData["FileContent"] = string.Join("\n", arr);
                    ViewData["ItemsNotExist"] = mItemCodeNotExist.Trim() == string.Empty ? "" : "<span>Note : Item code " + mItemCodeNotExist + " does not exist. </span>";
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                return View("InputItemCode",Customer);
            }

            return View("InputItemCode",Customer);
        }

        [HttpPost]
        public ActionResult SearchItem(FormCollection frm)
        {
            return Redirect("Products?item=" + frm["Search"] + "");
        }

        public ActionResult ShoppingCart(FormCollection frm)
        {
            try
            {
                ViewBag.Title = "Shopping Cart";
                if (Customer.EntityID != null)
                {
                    ViewBag.SpecialDeliveryDate = Customer.Orders.ShoppingCart.SpecialDeliveryDate;
                    if (GetAllItemsOrdered(Request))
                    {
                        
                    }
                    return View(Customer);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                return View();
            }
           
        }

        public ActionResult OrderHistory(FormCollection frmcol)
        {
            try
            {
                if (Customer.EntityID != null)
                {
                    Customer.Orders.ClearOrderCartHistory();
                    OrdersBLL.OrderHistoryParam _OrderHxParam = new OrdersBLL.OrderHistoryParam(Customer);
                    string _firstdayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                    string _lastdayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();
                    _OrderHxParam.DateType = 1;
                    if (frmcol.Count > 0)
                    {
                        _OrderHxParam.date_from = Convert.ToDateTime(frmcol["date_from"]).Date;
                        _OrderHxParam.date_to = Convert.ToDateTime(frmcol["date_to"]).Date;
                        _OrderHxParam.DateType = Convert.ToInt32(frmcol["sel_DateType"]);
                    }
                    else
                    {
                        _OrderHxParam.date_from = Convert.ToDateTime(_firstdayOfMonth).Date;
                        _OrderHxParam.date_to = Convert.ToDateTime(_lastdayOfMonth).Date;
                    }
                    _OrderHxParam.IsOrderHistory = true;
                    
                    return View(_OrderHxParam);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                return View();
            }
           
        }

        public ActionResult OrderStatus(FormCollection frmcol)
        {
            try
            {
                if (Customer.EntityID != null)
                {
                    Customer.Orders.ClearOrderCartHistory();
                    OrdersBLL.OrderHistoryParam _OrderHxParam = new OrdersBLL.OrderHistoryParam(Customer);
                    string _firstdayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                    string _lastdayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();
                    _OrderHxParam.DateType = 1;
                    if (frmcol.Count > 0)
                    {
                        _OrderHxParam.date_from = Convert.ToDateTime(frmcol["date_from"]).Date;
                        _OrderHxParam.date_to = Convert.ToDateTime(frmcol["date_to"]).Date;
                        _OrderHxParam.DateType = Convert.ToInt32(frmcol["sel_DateType"]);
                    }
                    else
                    {
                        _OrderHxParam.date_from = Convert.ToDateTime(_firstdayOfMonth).Date;
                        _OrderHxParam.date_to = Convert.ToDateTime(_lastdayOfMonth).Date;
                    }
                    _OrderHxParam.IsOrderHistory = false;

                    return View(_OrderHxParam);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                return View();
            }

        }

        public ActionResult SpecialDelSched()
        { 
            if (Customer.EntityID != null)
            {
                if (Request["Special"] != null)
                {
                    ViewBag.Special = true;
                    ViewData["DateSelection"] = new SelectList(Customer.SpecialDeliverySchedule.ToList(), "Value", "Text", Convert.ToDateTime(Customer.OrderDeliveryDate).ToShortDateString());
                    
                    Customer.Orders.OrderCartHistory.tblTransaction.Special = true;
                }
                else
                {
                    ViewData["DateSelection"] = new SelectList(Customer.RegularDeliverySchedule.ToList(), "Value", "Text", Convert.ToDateTime(Customer.OrderDeliveryDate).ToShortDateString());
                    
                    Customer.Orders.OrderCartHistory.tblTransaction.Special = false;
                }
                
                return View("OrderDetails",Customer);
            }
            else
            {
                return Redirect(_iSolutionURI);
            }
        }

        public ActionResult OrderDetails()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                if (Customer.EntityID != null)
                {
                    Customer.Orders.ClearOrderCartHistory();
                    Customer.Orders.OrderCartHistory.OrderNo = Request["OrderNo"].ToString();

                    list = Customer.Orders.OrderCartHistory.tblTransaction.Trans_type == "S" || ViewBag.Special != null ? Customer.SpecialDeliverySchedule : Customer.RegularDeliverySchedule;

                    if (Customer.Orders.OrderCartHistory.tblTransaction.Status == "PROCESSED")
                    {
                        list.Add(new SelectListItem()
                        {
                            Value = Convert.ToDateTime(Customer.Orders.OrderCartHistory.tblTransaction.Del_date).ToShortDateString(),
                            Text = Convert.ToDateTime(Customer.Orders.OrderCartHistory.tblTransaction.Del_date).ToShortDateString()
                        });
                    }
                    
                    var selectList = new SelectList(list, "Value", "Text", Convert.ToDateTime(Customer.Orders.OrderCartHistory.tblTransaction.Delivery_date).ToShortDateString());
                    ViewData["DateSelection"] = selectList;

                    return View(Customer);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                return View();
            }
        }

        public ActionResult Products(int? catid, string category)
        {
            try
            {
                if (Customer.EntityID != null)
                {
                    Customer.Orders.SearchValue = Request.QueryString["Search"] == null ? "" : Request.QueryString["Search"];
                    ViewBag.catid = catid;
                    ViewData["cat"] = category == null ? "All Items" : category;
                    
                    //List<tbl_mcc_sub_cat> mcc = dbContext.tbl_mcc_sub_cat.ToList();
                    //if (Request.QueryString["subcat"] != null)
                    //{
                    //    var q = from _item in lstStoreItems.Contains()
                    //            where dbContext
                    //            select _item;

                    //    var results = lstStoreItems.Where(i => mcc.Contains(.wrin));
                    //    lstStoreItems = lstStoreItems.Where(i => i.wrin.Any(value => mcc.Contains(i.wrin));
                    //    //lstStoreItems = lstStoreItems.ToList().Where(x => dbContext.tbl_mcc_sub_cat.ToList().Where(y => y.wrin == x.wrin && y.subcatid == Request.QueryString["subcatid"]).Count() > 1).ToList();
                    //}
                    return View(Customer);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                return View();
            }
        }

        public ActionResult ProductsSubCat(int catid,string category)
        {
            try
            {
                if (Customer.EntityID != null)
                {
                    List<ProductsBLL.ProductsSubCatModel> ProdSubCat = new List<ProductsBLL.ProductsSubCatModel>();
                    ViewData["subcat"] = "Subcategory";
                    ViewData["category"] = category;
                    //if (catid == 47)
                    //{
                    //    ViewData["subcat"] = "Direct Deliveries";
                    //    ProdSubCat = dbContext.vw_SupplierLeadtime.Where(x => x.store_no == Customer.StoreNo).ToList()
                    //                .Select(x => new ProductsBLL.ProductsSubCatModel() { categoryid = catid, Subcatid = x.Subcatid, Description = x.Description })
                    //                .ToList();
                    //    //AutoMapper.Mapper.Map(lstSuplt, ProdSubCat);
                    //}
                    //else
                    //{
                    //    ProdSubCat = dbContext.vw_Subcat.ToList()
                    //                .Select(x => new ProductsBLL.ProductsSubCatModel() { categoryid = catid, Subcatid = x.subcatid, Description = x.Description })
                    //                .ToList();
                    //    //AutoMapper.Mapper.Map(db.vw_Subcat.ToList(), ProdSubCat);
                    //}
                    return View(ProdSubCat);
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                return View();
            }
        }

        public ActionResult DeleteItem(string itemcode,bool? OrderHx,int? OrderNo)
        {
            try
            {
                if (Customer.EntityID != null)
                {
                    //CustomerBLL _SessCustomer = (Session["_Customer"] as CustomerBLL);

                    if (OrderHx != null)
                    {
                        if (OrderNo != null)
                        {
                            Customer.Orders.OrderCartHistory.RemoveOrderNo(OrderNo);
                            new UtilityBLL(this).MessageBox("Order was successfully removed", "Order Cancelled");
                            return View("OrderHistory",
                            new OrdersBLL.OrderHistoryParam(Customer)
                            {
                                date_from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                                date_to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1),
                                DateType = 1
                            });
                        }
                        else
                        {
                            Customer.Orders.OrderCartHistory.RemoveItem(itemcode);
                            var list = Customer.Orders.OrderCartHistory.tblTransaction.Trans_type == "S" || ViewBag.Special != null ? Customer.SpecialDeliverySchedule : Customer.RegularDeliverySchedule;
                            var selectList = new SelectList(list, "Value", "Text", Convert.ToDateTime(Customer.Orders.OrderCartHistory.tblTransaction.Delivery_date).ToShortDateString());
                            ViewData["DateSelection"] = selectList;

                            return View("OrderDetails", Customer);
                        }
                        
                        
                    }
                    else
                    {
                        Customer.Orders.ShoppingCartItems.RemoveAll(x => x.wrin == itemcode);
                        return View("ShoppingCart", Customer);
                    }
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                Customer.Orders.OrderCartHistory.RemoveItem(itemcode);
                var list = Customer.Orders.OrderCartHistory.tblTransaction.Trans_type == "S" || ViewBag.Special != null ? Customer.SpecialDeliverySchedule : Customer.RegularDeliverySchedule;
                var selectList = new SelectList(list, "Value", "Text", Convert.ToDateTime(Customer.Orders.OrderCartHistory.tblTransaction.Delivery_date).ToShortDateString());
                ViewData["DateSelection"] = selectList;
                return View("OrderDetails", Customer);
            }
        }


        [ValidateInput(false)]
        public JsonResult UpdateShoppingCart(object cust)
        {
            if (!string.IsNullOrEmpty(Request.Form["SpecialOrder"]))
            {
                Customer.Orders.ShoppingCart.IsSpecialOrder = new UtilityBLL(null).ConvertToBoolean(Request.Form["SpecialOrder"]);    
            }
            if (Request.Form["SpecialDeliveryDate"] != null && Request.Form["SpecialDeliveryDate"] != string.Empty)
            {
                Customer.Orders.ShoppingCart.SpecialDeliveryDate = Convert.ToDateTime(Request.Form["SpecialDeliveryDate"]);    
            }
            else
            {
                Customer.Orders.ShoppingCart.SpecialDeliveryDate = null;
            }
            return Json(new { Result="Success" });
        }


        public ActionResult LogOut()
        {
            Session.RemoveAll();
            Customer = null;
            return Redirect(_iSolutionURI + "/index?act=out");
        }


        [HttpPost]
        public ActionResult UpdateOrders(CustomerBLL Cust)
        {
            try
            {
                if (Customer.EntityID != null)
                {
                    Customer.Orders.ClearOrderCartHistory();
                    Customer.Orders.OrderCartHistory.OrderNo = Cust.Orders.OrderCartHistory.OrderNo;
                    Customer.Orders.OrderCartHistory.tblTransaction.Delivery_date = Convert.ToDateTime(Cust.Orders.OrderCartHistory.tblTransaction.Del_date);

                    ViewBag.SpecialRequest = Cust.Orders.OrderCartHistory.tblTransaction.Special;
                    if (!Cust.Orders.OrderCartHistory.tblTransaction.Special)
                    {
                        if (Cust.Orders.OrderCartHistory.ReferenceForDeliveryPerItem.Count() > 0)
                        {
                            new UtilityBLL(this).MessageBox("Please see reference for delivery per item. Remove all items conflict in your ordering.", "Order Invalid");
                            var list = Customer.Orders.OrderCartHistory.tblTransaction.Trans_type == "S" || ViewBag.Special != null ? Customer.SpecialDeliverySchedule : Customer.RegularDeliverySchedule;
                            var selectList = new SelectList(list, "Value", "Text", Convert.ToDateTime(Customer.Orders.OrderCartHistory.tblTransaction.Delivery_date).ToShortDateString());
                            ViewData["DateSelection"] = selectList;
                            return View("OrderDetails", Customer);
                        }
                    }
                   
                    
                    
                    if (Cust.Orders.OrderCartHistory.UpdateOrderInfo(Cust.Orders.OrderCartHistory.tblTransaction))
                    {
                        if (Customer.Orders.OrderCartHistory.UpdateOrders(Cust.Orders.OrderCartHistory.tblCart))
                        {
                            ViewBag.ModalHeader = "Orders Updated!";
                            new UtilityBLL(this).ThrowError("Orders were succesfully Updated!");
                            Customer.Orders.ClearOrderCartHistory();
                            return View("OrderHistory",
                                new OrdersBLL.OrderHistoryParam(Customer)
                                {
                                    date_from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                                    date_to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1),
                                    DateType = 1
                                });
                        }
                    }   
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
                return View("OrderHistory", new OrdersBLL.OrderHistoryParam(Customer)
                {
                    date_from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    date_to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1),
                    DateType = 1
                });
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                return View("OrderHistory",
                    new OrdersBLL.OrderHistoryParam(Customer)
                    {
                        date_from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                        date_to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1),
                        DateType = 1
                    });
            }
        }

        public ActionResult SubmitOrders()
        {
            try
            {
                OrdersBLL.OrderHistoryParam _OrderHxParam = new OrdersBLL.OrderHistoryParam(Customer);
                ViewBag.OrderisInvalid = false;
                DateTime? accdel;
                bool? ar;

                if (Customer.EntityID != null)
                {
                    if (Customer.Orders.GetCartItems.Count == 0)
                    {
                        return View("OrderHistory",
                        new OrdersBLL.OrderHistoryParam(Customer)
                        {
                            date_from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                            date_to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1),
                            DateType = 1
                        });
                    }
                    Customer.Request = Request.Form.Count > 0 ? Request : Customer.Request;
                    GetAllItemsOrdered(Request);
                    
                    if (Customer.Orders.GetCartItems.Count > 0)
                    {
                    
                        if (Customer.OrderDeliveryDate != null)
                        {

                            if (!Customer.Orders.ShoppingCart.IsSpecialOrder)
                            {
                                // Order has different leadtime
                                if (Customer.Orders.ReferenceForDeliveryPerItem.Count() > 0)
                                {
                                    new UtilityBLL(this).MessageBox("Please see reference for delivery per item. Remove all items conflict in your ordering.", "Order Invalid");
                                    return View("ShoppingCart", Customer);
                                }
                                // Order has different leadtime
                            }
                      
                       

                            //Add Order Transaction
                            if (Customer.Orders.AddOrder())
                            {
                                if (Customer.Orders.AddOrderedItems())
                                {
                                    new UtilityBLL(this).MessageBox("Orders were succesfully submitted!", "Orders complete");
                                    Customer.Orders.RemoveShoppingCartIitems();

                                
                                    string _firstdayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                                    string _lastdayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();
                                    _OrderHxParam.DateType = 1;

                                    _OrderHxParam.date_from = Convert.ToDateTime(_firstdayOfMonth).Date;
                                    _OrderHxParam.date_to = Convert.ToDateTime(_lastdayOfMonth).Date;

                                }
                                else
                                {
                                    ViewBag.ModalHeader = "Error encounters while saving";
                                    new UtilityBLL(this).ThrowError("Ordered Items was not successfully saved. Please contact HAVI CS for assistance!");
                                }
                            }
                            //Add Order Transaction
                            else
                            {
                                new UtilityBLL(this).ThrowError("Order was not successfully saved. Please contact HAVI CS for assistance!");
                                return View("ShoppingCart", Customer);
                            }
                        }
                        else
                        {
                            throw new Exception("No Default Delivery Date Found");
                        }
                    }

                    return View("OrderHistory",
                        new OrdersBLL.OrderHistoryParam(Customer)
                        {
                            date_from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                            date_to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1),
                            DateType = 1
                        });
                }
                else
                {
                    return Redirect(_iSolutionURI);
                }
            }
            catch (Exception ex)
            {
                new UtilityBLL(this).ThrowError(ex.Message);
                return View("OrderHistory",
                    new OrdersBLL.OrderHistoryParam(Customer)
                    {
                        date_from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                        date_to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1),
                        DateType = 1
                    });
            }
            
        }




        #region METHODS

        void StoreSession()
        {
            
            Customer.StoreNo = Request["storeno"].ToString();
            Customer.EntityID = Request["eid"].ToString();
            Customer.UserID = Request["UserID"].ToString();
            Customer.Login = Request["login"].ToString();
            Customer.StoreName = Request["storeName"].ToString();
            Customer.ValidEmailAddress = Request["ValidEmailAddress"].ToString();
            Customer.Company = Request["company"].ToString();


            Session["storeno"] = Customer.StoreNo;
            //Session["eid"] = _eid = Request["eid"].ToString();
            //Session["UserID"] = _UserID = Request["UserID"].ToString();
            //Session["login"] = _login = Request["login"].ToString();
            //Session["storeName"] = _storeName = Request["storeName"].ToString();
            //Session["ValidEmailAddress"] = _ValidEmailAddress = Request["ValidEmailAddress"].ToString();
            //Session["company"] = _company = Request["company"].ToString();
            //Session["StoreNameNo"] = _storeno + "-" + _storeName;
            //Session["_Customer"] = Customer;
        }

        public bool GetAllItemsOrdered(HttpRequestBase req)
        { 
            try 
	        {
                if (req.Form.Count > 0)
                {
                    //List all item code submitted
                    var _ItemsOrdered = req.Form.AllKeys.Where(x => new UtilityBLL(null).IsInteger(req.Form[x])).ToList();
                    //Convert Item Code to Array
                    var _Wrin = _ItemsOrdered.Select(x => x.Replace("row_", "")).ToArray();
                    //Remove Duplicate Items
                    Customer.Orders.ShoppingCartItems.RemoveAll(x => _Wrin.Contains(x.wrin));
                    // Get all Items Information
                    var _ItemsOrderedwInfo = (from itemmstr in dbContext.tbl_item_mstr
                                              join webitem in dbContext.tbl_items on itemmstr.Wrin equals webitem.wrin
                                              join cat in dbContext.tbl_Category on webitem.category_id equals cat.category_id
                                              where _Wrin.Contains(itemmstr.Wrin)
                                              select new
                                              {
                                                  wrin = itemmstr.Wrin,
                                                  item_desc = webitem.item_desc,
                                                  item_cat = itemmstr.item_cat,
                                                  case_pk = itemmstr.case_pk,
                                                  case_cu = itemmstr.case_cube,
                                                  case_we = itemmstr.case_wt,
                                                  std_price = webitem.std_price,
                                                  category_id = webitem.category_id,
                                                  cat_desc = cat.category_name
                                              }
                                             ).Distinct();

                    // Add new items
                    foreach (var item in _ItemsOrderedwInfo)
                    {
                        Customer.Orders.ShoppingCartItems.Add(new OrdersBLL.Cart()
                        {
                            storeno = Customer.StoreNo,
                            wrin = item.wrin.ToString().Trim(),
                            item_desc = item.item_desc.ToString().Trim(),
                            item_cat = new UtilityBLL(null).ConvertToString(item.item_cat),
                            pk = new UtilityBLL(null).ConvertToString(item.case_pk),
                            cu = new UtilityBLL(null).ConvertToString(item.case_cu),
                            wt = new UtilityBLL(null).ConvertToString(item.case_we),
                            unitprice = Convert.ToDecimal(item.std_price),
                            catid = item.category_id,
                            cat_desc = item.cat_desc,
                            qty = Convert.ToInt32(req.Form["row_" + item.wrin.ToString().Trim()])
                        });
                    }
                }
                return true;
	        }
	        catch (Exception ex)
	        {
                throw ex;
	        }
        }

        

        public DateTime GetPOStamp(string posched,string deldate)
        {

            DateTime podate;
            int rnds, r,poday;

            string[] postamp = posched.Split('-');
            podate = Convert.ToDateTime(deldate);
            int z = (int)Convert.ToDateTime(deldate).DayOfWeek;
            rnds = 0;
            r = 0;


            while (true)
            {
                if (Convert.ToInt32(postamp[z - 1]) <= 7)
                {
                    if (Convert.ToInt32(postamp[z - 1]) == (int)Convert.ToDateTime(deldate).DayOfWeek)
                    {
                        break;
                    }

                    z = z - 1;
                    if (z == 0)
                    {
                        z = 7;
                        rnds = rnds + 1;
                        if (rnds >= 3)
                        {
                            while (7 > r)
                            {
                                if (Convert.ToInt32(postamp[z-1]) != 0)
                                {
                                    break;
                                }
                                else
                                {
                                    z = z - 1;
                                }
                                r = r + 1;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(postamp[z-1]) % 7 != (int)Convert.ToDateTime(deldate).DayOfWeek)
                    {
                        z = z - 1;
                        if (z == 0)
                        {
                            z = 7;
                        }
                    }
                    else if (Convert.ToInt32(postamp[z-1]) % 7 == (int)Convert.ToDateTime(deldate).DayOfWeek)
                    {
                        break;
                    }
                }
            }

            poday = z;
            if (Convert.ToInt32(postamp[z-1]) > 7)
            {
                int Wday = Convert.ToInt32(postamp[z-1]) / 7;
                if (Convert.ToInt32(postamp[z-1]) % 7 == 0)
                {
                    Wday = Wday - 1;
                    if ((int)Convert.ToDateTime(podate).DayOfWeek == 1) { podate = podate.AddDays(-1); }
                    while (Wday > 0)
                    {
                        while ((int)podate.DayOfWeek != 1)
                        {
                            podate = podate.AddDays(-1);
                        }
                        Wday = Wday - 1;
                    }
                    while ((int)podate.DayOfWeek != poday)
                    {
                        podate = podate.AddDays(-1);
                    }
                }
                else
                {
                    while (Wday > 0)
                    {
                        while ((int)podate.DayOfWeek != 1)
                        {
                            podate = podate.AddDays(-1);
                        }
                        Wday = Wday - 1;
                        if ((int)podate.DayOfWeek == 1) { podate = podate.AddDays(-1); }
                    }
                    while ((int)podate.DayOfWeek != poday)
                    {
                        podate = podate.AddDays(-1);
                    }
                }
            }
            else
            {
                while ((int)podate.DayOfWeek != poday)
                {
                    podate = podate.AddDays(-1);
                }
            }
            return podate;
        }

        //public bool IsInDeliverySched(string wrin, string storesched, DateTime orderday, DateTime deldate)
        //{
        //    int d = (int)orderday.DayOfWeek;
        //    int[] avDdate = new int[20];
        //    DateTime NewDate = orderday;

        //    string[] delsched = storesched.Split('-');

        //    if (Convert.ToInt32(delsched[d-1]) > 7)
        //    {
        //        int Wday = (int)Convert.ToInt32(delsched[d - 1]) / 7;
        //    }
        //}


       
//        If CInt(Delsched(d - 1)) > 7 Then
//                Wday = CInt(Delsched(d - 1)) \ 7   
//                If CInt(Delsched(d - 1)) Mod 7 = 0 Then
//                    Wday = Wday - 1
//                    DelDay = 7
//                    Do While Wday > 0
//                        If Weekday(NewDate) = 7 Then NewDate = Cdate(NewDate) + 1
//                        Do While Weekday(NewDate) <> 7
//                              NewDate = Cdate(NewDate) + 1
//                        Loop
//                        Wday = Wday - 1
//                    Loop
//                Else
//                    DelDay = CInt(Delsched(d - 1)) Mod 7
//                    Do While Wday > 0
                    
//                       Do While Weekday(NewDate) <> 7
//                          NewDate = Cdate(NewDate) + 1
//                       Loop
//                       If Weekday(NewDate) = 7 Then NewDate = NewDate + 1
//                       Wday = Wday - 1
//                    Loop	
//                        Do While Weekday(NewDate) <> DelDay
//                            NewDate = Cdate(NewDate) + 1
//                        Loop
//               End If
//        Else
//            Wday = 0
//            DelDay = CInt(Delsched(d - 1))
//            Do While Weekday(NewDate) <> DelDay
//                  NewDate = Cdate(NewDate) + 1
//           Loop
//        End If
//          For y = 0 To 6
//             If CInt(Delsched(y)) > 7 Then Delsched(y) = CInt(Delsched(y) Mod 7)
//             If CInt(Delsched(y)) = 0 Then Delsched(y) = 7
//          Next
//        x = 0
//        While x <> 20
//            If Weekday(NewDate) = CInt(Delsched(0)) Or Weekday(NewDate) = CInt(Delsched(1)) Or Weekday(NewDate) = CInt(Delsched(2)) _
//                Or Weekday(NewDate) = CInt(Delsched(3)) Or Weekday(NewDate) = CInt(Delsched(4)) Or Weekday(NewDate) = CInt(Delsched(5)) _
//                Or Weekday(NewDate) = CInt(Delsched(6)) Then
//                   avDdate(x) = Cdate(NewDate)
//                   x = x + 1
//            End If
//            NewDate = Cdate(NewDate) + 1
//        Wend
//        DeliverySched = False
//        For x = 0 To UUBound(avDdate)
//            If Cdate(avDdate(x)) = Cdate(ChosenDate) Then
//                DeliverySched = True
//                Exit For
//            End If
//        Next
//'DeliverySched =  "<br>" & UUBound(avDdate) & " | " & wrin & " | " & storesched & " | " & orderday & " | " & chosenDate  & " | " & Delday & " | " & Wday & " | " & NewDate
//End Function
        #endregion











        public ActionResult FlotCharts()
        {
            return View("FlotCharts");
        }

        public ActionResult MorrisCharts()
        {
            return View("MorrisCharts");
        }

        public ActionResult Tables()
        {
            return View("Tables");
        }

        public ActionResult Forms()
        {
            return View("Forms");
        }

        public ActionResult Panels()
        {
            return View("Panels");
        }

        public ActionResult Buttons()
        {
            return View("Buttons");
        }

        public ActionResult Notifications()
        {
            return View("Notifications");
        }

        public ActionResult Typography()
        {
            return View("Typography");
        }

        public ActionResult Icons()
        {
            return View("Icons");
        }

        public ActionResult Grid()
        {
            return View("Grid");
        }

        public ActionResult Blank()
        {
            return View("Blank");
        }

        




        #region CLASSES
        

        #endregion
    }
}