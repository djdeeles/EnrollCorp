using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class AnketKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AnketVer();
            }
        }

        private void AnketVer()
        {
            try
            {
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                AnketSorulari AS =
                    Veriler.AnketSorulari.Where(p => p.DilId == DilId && p.Durum == true).OrderBy(p => Guid.NewGuid()).
                        First();
                if (AS != null)
                {
                    HiddenField1.Value = AS.Soru;
                    lblSoru.Text = AS.Soru;
                    HiddenField2.Value = AS.Id.ToString();
                    CevaplariVer(AS.Id);
                }
                else
                {
                    HiddenField1.Value = string.Empty;
                    HiddenField2.Value = string.Empty;
                }
            }
            catch (SqlException Hata)
            {
                HiddenField1.Value = string.Empty;
                HiddenField2.Value = string.Empty;
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }

        private void CevaplariVer(int AnketId)
        {
            try
            {
                hdnToplam.Value =
                    Veriler.AnketCevaplari.Where(p => p.AnketSorulariId == AnketId && p.Durum == true).Sum(
                        p => p.OySayisi).Value.ToString();
                EntityDataSource1.Where = "it.AnketSorulariId=" + AnketId + " and it.Durum==True";
                GridView1.DataBind();
                HttpCookie myCookie = Request.Cookies["eNrollQ_Anket_" + AnketId];
                if (myCookie != null)
                {
                    Panel4.Visible = true;
                    RadioButtonList1.Visible = false;
                    LinkButtonOyla.Visible = false;
                }
                else
                {
                    Panel4.Visible = false;
                    RadioButtonList1.Visible = true;
                    LinkButtonOyla.Visible = true;
                }
            }
            catch (SqlException Hata)
            {
                GridView1.DataSource = string.Empty;
                GridView1.DataBind();
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }

        private void AnketVer(int AnketId)
        {
            try
            {
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                AnketSorulari AS = Veriler.AnketSorulari.Where(p => p.Id == AnketId && p.Durum == true).First();
                if (AS != null)
                {
                    HiddenField1.Value = AS.Soru;
                    lblSoru.Text = AS.Soru;
                    HiddenField2.Value = AS.Id.ToString();
                    CevaplariVer(AS.Id);
                }
                else
                {
                    HiddenField1.Value = string.Empty;
                    HiddenField2.Value = string.Empty;
                }
            }
            catch
            {
                HiddenField1.Value = string.Empty;
                HiddenField2.Value = string.Empty;
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                Label lblOran = (Label) row.FindControl("lblOran");
                Double oran = Convert.ToDouble(lblOran.Text);
                Int32 toplamSonuc = Convert.ToInt32(hdnToplam.Value);
                lblOran.Text = "%" + Math.Round((oran*100)/toplamSonuc).ToString();
            }
        }

        protected void LinkButtonOyla_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(RadioButtonList1.SelectedValue) > 0)
            {
                int AnketCevapId = Convert.ToInt32(RadioButtonList1.SelectedValue);
                AnketCevaplari AC = Veriler.AnketCevaplari.Where(p => p.Id == AnketCevapId).First();
                AC.OySayisi = AC.OySayisi + 1;
                Veriler.SaveChanges();
                HttpCookie myCookie = new HttpCookie("eNrollQ_anket_" + HiddenField2.Value);
                myCookie["anketId"] = HiddenField2.Value;
                myCookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(myCookie);
                AnketVer(Convert.ToInt32(HiddenField2.Value));
            }
        }
    }
}