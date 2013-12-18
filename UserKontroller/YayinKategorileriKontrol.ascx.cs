using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class YayinKategorileriKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuKategoriler.Items.Clear();
                YayinKategorileriVer(MenuKategoriler.Items);
            }
        }

        private void YayinKategorileriVer(MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var YKList =
                Veriler.YayinKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(p => p.KategoriAdi).
                    ToList();
            foreach (YayinKategorileri YK in YKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../yayindetay/" + YK.Id.ToString() + "/" + MenuUrl.MenuUrlDuzenle(YK.KategoriAdi) +
                                 "/1";
                MI.Text = YK.KategoriAdi;
                Items.Add(MI);
            }
        }
    }
}