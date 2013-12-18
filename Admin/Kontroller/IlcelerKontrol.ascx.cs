using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class IlcelerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "İlçeler Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 11))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[IlId], p.[IlceAdi] from Ilceler as p where p.IlId==0 order by IlceAdi";
                }
                else
                {
                    MultiView2.ActiveViewIndex = 1;
                }
            }
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void Temizle()
        {
            TextBoxIlceAdi.Text = string.Empty;
            UlkeleriVer(DropDownListUlkeler);
            UlkeleriVer(DropDownListUlkelerGridView);
            IlleriVer(DropDownListIller, Convert.ToInt32(DropDownListUlkeler.SelectedValue));
            IlleriVer(DropDownListIllerGridView, Convert.ToInt32(DropDownListUlkeler.SelectedValue));
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void UlkeleriVer(DropDownList DropDownListUlkeler)
        {
            var UList = Veriler.Ulkeler.ToList();
            DropDownListUlkeler.DataTextField = "UlkeAdi";
            DropDownListUlkeler.DataValueField = "Id";
            DropDownListUlkeler.DataSource = UList;
            DropDownListUlkeler.DataBind();
            DropDownListUlkeler.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void IlleriVer(DropDownList DropDownListIller, int UlkeId)
        {
            var IList = Veriler.Iller.Where(p => p.UlkeId == UlkeId).ToList();
            DropDownListIller.DataTextField = "IlAdi";
            DropDownListIller.DataValueField = "Id";
            DropDownListIller.DataSource = IList;
            DropDownListIller.DataBind();
            DropDownListIller.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "İlçe Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Ilceler I;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    I = Veriler.Ilceler.Where(p => p.Id == Id).First();
                    I.IlId = Convert.ToInt32(DropDownListIller.SelectedValue);
                    I.IlceAdi = TextBoxIlceAdi.Text;
                    I.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    I.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[IlId], p.[IcelAdi] from Ilceler as p where p.IlId==" +
                        DropDownListUlkeler.SelectedValue + " order by IlceAdi";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    I = new Ilceler();
                    I.IlId = Convert.ToInt32(DropDownListIller.SelectedValue);
                    I.IlceAdi = TextBoxIlceAdi.Text;
                    I.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    I.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToIlceler(I);
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[IlId], p.[IcelAdi] from Ilceler as p where p.IlId==" +
                        DropDownListUlkeler.SelectedValue + " order by IlceAdi";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Temizle();
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Ilceler I = Veriler.Ilceler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(I);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Ilceler I = Veriler.Ilceler.Where(p => p.Id == Id).First();
                Veriler.Ilceler.DeleteObject(I);
                Veriler.SaveChanges();
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[IlId], p.[IlceAdi] from Ilceler as p where p.IlId==" + I.IlId +
                    " order by IlceAdi";
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[IlId], p.[IlceAdi] from Ilceler as p where p.IlId==" +
                    DropDownListIllerGridView.SelectedValue + " order by IlceAdi";
            }
        }

        private void Guncelle(Ilceler I)
        {
            TextBoxIlceAdi.Text = I.IlceAdi;
            HiddenFieldId.Value = I.Id.ToString();
            var U = from p in Veriler.Iller
                    join p1 in Veriler.Ulkeler
                        on p.UlkeId equals p1.Id
                    where p.Id == I.IlId
                    select new
                               {
                                   UlkeId = p1.Id
                               };
            DropDownListUlkeler.SelectedValue = U.First().UlkeId.ToString();
            DropDownListIller.SelectedValue = I.IlId.Value.ToString();
            LabelBaslik.Text = "İlçe Düzenle";
        }

        protected void DropDownListUlkelerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            IlleriVer(DropDownListIllerGridView, Convert.ToInt32(DropDownListUlkelerGridView.SelectedValue));
        }

        protected void DropDownListUlkeler_SelectedIndexChanged(object sender, EventArgs e)
        {
            IlleriVer(DropDownListIller, Convert.ToInt32(DropDownListUlkeler.SelectedValue));
        }

        protected void DropDownListIllerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSource1.CommandText =
                "select p.[Id], p.[IlId], p.[IlceAdi] from Ilceler as p where p.IlId==" +
                DropDownListIllerGridView.SelectedValue + " order by IlceAdi";
            GridViewVeriler.DataBind();
        }
    }
}