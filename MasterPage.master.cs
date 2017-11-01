using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Common.nullcheck(Common.GetSession("CUST_ID")) != "")
        {
            Load_page(Common.nullcheck(Common.GetSession("CUST_ID")));
        }
        else
        {            
            Response.Redirect("/common/Close.aspx?gb=1");
        }

        if (_isRefresh) // 새로고침시 두번 처리 방지
        {
            
            Response.Redirect(Request.RawUrl);
        }

    }

    private void Load_page(string Cust_id)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["HbDbConnection"]);
        con.Open();
        string sqlText = "SELECT CUST_NM FROM CUST_MAIN WHERE CUST_ID = '" + Cust_id + "' ";

        SqlCommand cmd = new SqlCommand(sqlText, con);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            this.Cust_Nm.Text = Convert.ToString(reader["CUST_NM"]);
        }
        reader.Close();
        con.Close();
    }


    private bool _refreshState;
    private bool _isRefresh;

    public bool IsRefresh
    { 
        get { return _isRefresh; }
    }

    protected override void LoadViewState(object savedState)
    {
        object[] allStates = (object[])savedState;
        base.LoadViewState(allStates[0]);
        _refreshState = (bool)allStates[1];
        if (Common.nullcheck(Common.GetSession("CUST_ID")) == "")
        {
            Response.Redirect("/common/Close.aspx?gb=1");
            Response.End();
        }
        _isRefresh = _refreshState == (bool)Session["__ISREFRESH"];
    }

    protected override object SaveViewState()
    {
        Session["__ISREFRESH"] = _refreshState;
        object[] allStates = new object[2];
        allStates[0] = base.SaveViewState();
        allStates[1] = !_refreshState;
        return allStates;
    }



}
