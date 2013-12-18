using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class HizmetSliderKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HizmetSliderVer();
            }
        }

        private void HizmetSliderVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HS =
                Veriler.HizmetSlider.Where(p => p.DilId == DilId && p.Durum == true).OrderBy(p => p.SiraNo).ToList();
            RepeaterHizmetSlider.DataSource = HS;
            RepeaterHizmetSlider.DataBind();
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