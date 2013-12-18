using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class BelediyeHizmetleriKategorileriKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuKategoriler.Items.Clear();
                KategorileriVer();
                BelediyeHizmetleriKategorileriVer(MenuKategoriler.Items);
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["arama"] != null)
                    {
                        TextBoxAra.Text = QueryStringeTersCevir(Request.QueryString["arama"]);
                    }
                    else
                    {
                        TextBoxAra.Text = "Ne aramıştınız?";
                    }
                }
                else
                {
                    TextBoxAra.Text = "Ne aramıştınız?";
                }
            }
        }

        private void BelediyeHizmetleriKategorileriVer(MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var BHKList =
                Veriler.BelediyeHizmetleriKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(
                    p => p.SiraNo).ToList();
            foreach (BelediyeHizmetleriKategorileri BHK in BHKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../belediyehizmetleri/" + BHK.Id.ToString() + "/" +
                                 MenuUrl.MenuUrlDuzenle(BHK.KategoriAdi) + "/1";
                MI.Text = BHK.KategoriAdi;
                Items.Add(MI);
            }
        }

        private void KategorileriVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var BHK =
                Veriler.BelediyeHizmetleriKategorileri.Where(p => p.DilId == DilId && p.Durum == true).OrderBy(
                    p => p.SiraNo).ToList();
            DropDownListKategoriler.DataTextField = "KategoriAdi";
            DropDownListKategoriler.DataValueField = "Id";
            DropDownListKategoriler.DataSource = BHK;
            DropDownListKategoriler.DataBind();
            DropDownListKategoriler.Items.Insert(0, new ListItem("Tümü", "0"));
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

        protected void ImageButtonAra_Click(object sender, ImageClickEventArgs e)
        {
            string ArananKelime = QueryStringeCevir(TextBoxAra.Text);
            TextBoxAra.Text = "Ne aramıştınız?";
            Response.Redirect("/belediyehizmetleriara/" + DropDownListKategoriler.SelectedValue + "/" + ArananKelime +
                              "/1");
        }
    }
}