﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaviOnlineOrdering2018.DAL;
using System.Data;
using System.Web.SessionState;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;


namespace HaviOnlineOrdering2018.BLL
{
    public class OrdersBLL : IDisposable
    {

        #region VARIABLES
        private CustomerBLL Customer;
        private string mOrderNo = string.Empty;
        public string OrderNo { get{ return mOrderNo;   } set{ this.mOrderNo = value; } }

        private List<Cart> _ShoppingCartItems;
        
        private List<OrderedItemQty> _ListOrderedItemQty;
        private cShoppingCart _ShoppingCart;
        //private List<cReferenceForDeliveryPerItem> _ReferenceForDeliveryPerItem;

        public string SearchValue { get; set; }

        #endregion

        public OrdersBLL(CustomerBLL cust)
        {
            Customer = cust;
        }

        

        public cShoppingCart ShoppingCart
        {
            get {
                if (this._ShoppingCart == null)
                {
                    this._ShoppingCart = new cShoppingCart();
                }
                return this._ShoppingCart;
            }
        }

        public List<Cart> ShoppingCartItems
        {
            get
            {
                if (this._ShoppingCartItems == null)
                {
                    this._ShoppingCartItems = new List<Cart>();
                }
                return this._ShoppingCartItems;
            }
        }

        public List<cReferenceForDeliveryPerItem> ReferenceForDeliveryPerItem
        {
            get
            {

              
                //List<cReferenceForDeliveryPerItem> mReferenceForDeliveryPerItem = new List<cReferenceForDeliveryPerItem>();
                //foreach (tbl_Cart item in this.mtblCart)
                //{
                //    var _suggestdeldate = mCustomer.Orders.ItemSuggestedDelSched(item.wrin);
                //    DateTime? _deldate = Convert.ToDateTime(mCustomer.Orders.OrderCartHistory.mtblTransaction.Del_date);

                //    if (Convert.ToDateTime(_deldate).ToShortDateString() != Convert.ToDateTime(_suggestdeldate).ToShortDateString())
                //    {
                //        mReferenceForDeliveryPerItem.Add(new cReferenceForDeliveryPerItem()
                //        {
                //            ItemCode = item.wrin,
                //            ItemDescription = item.iteminfo.Item_desc,
                //            SystemSuggestedDelivery = _suggestdeldate
                //        });
                //    }
                //}
                //return mReferenceForDeliveryPerItem;
              

               
                List<cReferenceForDeliveryPerItem> mReferenceForDeliveryPerItem = new List<cReferenceForDeliveryPerItem>();
                foreach (Cart item in Customer.Orders.ShoppingCartItems)
                {
                    var _suggestdeldate = ItemSuggestedDelSched(item.wrin);
                    DateTime? _deldate = Customer.OrderDeliveryDate;

                    if (Customer.Request != null)
                    {
                        if (Customer.Request.Form["IsSpecialOrder"] == null)
                        {
                            _deldate = Customer.OrderDeliveryDate;
                        }
                        else
                        {
                            _deldate = new UtilityBLL(null).ConvertToBoolean(Customer.Request["IsSpecialOrder"]) ? Convert.ToDateTime(Customer.Request["SpecialDeliveryDate"]) : Customer.OrderDeliveryDate;
                        }
                        
                    }


                    if (Convert.ToDateTime(_deldate).ToShortDateString() != Convert.ToDateTime(_suggestdeldate).ToShortDateString())
                    {
                        mReferenceForDeliveryPerItem.Add(new cReferenceForDeliveryPerItem()
                        {
                            ItemCode = item.wrin,
                            ItemDescription = item.item_desc,
                            SystemSuggestedDelivery = _suggestdeldate
                        });
                    }
                }
                
                return mReferenceForDeliveryPerItem;
            }
        }

        public bool CartItemsHasDiffLeadTime
        {
            get
            {
                var leaditem = this.ShoppingCartItems.GroupBy(x => x.Leadtime).ToList();
                return leaditem.Count > 1;
            }
        }


        public List<OrderedItemQty> ListOrderedItemQty
        {
            get
            {
                if (this._ListOrderedItemQty == null)
                {
                    this._ListOrderedItemQty = new List<OrderedItemQty>();
                }
                return this._ListOrderedItemQty;
            }
        }

        public void RemoveShoppingCartIitems()
        {
            _ShoppingCartItems = null;
            _ShoppingCart = null;
        }
        public void RemoveListOrderedItemQty()
        {
            _ListOrderedItemQty = null;
        }

