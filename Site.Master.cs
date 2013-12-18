using System;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal
{
    public partial class Site : MasterPage
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SiteBilgileriFooterVer();
                TextBoxArama.Text = "Ne aramıştınız?";
            }
            //string KullaniciIp = HttpContext.Current.Request.UserHostAddress;
            //string Ip = "212.175.222.97";
            //if ((KullaniciIp == Ip) || (KullaniciIp.StartsWith("94.54")))
            //{
            //    form1.Action = Request.RawUrl;
            //    if (!IsPostBack)
            //    {
            //        SiteBilgileriFooterVer();
            //        TextBoxArama.Text = "Ne aramıştınız?";
            //    }
            //}
            //else
            //{
            //    Response.Redirect("http://www.enroll.com.tr/");
            //}
        }

        private void SiteBilgileriFooterVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            SiteBilgileri SB = Veriler.SiteBilgileri.Where(p => p.DilId == DilId).First();
            Page.Title = SB.PageTitle;
            Page.MetaDescription = SB.Description;
            Page.MetaKeywords = SB.Keywords;
            LabelSiteBilgileriFooter.Text = SB.Footer;
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
            ArananKelime = ArananKelime.Replace("?", "");
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

        protected void ImageButtonAra_Click(object sender, ImageClickEventArgs e)
        {
            string ArananKelime = QueryStringeCevir(TextBoxArama.Text);
            TextBoxArama.Text = "Ne aramıştınız?";
            Response.Redirect("/arama/0/" + ArananKelime + "/1");
        }
    }
}