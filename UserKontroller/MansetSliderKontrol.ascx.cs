using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class MansetSliderKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MansetSliderVer();
            }
        }

        private void MansetSliderVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var MS =
                Veriler.MansetSlider.Where(p => p.DilId == DilId && p.Durum == true).OrderBy(p => p.SiraNo).ToList();
            RepeaterMansetSlider.DataSource = MS;
            RepeaterMansetSlider.DataBind();
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