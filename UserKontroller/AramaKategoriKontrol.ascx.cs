using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnrollKurumsal.UserKontroller
{
    public partial class AramaKategoriKontrol : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KategorileriVer();
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["code"] != null)
                    {
                        TextBoxAra.Text = QueryStringeTersCevir(Request.QueryString["code"]);
                    }
                    else
                    {
                        TextBoxAra.Text = "Ne aramıştınız?";
                    }
                    if (Request.QueryString["Kategori"] != null)
                    {
                        DropDownListKategoriler.SelectedValue = Request.QueryString["Kategori"];
                    }
                    else
                    {
                        DropDownListKategoriler.SelectedValue = "0";
                    }
                }
                else
                {
                    TextBoxAra.Text = "Ne aramıştınız?";
                    DropDownListKategoriler.SelectedValue = "0";
                }
            }
        }

        private void KategorileriVer()
        {
            DropDownListKategoriler.Items.Add(new ListItem("Tümü", "0"));
            DropDownListKategoriler.Items.Add(new ListItem("İçerik", "1"));
            DropDownListKategoriler.Items.Add(new ListItem("Haberler", "2"));
            DropDownListKategoriler.Items.Add(new ListItem("Duyurular", "3"));
            DropDownListKategoriler.Items.Add(new ListItem("Etkinlikler", "4"));
            DropDownListKategoriler.Items.Add(new ListItem("İhaleler", "5"));
        }

        public string QueryStringeCevir(string ArananKelime)
        {
            ArananKelime = ArananKelime.Replace(" ", "-");
            ArananKelime = ArananKelime.Replace("ı", "i_");
            ArananKelime = ArananKelime.Replace("I", "_i");
            ArananKelime = ArananKelime.Replace("ğ", "g_");
            ArananKelime = ArananKelime.Replace("Ğ", "_g");
            ArananKelime = ArananKelime.Replace("ü", "u_");
            ArananKelime = ArananKelime.Replace("Ü", "_u");
            ArananKelime = ArananKelime.Replace("ş", "s_");
            ArananKelime = ArananKelime.Replace("Ş", "_s");
            ArananKelime = ArananKelime.Replace("ö", "o_");
            ArananKelime = ArananKelime.Replace("Ö", "_o");
            ArananKelime = ArananKelime.Replace("ç", "c_");
            ArananKelime = ArananKelime.Replace("Ç", "_c");
            ArananKelime = ArananKelime.Replace("<", "");
            ArananKelime = ArananKelime.Replace(">", "");
            ArananKelime = ArananKelime.Replace(";", "");
            ArananKelime = ArananKelime.Replace(":", "");
            ArananKelime = ArananKelime.Replace("~", "");
            ArananKelime = ArananKelime.Replace(",", "");
            ArananKelime = ArananKelime.Replace("`", "");
            ArananKelime = ArananKelime.Replace("'", "");
            ArananKelime = ArananKelime.Replace("!", "");
            ArananKelime = ArananKelime.Replace("+", "");
            ArananKelime = ArananKelime.Replace("/", "");
            ArananKelime = ArananKelime.Replace(@"\", "");
            ArananKelime = ArananKelime.Replace("%", "");
            ArananKelime = ArananKelime.Replace("^", "");
            ArananKelime = ArananKelime.Replace("\"", "-");
            ArananKelime = ArananKelime.Replace("’", "-");
            ArananKelime = ArananKelime.ToLower();
            return ArananKelime;
        }

        public string QueryStringeTersCevir(string ArananKelime)
        {
            ArananKelime = ArananKelime.ToLower();
            ArananKelime = ArananKelime.Replace("u_", "ü");
            ArananKelime = ArananKelime.Replace("o_", "ö");
            ArananKelime = ArananKelime.Replace("c_", "ç");
            ArananKelime = ArananKelime.Replace("s_", "ş");
            ArananKelime = ArananKelime.Replace("i_", "ı");
            ArananKelime = ArananKelime.Replace("g_", "ğ");
            ArananKelime = ArananKelime.Replace("-", " ");
            ArananKelime = ArananKelime.ToLower();
            return ArananKelime;
        }

        protected void LinkButtonAra_Click(object sender, EventArgs e)
        {
            string ArananKelime = QueryStringeCevir(TextBoxAra.Text);
            TextBoxAra.Text = "Ne aramıştınız?";
            Response.Redirect("/arama/" + DropDownListKategoriler.SelectedValue + "/" + ArananKelime + "/1");
        }

        protected void ButtonAra_Click(object sender, EventArgs e)
        {
            string ArananKelime = QueryStringeCevir(TextBoxAra.Text);
            TextBoxAra.Text = "Ne aramıştınız?";
            Response.Redirect("/arama/" + DropDownListKategoriler.SelectedValue + "/" + ArananKelime + "/1");
        }

        protected void ImageButtonAra_Click(object sender, ImageClickEventArgs e)
        {
            string ArananKelime = QueryStringeCevir(TextBoxAra.Text);
            TextBoxAra.Text = "Ne aramıştınız?";
            Response.Redirect("/arama/" + DropDownListKategoriler.SelectedValue + "/" + ArananKelime + "/1");
        }
    }
}