using System;
using System.Web;
using System.Web.UI;

namespace EnrollKurumsal
{
    public partial class ShowMyIP : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LabelIp.Text = HttpContext.Current.Request.UserHostAddress;
            }
        }
    }
}