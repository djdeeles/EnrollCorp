using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class IllerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "İller Yönetimi";
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
                        "select p.[Id], p.[UlkeId], p.[IlAdi] from Iller as p where p.UlkeId==0 order by IlAdi";
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
            TextBoxIlAdi.Text = string.Empty;
            UlkeleriVer(DropDownListUlkeler);
            UlkeleriVer(DropDownListUlkelerGridView);
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

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "İl Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Iller I;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    I = Veriler.Iller.Where(p => p.Id == Id).First();
                    I.UlkeId = Convert.ToInt32(DropDownListUlkeler.SelectedValue);
                    I.IlAdi = TextBoxIlAdi.Text;
                    I.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    I.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[UlkeId], p.[IlAdi] from Iller as p where p.UlkeId==" +
                        DropDownListUlkeler.SelectedValue + " order by IlAdi";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    I = new Iller();
                    I.UlkeId = Convert.ToInt32(DropDownListUlkeler.SelectedValue);
                    I.IlAdi = TextBoxIlAdi.Text;
                    I.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    I.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToIller(I);
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[UlkeId], p.[IlAdi] from Iller as p where p.UlkeId==" +
                        DropDownListUlkeler.SelectedValue + " order by IlAdi";
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
                Iller I = Veriler.Iller.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(I);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Iller I = Veriler.Iller.Where(p => p.Id == Id).First();
                Veriler.Iller.DeleteObject(I);
                Veriler.SaveChanges();
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[UlkeId], p.[IlAdi] from Iller as p where p.UlkeId==" + I.UlkeId +
                    " order by IlAdi";
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[UlkeId], p.[IlAdi] from Iller as p where p.UlkeId==" +
                    DropDownListUlkelerGridView.SelectedValue + " order by IlAdi";
            }
        }

        private void Guncelle(Iller I)
        {
            TextBoxIlAdi.Text = I.IlAdi;
            HiddenFieldId.Value = I.Id.ToString();
            DropDownListUlkeler.SelectedValue = I.UlkeId.Value.ToString();
            LabelBaslik.Text = "İl Düzenle";
        }

        protected void DropDownListUlkelerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSource1.CommandText =
                "select p.[Id], p.[UlkeId], p.[IlAdi] from Iller as p where p.UlkeId==" +
                DropDownListUlkelerGridView.SelectedValue + " order by IlAdi";
            GridViewVeriler.DataBind();
        }
    }
}