using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LoginPanel_Host.Operation;

namespace LoginPanel_Host
{
    /// <summary>
    /// Summary description for LoginService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LoginService : System.Web.Services.WebService
    {

        [WebMethod]
        public string SignIn(string username,string password)
        {
            return LoginOperation.CompareUsernameAndPassword(username, password);
        }
    }
}
