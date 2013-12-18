using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class BannerSliderKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerSliderVer();
            }
        }

        private void BannerSliderVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var BSList =
                Veriler.BannerSlider.Where(p => p.DilId == DilId && p.Durum == true).OrderBy(p => p.SiraNo).ToList();
            RepeaterBannerSlider.DataSource = BSList;
            RepeaterBannerSlider.DataBind();
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