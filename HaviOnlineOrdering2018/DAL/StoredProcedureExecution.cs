using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace HaviOnlineOrdering2018.DAL
{
    public partial class StoredProcedureExecution : DbContext
    {
        
        public List<object>[] SPParameters { get; set; }
        public string SPName { get; set; }
        public StoredProcedureExecution()
            : base("name=StoredProcedureExecution")
        {

        }

        public virtual DataTable Execute()
        {
            DataTable _dt = new DataTable();
            SqlDataReader _dr = default(SqlDataReader);
            try
            {
                using (SqlConnection _con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HaviConnection"].ConnectionString))
                {
                    _con.Open();
                    using (SqlCommand _com = new SqlCommand(SPName, _con))
                    {
                        _com.CommandType = CommandType.StoredProcedure;
                        if (!(SPParameters == null))
                        {
                            foreach (List<object> _dict in SPParameters)
                            {
                                if (_dict[1] != null)
                                {
                                    _com.Parameters.AddWithValue((string)_dict[0], string.IsNullOrWhiteSpace(_dict[1].ToString()) ? (object)DBNull.Value : (_dict[1].ToString().ToUpper() == "FALSE" || _dict[1].ToString().ToUpper() == "TRUE") ? (_dict[1].ToString().ToUpper() == "TRUE" ? 1 : 0) : _dict[1]);
                                }
                            }
                        }
                        _com.CommandTimeout = 0;
                        _dr = _com.ExecuteReader();
                        _dt.Load(_dr);
                        _dr.Close();

                        if (_dt.SqlErrorMessage() != string.Empty)
                        {
                            throw new Exception(_dt.SqlErrorMessage());
                        }

                        return _dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }

    
}

public static class Extension
{
    public static string SqlErrorMessage(this DataTable dt)
    {
        string SqlErrorMessage = string.Empty;
        if (dt != null)
        {
            if (dt.Columns.Contains("ErrorMessage"))
            {
                SqlErrorMessage = "Error Number : " + dt.Rows[0]["ErrorNumber"].ToString() + System.Environment.NewLine +
                                "Error Severity : " + dt.Rows[0]["ErrorSeverity"].ToString() + System.Environment.NewLine +
                                "Error Procedure : " + dt.Rows[0]["ErrorProcedure"].ToString() + System.Environment.NewLine +
                                "Error Line : " + dt.Rows[0]["ErrorLine"].ToString() + System.Environment.NewLine +
                                "Error Message : " + dt.Rows[0]["ErrorMessage"].ToString();

            }
        }
        return SqlErrorMessage;
    }
}
