<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {

    }
    
    void Application_End(object sender, EventArgs e)
    {
        //  응용 프로그램 종료 시 실행되는 코드

    }

    void Application_Error(object sender, EventArgs e)
    {
        // 처리되지 않은 오류 발생 시 실행되는 코드
        Exception ex = Server.GetLastError().GetBaseException();

        string errString = "MESSAGE: " + ex.Message + 

                           "\r\nSOURCE: " + ex.Source +

                           "\r\nFORM: " + Request.Form.ToString() +

                           "\r\nQUERYSTRING: " + Request.QueryString.ToString() +

                           "\r\nTARGETSITE: " + ex.TargetSite +

                           "\r\nSTACKTRACE: " + ex.StackTrace +

                           "\r\nClientIp: " + Request.ServerVariables.Get("REMOTE_ADDR") +

                           "\r\nCUSTID: " + Common.nullcheck(Common.GetSession("CUST_ID"));
        

        Common.Log(errString);
    }

</script>
