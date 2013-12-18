using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class DillerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            var Diller = (from p in Veriler.Diller
                          where p.Durum == true
                          orderby p.Id ascending
                          select p).ToList();
            foreach (var Dil in Diller)
            {
                ImageButton Ib = new ImageButton();
                switch (Dil.Id)
                {
                    case 1:
                        Ib.ImageUrl = "../Theme/Images/tr.png";
                        break;
                    case 2:
                        Ib.ImageUrl = "../Theme/Images/en.png";
                        break;
                }
                Ib.CommandArgument = Dil.Id.ToString();
                Ib.Click += Ib_Click;
                Ib.CssClass = "Ib";
                if (EnrollContext.Current.WorkingLanguage.languageId == Dil.Id)
                {
                    Ib.BorderColor = Color.Purple;
                    Ib.BorderStyle = BorderStyle.Double;
                    Ib.BorderWidth = Unit.Pixel(3);
                }
                else
                {
                    Ib.BorderColor = Color.Purple;
                    Ib.BorderStyle = BorderStyle.Solid;
                    Ib.BorderWidth = Unit.Pixel(1);
                }
                Panel1.Controls.Add(Ib);
            }
        }

        private void Ib_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton Ib = (ImageButton) sender;
                int DilId = Convert.ToInt32(Ib.CommandArgument);
                EnrollContext.Current.WorkingLanguage.languageId = DilId;
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }
    }
}