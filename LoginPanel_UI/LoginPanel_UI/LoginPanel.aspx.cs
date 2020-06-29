using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginPanel_UI
{
    public partial class LoginPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {        
            string result = "";
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            try 
            {
                //Password hashleniyor.
                password = Hashing.getSHA1Hash(password);
                //LoginService nesnesi oluşturuluyor.
                LoginServiceReference.LoginServiceSoapClient soapClient = new LoginServiceReference.LoginServiceSoapClient();
                //LoginService SingIn web metodu çağrılıyor.
                result = soapClient.SignIn(username, password);

                if (result != "NOT")
                {
                    switch (result)
                    {
                        case "OK":
                            result = "Giriş Yapıldı.Hoşgeldiniz " + username;
                            break;
                        case "BLOCK":
                            result = "Hesabınız bloke edilmiş.";
                            break;
                        case "CANCEL":
                            result = "Hesabınız iptal edilmiş";
                            break;
                    }
                }
                else 
                {
                    result = "Kullanıcı Adı veya parola yanlış";
                }
                lblResult.Text = result;
                ClearComponents();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('Hata:" + ex.Message+ "')</script>");
            }
        }

        private void ClearComponents()
        {
           txtUsername.Text="";
           txtPassword.Text="";
        }
    }
}