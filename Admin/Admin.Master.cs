using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin
{
    public partial class Admin : MasterPage
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LokasyonlariVer();
                lblLocation.Text = Session["currentPath"].ToString();
                Page.Title = "eNroll Web Çözümleri";
            }
        }

        private void LokasyonlariVer()
        {
            var Lokasyolar = Veriler.MenuLokasyonlari.ToList();
            RepeaterLokasyonlar.DataSource = Lokasyolar;
            RepeaterLokasyonlar.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Cookies["KurumsalCookie"].Expires = DateTime.Now.AddMinutes(-1);
            Response.Redirect("~/Default.aspx");
        }
    }
}