        private OrderHistory mOrderCartHistory;
        public OrderHistory OrderCartHistory
        { 
            get
            {
                try
                {
                    if (mOrderCartHistory == null)
                    {
                        return mOrderCartHistory = new OrderHistory(this.Customer);
                    }
                    
                    return mOrderCartHistory;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<ItemWithConversion> ItemWithConversion
        {
            get 
            {
                using (HAVIEntities _entity = new HAVIEntities())
                {
                    return _entity.ItemWithConversions.AsEnumerable().ToList();
                }
            }
        }

        public void ClearOrderCartHistory()
        {
            this.mOrderCartHistory = null;
        }

        public decimal ShoppingCartTotalQty
        { 
            get{
                decimal _total = 0;
                //_total =  ShoppingCartItems.Select(x => x.totalprice == null ? 0 : x.totalprice).ToList().Sum();
        

            return _total;
            }
        }


        public List<Cart> UploadedOrders()
        {
            try
            {
                if (_ListOrderedItemQty == null)
                {
                    return null;
                }

                List<Cart> lstStoreItems = new List<Cart>();
                List<vw_AllowedItemsOnStore> dt = new List<vw_AllowedItemsOnStore>();
                string[] mWrin = _ListOrderedItemQty.Select(x => x.ItemCode).ToArray();


                using (HAVIEntities _context = new HAVIEntities())
                {
                    dt = _context.vw_AllowedItemsOnStore.Where(x => x.store_no == Customer.StoreNo && mWrin.Contains(x.wrin)).OrderBy(o => o.wrin).ToList();

                    if (dt.Count > 0)
                    {
                        foreach (var item in dt)
                        {
                            var mmwrin = item.wrin.ToString().Trim();
                            var mqty = _ListOrderedItemQty.Where(x => x.ItemCode == mmwrin).Select(y => y.Qty).FirstOrDefault();
                            lstStoreItems.Add(new OrdersBLL.Cart()
                            {
                                cu =  item.case_cu.ToString().Trim() //item["case_cu"].ToString().Trim()
                                ,
                                pk = item.case_pk.ToString().Trim() //item["case_pk"].ToString().Trim()
                                ,
                                wt = item.case_we.ToString().Trim() // item["case_we"].ToString().Trim()
                                ,
                                catid =  Convert.ToInt32(item.category_id) // Convert.ToInt32(item["category_id"])
                                ,
                                item_desc = item.item_desc.ToString().Trim() //item["item_desc"].ToString().Trim()
                                ,
                                ownerid = Convert.ToInt32(item.owner_id) //Convert.ToInt32(item["owner_id"].ToString().Trim())
                                ,
                                unitprice = Convert.ToDecimal(item.std_price) //Convert.ToDecimal(item["std_price"].ToString().Trim())
                                ,
                                uom = item.uom.ToString().Trim() //item["uom"].ToString().Trim()
                                ,
                                wrin = item.wrin.ToString().Trim() //item["wrin"].ToString().Trim()
                                ,
                                qty = Convert.ToInt32(mqty)
                                ,
                                inners = Convert.ToInt32(mqty)
                            });
                        }
                    }
                }

                return lstStoreItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region CLASSES

        public partial class cShoppingCart
        {


            private bool mIsSpecialOrder = false;
            public bool IsSpecialOrder 
            { 
                get { return mIsSpecialOrder; }
                set { this.mIsSpecialOrder = value; }
            }
            private DateTime? mSpecialDeliveryDate = null;
            public DateTime? SpecialDeliveryDate 
            { 
                get { return this.mSpecialDeliveryDate.HasValue ? this.mSpecialDeliveryDate : null; } 
                set { this.mSpecialDeliveryDate = value; } 
            }
            public bool StandingOrder { get; set; }

            private List<cTotalPerCategory> mTotalPerCategory;

            public List<cTotalPerCategory> vTotalPerCategory
            {
                get {
                    if (mTotalPerCategory == null)
                    {
                        mTotalPerCategory = new List<cTotalPerCategory>();
                    }
                    return mTotalPerCategory;
                }
            }

            //public List<cTotalPerCategory> TotalPerCategory
            //{
            //    get {

            //        foreach (var item in )
            //        {
                        
            //        }
                    
            //    }
            //}
        }

        public partial class OrderedItemQty
        {
            public string ItemCode { get; set; }
            public int Qty { get; set; }
        }

        public class cTotalPerCategory
        {
            public string CategoryName { get; set; }
            public int Qty { get; set; }
            public decimal CaseCube { get; set; }
            public decimal CaseWeight { get; set; }
        }

        public partial class Cart
        {
            public string storeno { get; set; }
            public int ownerid { get; set; }
            public string item_cat { get; set; }
            public int catid { get; set; }
            public string cat_desc { get; set; }
            public string wrin { get; set; }
            public string item_desc { get; set; }

            public string pk { get; set; }
            public string cu { get; set; }
            public string wt { get; set; }
            public string uom { get; set; }
            public int qty { get; set; }
            public int inners { get; set; }
            public decimal unitprice { get; set; }
            //public string avgorderqty { get; set; }
            public decimal? totalprice { get; set; }

            public string downloaded { get; set; }
        
            public string Leadtime
            {
                get
                {
                    try
                    {
                        using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
                        {
                            SPContext.SPName = "sp_GetItemLeadtime";
                            SPContext.SPParameters = new List<object>[] { new List<object> { "@wrin", wrin },
                                                                          new List<object> { "@storeno", storeno }
                                        };
                        DataTable _dt = SPContext.Execute();

                            return _dt.Rows.Count > 0 ? new UtilityBLL(null).ConvertToString(_dt.Rows[0]["Leadtime"].ToString()) : "";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                } 
            }
        }

        public partial class OrderHistory
        {
            private CustomerBLL mCustomer;
            public string OrderNo { get; set; }
            public OrderHistory(CustomerBLL cust)
            {
                mCustomer = cust;
            }

            public bool Initialized = false;
          

            public bool HasDownloadedItem
            {
                get {
                        if (this.tblCart.Count() == 0) 
                        { 
                            return false; 
                        }
                    var _ditems = this.mtblCart.Where(x => (x.downloaded == null ? "" : x.downloaded.ToString().Trim()) == "Y");

                        return _ditems.Count() > 0;
                }
            }

            private tbl_Transaction mtblTransaction = null;
            public tbl_Transaction tblTransaction
            {
                get 
                {
                    if (mtblTransaction == null)
                    {
                        var iOrderno = Convert.ToInt32(OrderNo);
                        using (HAVIEntities _context = new HAVIEntities())
                        {
                            mtblTransaction = _context.tbl_Transaction.Where(x => x.Order_no == iOrderno).FirstOrDefault();
                        }
                        mCustomer.StoreNo = mtblTransaction.Store_no.Trim();
                    }
                    return mtblTransaction;
                }
            }

            private List<tbl_Cart> mtblCart = null;
            public List<tbl_Cart> tblCart
            {
                get 
                {
                    if (mtblCart == null)
                    {
                        using (HAVIEntities _context = new HAVIEntities())
                        {
                            this.mtblCart = _context.tbl_Cart.Where(x => x.cart_id == this.tblTransaction.Cart_id).ToList();
                        }
                    }
                    return mtblCart;
                }
            }

            public List<cReferenceForDeliveryPerItem> ReferenceForDeliveryPerItem
            {
                get
                {
                    List<cReferenceForDeliveryPerItem> mReferenceForDeliveryPerItem = new List<cReferenceForDeliveryPerItem>();
                    foreach (tbl_Cart item in this.mtblCart)
                    {
                        var _suggestdeldate = mCustomer.Orders.ItemSuggestedDelSched(item.wrin);
                        DateTime? _deldate = Convert.ToDateTime(mCustomer.Orders.OrderCartHistory.mtblTransaction.Del_date);

                        if (Convert.ToDateTime(_deldate).ToShortDateString() != Convert.ToDateTime(_suggestdeldate).ToShortDateString())
                        {
                            mReferenceForDeliveryPerItem.Add(new cReferenceForDeliveryPerItem()
                            {
                                ItemCode = item.wrin,
                                ItemDescription = item.iteminfo.Item_desc,
                                SystemSuggestedDelivery = _suggestdeldate
                            });
                        }
                    }
                    return mReferenceForDeliveryPerItem;
                }
            }

            public bool RemoveItem(string wrin)
            {
                try
                {
                    using (HAVIEntities db = new HAVIEntities())
                    {
                        tbl_Cart mC = db.tbl_Cart.Where(x => x.cart_id == mtblTransaction.Cart_id && x.wrin == wrin).FirstOrDefault();
                        if (mC != null)
                        {
                            this.mtblCart.Remove(mtblCart.Where(x => x.cart_id == mtblTransaction.Cart_id && x.wrin.Trim() == wrin.Trim()).FirstOrDefault());
                            db.tbl_Cart.Remove(mC);
                            db.SaveChanges();
                        }
                       
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public bool RemoveOrderNo(int? orderno)
            {
                try
                {
                    using (HAVIEntities db = new HAVIEntities())
                    {
                        tbl_Transaction Order = db.tbl_Transaction.Where(x => x.Order_no == orderno).FirstOrDefault();
                        db.tbl_Transaction.Remove(Order);

                        List<tbl_Cart> mC = db.tbl_Cart.Where(x => x.cart_id == Order.Cart_id).ToList();
                        
                        foreach (tbl_Cart cart in mC)
	                    {
                            db.tbl_Cart.Remove(cart);
	                    }
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public bool UpdateOrderInfo(tbl_Transaction mOrder)
            {
                try
                {
                  
                    using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
                    {
                        SPContext.SPName = "sp_UpdateOrderInfo";
                        SPContext.SPParameters = new List<object>[] {   new List<object> { "@Special", mOrder.Special },
                                                                        new List<object> { "@DeliveryDate", mOrder.Del_date },
                                                                        new List<object> { "@OrderNo", mOrder.Order_no }
                                                                    };
                        DataTable _dt = SPContext.Execute();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public bool UpdateOrders(List<tbl_Cart> mCart)
            {
                try
                {
                    foreach (tbl_Cart item in mCart)
                    {
                        using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
                        {
                            SPContext.SPName = "sp_UpdateOrderedQty";
                            SPContext.SPParameters = new List<object>[] { new List<object> { "@cart_id", item.cart_id },
                                                                          new List<object> { "@wrin", item.wrin },
                                                                          new List<object> { "@qty", item.qty }
                                                                        };
                            DataTable _dt = SPContext.Execute();
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }




        }


        public partial class OrderHistoryParam
        {
            [Display(Name = "Date From")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime date_from { get; set; }


            [Display(Name = "Date To")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime date_to { get; set; }

            public int DateType { get; set; }
            public bool IsOrderHistory = false;

            private CustomerBLL mCustomer = null;
            public OrderHistoryParam(CustomerBLL cust)
            {
                mCustomer = cust;
            }

            private List<tbl_Transaction> mStoreOrderHistory = null;

            public List<tbl_Transaction> StoreOrderHistory
            {
                get
                {
                    try
                    {
                        if (mStoreOrderHistory == null)
                        {
                            mStoreOrderHistory = new List<tbl_Transaction>();
                            using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
                            {
                                SPContext.SPName = "sp_order_OrderHistory";
                                SPContext.SPParameters = new List<object>[] { 
                                                    new List<object> { "@StoreNo", mCustomer.StoreNo},
                                                    new List<object> { "@Datetype", this.DateType},
                                                    new List<object> { "@IsOrderHistory", this.IsOrderHistory},
                                                    new List<object> { "@DateFrom", this.date_from},
                                                    new List<object> { "@DateTo", this.date_to} 
                                                    };
                                DataTable _dt = SPContext.Execute();

                                foreach (DataRow item in _dt.Rows)
                                {
                                    DateTime updatedate;
                                    DateTime? dateValue = DateTime.TryParse(item["ModifiedDate"].ToString(), out updatedate) ? updatedate : (DateTime?)null;
                                    mStoreOrderHistory.Add(new tbl_Transaction()
                                    {
                                        Order_no = new UtilityBLL(null).ConvertToInt(item["OrderNo"].ToString()),
                                        refpo = item["refpo"].ToString().Trim(),
                                        Trans_date = Convert.ToDateTime(item["OrderDate"].ToString()),
                                        Delivery_date = Convert.ToDateTime(item["DeliveryDate"].ToString()),
                                        Trans_type = item["Type"].ToString().Trim(),
                                        Trans_status = item["Status"].ToString().Trim(),
                                        careof = item["careof"].ToString().Trim(),
                                        update_date = dateValue
                                    }
                                    );
                                }
                            }
                        }
                        return mStoreOrderHistory;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public partial class cReferenceForDeliveryPerItem
        {
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string SystemSuggestedDelivery { get; set; }
        }



        #endregion


#region METHODS

        public List<tbl_Cart> GetCartItems
        {
            get
            {
                try
                {
                    List<tbl_Cart> lCartItems = new List<tbl_Cart>();

                    foreach (OrdersBLL.Cart item in Customer.Orders.ShoppingCartItems)
                    {
                        lCartItems.Add(new tbl_Cart()
                        {
                            cart_id = Customer.CartID,
                            wrin = item.wrin,
                            price = item.unitprice,
                            qty = item.qty,
                            category_id = item.catid,
                            delivered_qty = 0,
                            downloaded = "N"
                        });
                    }

                    return lCartItems;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
                //tbl_Cart mCartItems = new tbl_Cart();



                
            }
        }

        public bool AddOrder()
        {
            bool _success = false;
            try
            {
                if (Customer.Request.Form.Count > 0)
                {

                    using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
                    {
                        SPContext.SPName = "sp_InsertUpdateOrder";
                        SPContext.SPParameters = new List<object>[] { 
                                                    new List<object> { "@Action", "C"},
                                                    new List<object> { "@Custid", Customer.CustomerInfo.custid},
                                                    new List<object> { "@Store_no", Customer.StoreNo},
                                                    new List<object> { "@Trans_type", new UtilityBLL(null).ConvertToBoolean(Customer.Request["IsSpecialOrder"]) ? "S" : "R"},
                                                    new List<object> { "@Delivery_date", new UtilityBLL(null).ConvertToBoolean(Customer.Request["IsSpecialOrder"]) ? Convert.ToDateTime(Customer.Request["SpecialDeliveryDate"]) : Customer.OrderDeliveryDate},
                                                    new List<object> { "@Cart_id", Customer.CartID},
                                                    new List<object> { "@Trans_status", "N"},
                                                    new List<object> { "@owner_id",  Customer.ownerid}
                                                    };
                        DataTable _dt = SPContext.Execute();
                        return new UtilityBLL(null).ConvertToBoolean(_dt.Rows[0]["Result"]);
                    }
                }

                return _success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteOrderedItems()
        {
            bool _success = false;
            try
            {
                foreach (tbl_Cart item in GetCartItems)
                {
                    using (HAVIEntities _context = new HAVIEntities())
                    {
                        _context.tbl_Cart.Add(item);
                        _context.SaveChanges();
                    }
                }
                _success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _success;
        
        }

        public bool AddOrderedItems()
        {
            bool _success = false;
            try
            {
                foreach (tbl_Cart item in GetCartItems)
                {
                    using (HAVIEntities _context = new HAVIEntities())
                    {
                        _context.tbl_Cart.Add(item);
                        _context.SaveChanges();
                    }
                }
                _success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _success;
        }

        public DateTime? GetPoSchedule(string wrin)
        {
            using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
            {
                SPContext.SPName = "sp_StorePOSchedule";
                SPContext.SPParameters = new List<object>[] { 
                                                    new List<object> { "@store_no", Customer.StoreNo},
                                                    new List<object> { "@wrin", wrin}
                                                    };
                DataTable _dt = SPContext.Execute();
                if (_dt.Rows.Count > 0)
                {
                    return Convert.ToDateTime(_dt.Rows[0]["PoDate"]);
                }
                return null;
            }
        }

        public bool? DDItemsHasDeliverySched(string wrin,DateTime OrderDay)
        { 
            using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
            {
                SPContext.SPName = "sp_DDItemsHasDeliverySched";
                SPContext.SPParameters = new List<object>[] { 
                                                    new List<object> { "@store_no", Customer.StoreNo},
                                                    new List<object> { "@wrin", wrin},
                                                    new List<object> { "@Orderday", OrderDay}
                                                    };
                DataTable _dt = SPContext.Execute();
                if (_dt.Rows.Count > 0)
                {
                    return new UtilityBLL(null).ConvertToBoolean(_dt.Rows[0]["DDItemsHasDeliverySched"]);
                }
            }
            return null;
        }

        public DateTime? GetAcceptedDeliverySched(string wrin, DateTime OrderDay)
        {
            using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
            {
                SPContext.SPName = "sp_GetAcceptedDeliverySched";
                SPContext.SPParameters = new List<object>[] { 
                                                    new List<object> { "@store_no", Customer.StoreNo},
                                                    new List<object> { "@wrin", wrin},
                                                    new List<object> { "@Orderday", OrderDay}
                                                    };
                DataTable _dt = SPContext.Execute();
                if (_dt.Rows.Count > 0)
                {
                    return Convert.ToDateTime(_dt.Rows[0]["AcceptedDeliverySched"]);
                }
            }
            return null;
        }

        public string ItemSuggestedDelSched(string wrin)
        {
            using (StoredProcedureExecution SPContext = new StoredProcedureExecution())
            {
                SPContext.SPName = "sp_ItemSuggestedDelSched";
                SPContext.SPParameters = new List<object>[] { 
                                                    new List<object> { "@store_no", Customer.StoreNo},
                                                    new List<object> { "@wrin", wrin}
                                                    };
                DataTable _dt = SPContext.Execute();
                if (_dt.Rows.Count > 0)
                {
                    return Convert.ToDateTime(_dt.Rows[0]["DeliveryDate"]).ToShortDateString();
                }
                return string.Empty;
            }
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
#endregion


    }
}