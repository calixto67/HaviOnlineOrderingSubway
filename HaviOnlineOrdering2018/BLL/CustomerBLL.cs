using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaviOnlineOrdering2018.DAL;
using System.Data;
using System.Web.SessionState;
using System.ComponentModel.DataAnnotations;

namespace HaviOnlineOrdering2018.BLL
{
    public class CustomerBLL : IDisposable 
    {
        HAVIEntities dbContext = new HAVIEntities();


        #region VARIABLES
        public string StoreNo { get; set; }
        public int ownerid { get; set; }

        public string EntityID { get; set; }
        public string UserID { get; set; }
        public string Login { get; set; }
        public string StoreName { get; set; }
        public string ValidEmailAddress { get; set; }
        public string Company { get; set; }
        public string StoreNameNo { get { return StoreNo + "-" + StoreName; } }

        private string mCartID = string.Empty;

        public HttpRequestBase Request { get; set; }
        public string CartID
        {
            get {
                //string _storeno = Model.StoreNo;
                //var mCartid = _storeno.PadLeft(4, '0') + String.Format("{0:d11}", (DateTime.Now.Ticks / 10) % 1000000000);
                //ViewBag.ErrorMessage = mCartid;
                //ViewBag.JavaScriptFunction = "ShowDialog()";  
                //HaviOnlineOrdering2018.BLL.CustomerBLL mCustomerBLL = (Session["_Customer"] as HaviOnlineOrdering2018.BLL.CustomerBLL);
                bool mCartIDExist = true;
                
                while (mCartIDExist)
                {
                    if (mCartID == string.Empty)
                    {
                        mCartID = this.StoreNo.PadLeft(6, '0') + String.Format("{0:d8}", (DateTime.Now.Ticks / 10) % 1000000000);
                    }
                    var mArrCartID = dbContext.tbl_Cart.Select(x => x.cart_id).Distinct();

                    //check if CartID Generated already exists
                    if (mArrCartID.Contains(mCartID))
                    {
                        mCartID = string.Empty;
                    }
                    else
                    {
                        mCartIDExist = false;
                    }
                }
              

                return mCartID;
            }
        }
        #endregion


        public void ResetCartID()
        {
            mCartID = string.Empty;
        }
        public static CustomerBLL Customer
        {
            get 
            {
                CustomerBLL mCustomerBLL = (CustomerBLL)HttpContext.Current.Session["_Customer"];
                if (mCustomerBLL == null) 
                {
                    mCustomerBLL = new CustomerBLL();
                    HttpContext.Current.Session["_Customer"] = mCustomerBLL;
                }
                return mCustomerBLL;
            }
        }

        private ProductsBLL mProducts;
        public ProductsBLL Products
        {
            get
            {
                if (mProducts == null)
                {
                    mProducts = new ProductsBLL(this);
                }
                return mProducts;
            }
        }

        private OrdersBLL mOrders;
        public OrdersBLL Orders
        {
            get 
            {
                if (mOrders == null)
                {
                    mOrders = new OrdersBLL(this);
                }
                return mOrders;
            }
        }

        public tbl_Customer CustomerInfo
        {
            get {

                if (HttpContext.Current.Session["_CustInfo"] != null)
                {
                    return (HttpContext.Current.Session["_CustInfo"] as tbl_Customer);
                }

                tbl_Customer _CustInfo = dbContext.tbl_Customer.Where(x => x.store_no == StoreNo).ToList()[0];
                
                if (_CustInfo != null)
                {
                    return _CustInfo;
                }
                else
                {
                    return null;
                }

            }

        }
        
