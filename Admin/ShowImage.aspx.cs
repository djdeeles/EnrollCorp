using System;
using System.Web.UI;

namespace EnrollKurumsal.Admin
{
    public partial class ShowImage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Image1.ImageUrl = Request.QueryString["url"];
        }
    }
}