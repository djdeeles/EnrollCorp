using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class HaberGrubuUyeligiKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Temizle();
            }
        }

        private void Temizle()
        {
            TextBoxHaberGrubuUyeligi.Text = "E-posta adresiniz.";
            LabelHaberGrubuUyeligiMesaj.Text = string.Empty;
        }

        protected void LinkButtonHaberGrubuUyeligiKayit_Click(object sender, EventArgs e)
        {
            try
            {
                HaberGrubuUyeligi H =
                    Veriler.HaberGrubuUyeligi.Where(p => p.EPosta == TextBoxHaberGrubuUyeligi.Text).FirstOrDefault();
                if (H == null)
                {
                    HaberGrubuUyeligi HGU = new HaberGrubuUyeligi();
                    HGU.Id = Guid.NewGuid();
                    HGU.EPosta = TextBoxHaberGrubuUyeligi.Text;
                    HGU.Durum = true;
                    HGU.KayitTarihi = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
                    Veriler.HaberGrubuUyeligi.AddObject(HGU);
                    Veriler.SaveChanges();
                    Temizle();
                    LabelHaberGrubuUyeligiMesaj.Text = "Kayıt edildi.";
                }
                else
                {
                    Temizle();
                    LabelHaberGrubuUyeligiMesaj.Text = "Kayıt edildi.";
                }
            }
            catch (SqlException Hata)
            {
                LabelHaberGrubuUyeligiMesaj.Text = "Hata oluştu!";
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }
    }
}