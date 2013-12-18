using System;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal
{
    public partial class Default : Page
    {
        private EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.RawUrl != "/anasayfa")
                {
                    Response.Redirect("/anasayfa");
                }
            }
        }
    }
}