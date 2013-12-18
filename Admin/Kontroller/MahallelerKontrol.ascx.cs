using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class MahallelerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Mahalleler Yönetimi";
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
                        "select p.[Id], p.[IlceId], p.[MahalleAdi] from Mahalleler as p where p.IlceId==0 order by MahalleAdi";
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
            TextBoxMahalleAdi.Text = string.Empty;
            UlkeleriVer(DropDownListUlkeler);
            UlkeleriVer(DropDownListUlkelerGridView);
            IlleriVer(DropDownListIller, Convert.ToInt32(DropDownListUlkeler.SelectedValue));
            IlleriVer(DropDownListIllerGridView, Convert.ToInt32(DropDownListUlkeler.SelectedValue));
            IlceleriVer(DropDownListIlceler, Convert.ToInt32(DropDownListIller.SelectedValue));
            IlceleriVer(DropDownListIlcelerGridView, Convert.ToInt32(DropDownListIller.SelectedValue));
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

        private void IlceleriVer(DropDownList DropDownListIlceler, int IlId)
        {
            var IList = Veriler.Ilceler.Where(p => p.IlId == IlId).ToList();
            DropDownListIlceler.DataTextField = "IlceAdi";
            DropDownListIlceler.DataValueField = "Id";
            DropDownListIlceler.DataSource = IList;
            DropDownListIlceler.DataBind();
            DropDownListIlceler.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Mahalle Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Mahalleler M;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    M = Veriler.Mahalleler.Where(p => p.Id == Id).First();
                    M.IlceId = Convert.ToInt32(DropDownListIlceler.SelectedValue);
                    M.MahalleAdi = TextBoxMahalleAdi.Text;
                    M.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    M.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[IlceId], p.[MahalleAdi] from Mahalleler as p where p.IlceId==" +
                        DropDownListIlceler.SelectedValue + " order by MahalleAdi";
                    GridViewVeriler.DataBind();
                    Temizle();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    M = new Mahalleler();
                    M.IlceId = Convert.ToInt32(DropDownListIlceler.SelectedValue);
                    M.MahalleAdi = TextBoxMahalleAdi.Text;
                    M.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    M.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToMahalleler(M);
                    Veriler.SaveChanges();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[IlceId], p.[MahalleAdi] from Mahalleler as p where p.IlceId==" +
                        DropDownListIlceler.SelectedValue + " order by MahalleAdi";
                    GridViewVeriler.DataBind();
                    Temizle();
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
                Mahalleler M = Veriler.Mahalleler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(M);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Mahalleler M = Veriler.Mahalleler.Where(p => p.Id == Id).First();
                Veriler.Mahalleler.DeleteObject(M);
                Veriler.SaveChanges();
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[IlceId], p.[MahalleAdi] from Mahalleler as p where p.IlceId==" + M.IlceId +
                    " order by MahalleAdi";
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[IlceId], p.[MahalleAdi] from Mahalleler as p where p.IlceId==" +
                    DropDownListIlcelerGridView.SelectedValue + " order by MahalleAdi";
            }
        }

        private void Guncelle(Mahalleler M)
        {
            TextBoxMahalleAdi.Text = M.MahalleAdi;
            HiddenFieldId.Value = M.Id.ToString();
            var U = from p in Veriler.Ilceler
                    join p1 in Veriler.Iller
                        on p.IlId equals p1.Id
                    join p2 in Veriler.Ulkeler
                        on p1.UlkeId equals p2.Id
                    where p.Id == M.IlceId
                    select new
                               {
                                   UlkeId = p2.Id,
                                   IlId = p1.Id,
                               };
            DropDownListUlkeler.SelectedValue = U.First().UlkeId.ToString();
            DropDownListIller.SelectedValue = U.First().IlId.ToString();
            DropDownListIlceler.SelectedValue = M.IlceId.Value.ToString();
            LabelBaslik.Text = "Mahalle Düzenle";
        }

        protected void DropDownListUlkeler_SelectedIndexChanged(object sender, EventArgs e)
        {
            IlleriVer(DropDownListIller, Convert.ToInt32(DropDownListUlkeler.SelectedValue));
        }

        protected void DropDownListIller_SelectedIndexChanged(object sender, EventArgs e)
        {
            IlceleriVer(DropDownListIlceler, Convert.ToInt32(DropDownListIller.SelectedValue));
        }

        protected void DropDownListUlkelerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            IlleriVer(DropDownListIllerGridView, Convert.ToInt32(DropDownListUlkelerGridView.SelectedValue));
        }

        protected void DropDownListIllerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            IlceleriVer(DropDownListIlcelerGridView, Convert.ToInt32(DropDownListIllerGridView.SelectedValue));
        }

        protected void DropDownListIlcelerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSource1.CommandText =
                "select p.[Id], p.[IlceId], p.[MahalleAdi] from Mahalleler as p where p.IlceId==" +
                DropDownListIlcelerGridView.SelectedValue + " order by MahalleAdi";
            GridViewVeriler.DataBind();
        }
    }
}