        private DateTime? mOrderDeliveryDate;
        public DateTime? OrderDeliveryDate
        {
            get
            {
                try
                {
                    //if (mOrderDeliveryDate == null)
                    //{
                        using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
                        {
                            SPContext.SPName = "sp_GetDeliveryDate";
                            SPContext.SPParameters = new List<object>[] { new List<object> { "@StoreNo", StoreNo } };
                            DataTable _dt = SPContext.Execute();

                            if (_dt.Rows.Count > 0)
                            {
                                mOrderDeliveryDate = Convert.ToDateTime(_dt.Rows[0]["DeliveryDate"]);
                            }
                          
                        }
                    //}
                    return mOrderDeliveryDate;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            set { mOrderDeliveryDate = value; }
        }




        public List<SelectListItem> RegularDeliverySchedule
        {
            get
            {
                List<SelectListItem> RegDelSched = new List<SelectListItem>();

                try
                {
                    using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
                    {
                        SPContext.SPName = "sp_GetDeliverySched";
                        SPContext.SPParameters = new List<object>[] { new List<object> { "@storeno", StoreNo },
                                                                     new List<object> { "@DateOrdered", Customer.Orders.OrderCartHistory.tblTransaction.Trans_date }
                                                                    };
                        DataTable _dt = SPContext.Execute();

                        
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            RegDelSched.Add(new SelectListItem()
                            {
                                Value = Convert.ToDateTime(_dt.Rows[i]["DateStamp"]).ToShortDateString(),
                                Text = Convert.ToDateTime(_dt.Rows[i]["DateStamp"]).ToShortDateString()
                            });
                        }
                        
                        return RegDelSched;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<SelectListItem> SpecialDeliverySchedule
        {
            get
            {
                List<SelectListItem> RegDelSched = new List<SelectListItem>();
                try
                {
                    using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
                    {
                        SPContext.SPName = "sp_GetDeliverySched";
                        SPContext.SPParameters = new List<object>[] { new List<object> { "@storeno", StoreNo }
                                                                    , new List<object> { "@DateOrdered", Customer.Orders.OrderCartHistory.tblTransaction.Trans_date }
                                                                    , new List<object> { "@Special", "1" } };
                        DataTable _dt = SPContext.Execute();

                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            RegDelSched.Add(new SelectListItem()
                            {
                                Value = Convert.ToDateTime(_dt.Rows[i]["DateStamp"]).ToShortDateString(),
                                Text = Convert.ToDateTime(_dt.Rows[i]["DateStamp"]).ToShortDateString()
                            });
                        }

                        return RegDelSched;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<OrdersBLL.Cart> AllowedItemsOnStore(int? catid)
        {
            try
            {
                List<OrdersBLL.Cart> lstStoreItems = new List<OrdersBLL.Cart>();
                DataTable dt = new DataTable();

                using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
                {
                    SPContext.SPName = "sp_order_AllowedItemsOnStore";
                    SPContext.SPParameters = new List<object>[] { new List<object> { "@storeno", Customer.StoreNo},
                                            new List<object> { "@ownerid", Customer.ownerid},
                                            new List<object> { "@categoryid", catid} 
                                            };
                    dt = SPContext.Execute();

                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstStoreItems.Add(new OrdersBLL.Cart()
                        {
                            cu = item["case_cu"].ToString().Trim()
                            ,
                            pk = item["case_pk"].ToString().Trim()
                            ,
                            wt = item["case_we"].ToString().Trim()
                            ,
                            catid = Convert.ToInt32(item["category_id"])
                            ,
                            item_desc = item["item_desc"].ToString().Trim()
                            ,
                            ownerid = Convert.ToInt32(item["owner_id"].ToString().Trim())
                            ,
                            unitprice = Convert.ToDecimal(item["std_price"].ToString().Trim())
                            ,
                            uom = item["uom"].ToString().Trim()
                            ,
                            wrin = item["wrin"].ToString().Trim()
                        });
                    }
                }
                //var nums = "0123456789".ToCharArray();
                //var a = lstStoreItems.OrderBy(x => x.wrin.LastIndexOfAny(nums)).ThenBy(x => x.wrin);
                return lstStoreItems.OrderBy(x => x.wrin).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
#region METHODS

        
        
#endregion 


        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        #region IDISPOSABLE

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Customer.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion


    }
}