﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_YonetimGiris : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text == "destekenroll" && TextBox2.Text == "1357")
        {
            Session.Add("aktivasyon", 1);
            Session.Timeout = 60;
            Response.Redirect("~/admin/Yonetim.aspx");
            Label59.Text = "";
        }
        else
        {
            Label59.Text = "Hatalı giriş, Lütfen tekrar deneyiniz.";
        }
    }
}
