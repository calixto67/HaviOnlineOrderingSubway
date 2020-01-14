using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using HaviOnlineOrdering2018.DAL;
using System.Web.SessionState;
using System.ComponentModel.DataAnnotations;

namespace HaviOnlineOrdering2018.BLL
{
    public class ProductsBLL : IDisposable 
    {

        #region VARIABLE

        private CustomerBLL Customer;
        
        

        

        #endregion


        public ProductsBLL(CustomerBLL _cust)
        {
            Customer = _cust;
        }

        
        
        
        #region CLASSES

        public partial class ProductsSubCatModel
        {
            public int categoryid { get; set; }
            public string Subcatid { get; set; }
            public string Description { get; set; }
        }

        
        
        #endregion



        #region METHODS


        #region IDISPOSABLE

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



        #endregion

    }
}