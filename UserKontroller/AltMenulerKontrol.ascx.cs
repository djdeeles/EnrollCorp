using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class AltMenulerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        public static List<MenuListeleri> Liste { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AltMenuBasliklariVer();
            }
        }

        private void AltMenuBasliklariVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var Menuler =
                Veriler.Menuler.Where(
                    p =>
                    p.MenuLokasyonId == 4 && p.MenudeGoster == true && p.Durum == true && p.UstMenuId == 0 &&
                    p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            Liste = new List<MenuListeleri>();
            foreach (Menuler Menu in Menuler)
            {
                MenuListeleri MenuListe = new MenuListeleri();
                switch (Menu.MenuTipId)
                {
                    case 1:
                        MenuListe.Id = Menu.Id;
                        MenuListe.Text = Menu.MenuAdi;
                        MenuListe.NavigateUrl = string.Empty;
                        break;
                    case 2:
                        MenuListe.Id = Menu.Id;
                        MenuListe.Text = Menu.MenuAdi;
                        if (Menu.Url.StartsWith("http"))
                        {
                            MenuListe.NavigateUrl = Menu.Url;
                            MenuListe.Target = "_blank";
                        }
                        else
                        {
                            MenuListe.NavigateUrl = "/" + Menu.Url;
                        }
                        break;
                    case 3:
                        MenuListe.Id = Menu.Id;
                        MenuListe.Text = Menu.MenuAdi;
                        MenuListe.NavigateUrl = "sayfa/" + Menu.Id.ToString() + "/" +
                                                MenuUrl.MenuUrlDuzenle(Menu.MenuAdi);
                        break;
                }
                Liste.Add(MenuListe);
            }
            DataListAltMenuBasliklar.DataSource = Liste;
            DataListAltMenuBasliklar.DataBind();
        }

        public List<MenuListeleri> AltMenuleriVer(int UstMenuId)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var Menuler =
                Veriler.Menuler.Where(
                    p =>
                    p.MenuLokasyonId == 4 && p.MenudeGoster == true && p.Durum == true && p.UstMenuId == UstMenuId &&
                    p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            Liste = new List<MenuListeleri>();
            foreach (Menuler Menu in Menuler)
            {
                MenuListeleri MenuListe = new MenuListeleri();
                switch (Menu.MenuTipId)
                {
                    case 1:
                        MenuListe.Id = Menu.Id;
                        MenuListe.Text = Menu.MenuAdi;
                        MenuListe.NavigateUrl = string.Empty;
                        break;
                    case 2:
                        MenuListe.Id = Menu.Id;
                        MenuListe.Text = Menu.MenuAdi;
                        if (Menu.Url.StartsWith("http"))
                        {
                            MenuListe.NavigateUrl = Menu.Url;
                            MenuListe.Target = "_blank";
                        }
                        else
                        {
                            MenuListe.NavigateUrl = "/" + Menu.Url;
                        }
                        break;
                    case 3:
                        MenuListe.Id = Menu.Id;
                        MenuListe.Text = Menu.MenuAdi;
                        MenuListe.NavigateUrl = "sayfa/" + Menu.Id.ToString() + "/" +
                                                MenuUrl.MenuUrlDuzenle(Menu.MenuAdi);
                        break;
                }
                Liste.Add(MenuListe);
            }
            return Liste;
        }

        #region Nested type: MenuListeleri

        public class MenuListeleri
        {
            public int Id { get; set; }
            public string NavigateUrl { get; set; }
            public string Target { get; set; }
            public string Text { get; set; }
        }

        #endregion
    }
}