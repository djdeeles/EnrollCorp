using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.AdminGiris.Kontroller
{
    public partial class OturumAc : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CookieKontrol();
                MultiView1.ActiveViewIndex = 0;
                TextBoxEPosta.Focus();
            }
            //string KullaniciIp = HttpContext.Current.Request.UserHostAddress;
            //string Ip = "94.54.109.152";
            //if ((KullaniciIp == Ip) || (KullaniciIp.StartsWith("10.")))
            //{
            //    if (!IsPostBack)
            //    {
            //        CookieKontrol();
            //        MultiView1.ActiveViewIndex = 0;
            //        TextBoxEPosta.Focus();
            //    }
            //}
            //else
            //{
            //    Response.Redirect("http://www.enroll.com.tr/");
            //}
        }

        private void CookieKontrol()
        {
            HttpCookie C = Request.Cookies["KurumsalCookie"];
            if (C != null)
            {
                string EPosta = C["EPosta"];
                string Parola = C["Parola"];
                var Kullanici = (from p in Veriler.Kullanicilar
                                 from p1 in Veriler.Roller
                                 where p.EPosta == EPosta
                                       && p.Parola == Parola
                                       && p.Durum == true
                                       && p.SilindiMi == false
                                       && p1.Durum == true
                                 select new
                                            {
                                                p.Id,
                                                p.EPosta,
                                                p.Parola,
                                            }).FirstOrDefault();
                if (Kullanici != null)
                {
                    FormsAuthenticationTicket Ticket =
                        new FormsAuthenticationTicket(
                            1,
                            Kullanici.Id.ToString(),
                            DateTime.Now, DateTime.Now.AddDays(1),
                            false,
                            FormsAuthentication.FormsCookiePath);
                    String encTicket = FormsAuthentication.Encrypt(Ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    if (Ticket.IsPersistent)
                    {
                        cookie.Expires = Ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    String a = FormsAuthentication.GetRedirectUrl(Kullanici.EPosta, false);
                    Response.Redirect(a, false);
                }
            }
        }

        protected void ImageButtonGiris_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string EPosta = TextBoxEPosta.Text;
                string Parola = TextBoxParola.Text;
                var Kullanici = (from p in Veriler.Kullanicilar
                                 from p1 in Veriler.Roller
                                 where p.EPosta == EPosta
                                       && p.Parola == Parola
                                       && p.Durum == true
                                       && p.SilindiMi == false
                                       && p1.Durum == true
                                 select new
                                            {
                                                p.Id,
                                                p.EPosta,
                                                p.Parola,
                                            }).FirstOrDefault();
                if (Kullanici != null)
                {
                    FormsAuthenticationTicket Ticket =
                        new FormsAuthenticationTicket(
                            1,
                            Kullanici.Id.ToString(),
                            DateTime.Now, DateTime.Now.AddDays(1),
                            false,
                            FormsAuthentication.FormsCookiePath);
                    String encTicket = FormsAuthentication.Encrypt(Ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    if (Ticket.IsPersistent)
                    {
                        cookie.Expires = Ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    if (CheckBoxBeniHatirla.Checked)
                    {
                        // string EPosta = FormsAuthentication.Encrypt(Kullanici.EPosta);
                        HttpCookie C = new HttpCookie("KurumsalCookie");
                        C["EPosta"] = Kullanici.EPosta;
                        C["Parola"] = Kullanici.Parola;
                        C.Expires = DateTime.Now.AddMonths(1);
                        Response.Cookies.Add(C);
                    }
                    String a = FormsAuthentication.GetRedirectUrl(Kullanici.EPosta, false);
                    Response.Redirect(a, false);
                }
                else
                {
                    LabelMesaj.Text = "Bilgileriniz geçersizdir";
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                LabelMesaj.Text = "Hata oluştu!";
            }
        }

        protected void ImageButtonParolamiUnuttum_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void ImageButtonGonder2_Click(object sender, ImageClickEventArgs e)
        {
            string EPosta = TextBoxEPosta1.Text;
            Kullanicilar K =
                Veriler.Kullanicilar.Where(p => p.EPosta == EPosta && p.Durum == true && p.SilindiMi == false).
                    FirstOrDefault();
            if (K != null)
            {
                try
                {
                    MailDefinition MD = new MailDefinition();
                    ListDictionary LD = new ListDictionary();
                    LD.Add("%%EPosta%%", K.EPosta);
                    LD.Add("%%Parola%%", K.Parola);
                    MD.IsBodyHtml = true;
                    MD.BodyFileName = "Templates/parolamiunuttum.htm";
                    MD.Subject = "Parolamı Unuttum";
                    MD.From = "localhost";
                    MailMessage MM = MD.CreateMailMessage(K.EPosta, LD, this);
                    MM.BodyEncoding = Encoding.Default;
                    MM.SubjectEncoding = Encoding.Default;
                    MM.Priority = MailPriority.High;
                    SmtpClient SC = new SmtpClient("localhost", 25);
                    //smtp.Credentials = new System.Net.NetworkCredential("kultursanat@pendik.bel.tr", "Pendik4918");
                    SC.Send(MM);
                    TextBoxEPosta1.Text = string.Empty;
                    LabelParolamiUnuttum.Text = "Parolanı e-posta adresinize gönderilmiştir.";
                }
                catch (Exception Hata)
                {
                    LabelParolamiUnuttum.Text = "Hata oluştu!";
                    EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                }
            }
            else
            {
                LabelParolamiUnuttum.Text = "Kullanıcı bulunamadı!";
            }
        }

        protected void ImageButtonGeri_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }
}