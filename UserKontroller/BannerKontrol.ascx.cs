using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class BannerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerlariVer();
            }
        }

        private void BannerlariVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var BS = (from p in Veriler.BannerSlider
                      where p.DilId == DilId && p.Durum == true
                      orderby Guid.NewGuid()
                      select new
                                 {
                                     p.Url,
                                     p.Resim,
                                     p.BannerAdi
                                 }).Take(3).ToList();
            DataListBanner.DataSource = BS;
            DataListBanner.DataBind();
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            if (!HL.NavigateUrl.StartsWith("http"))
            {
                HL.NavigateUrl = "../" + HL.NavigateUrl.Replace(Request.Url.PathAndQuery, HL.NavigateUrl);
            }
            else
            {
                HL.Target = "_blank";
            }
        }
    }
}