using System;
using System.Web.UI;

namespace EnrollKurumsal.Admin
{
    public partial class AdminSub : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblLocation.Text = Session["currentPath"].ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}