using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaviOnlineOrdering2018.BLL
{
    public class UtilityBLL 
    {
        Controller _ctl;
        public UtilityBLL(Controller ctl)
        {
            _ctl = ctl;
        }
        public void ThrowError(string ErrorMessage)
        {
            _ctl.ViewBag.ErrorMessage = ErrorMessage;
            _ctl.ViewBag.JavaScriptFunction = "ShowDialog()";   
        }

        public void MessageBox(string Text, string caption)
        {
            _ctl.ViewBag.ModalHeader = caption;
            _ctl.ViewBag.ErrorMessage = Text;
            _ctl.ViewBag.JavaScriptFunction = "ShowDialog()";
        }

        public bool IsInteger(string str)
        {
            int val = 0;
            return Int32.TryParse(str, out val);
        }

        public decimal ConvertToDecimal(object obj)
        {
            decimal val = 0;
            if (obj == null)
            {
                return 0;
            }
            else if (Decimal.TryParse(obj.ToString(), out val))
            {
                return Convert.ToDecimal(val);
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }

        public int ConvertToInt(object obj)
        {
            int val = 0;
            if (obj == null)
            {
                return 0;
            }
            else if (Int32.TryParse(obj.ToString(), out val))
            {
                return Convert.ToInt32(val);
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public string ConvertToString(object obj)
        {
            return obj == null ? string.Empty : obj.ToString().Trim();
        }
        public bool ConvertToBoolean(object obj)
        {
            int val = 0;
            if (obj == null)
            {
                return false;
            }
            else if (obj.GetType() == typeof(int))
            {
                if (Convert.ToInt32(obj) == 1 || Convert.ToInt32(obj) == 0)
                {
                    return Convert.ToBoolean(val);     
                }
                throw new System.Exception("Could not convert object[Integer] to boolean");
            }
            else if (obj.GetType() == typeof(string))
            {
                if (Convert.ToString(obj) == "1" || Convert.ToString(obj) == "2")
                {
                    return Convert.ToBoolean(Convert.ToInt32(obj));
                }
                if (Convert.ToString(obj).ToUpper() == "ON" || Convert.ToString(obj).ToUpper() == "TRUE" ) { return true; }
                if (Convert.ToString(obj).ToUpper() == "OFF" || Convert.ToString(obj).ToUpper() == "FALSE" ) { return false; }
                throw new System.Exception("Could not convert object[String] to boolean");
            }
            else
            {
                return Convert.ToBoolean(obj);
            }
        }

        public bool IsDate(string obj)
        {
            try
            {
                DateTime dt = DateTime.Parse(obj);
                return true;

            }
            catch
            {
                return false;

            }
        }
    }
}