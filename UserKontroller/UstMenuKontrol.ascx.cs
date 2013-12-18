using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class UstMenuKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        public static List<MenuListeleri> Liste { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UstMenuleriVer();
            }
        }

        private void UstMenuleriVer()
        {
            DataListUstMenuler.DataSource = string.Empty;
            DataListUstMenuler.DataBind();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var Menuler =
                Veriler.Menuler.Where(
                    p =>
                    p.MenuLokasyonId == 1 && p.MenudeGoster == true && p.Durum == true && p.UstMenuId == 0 &&
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
                            if (Menu.Id == 9)
                            {
                                MenuListe.NavigateUrl = Menu.Url;
                            }
                            else
                            {
                                MenuListe.NavigateUrl = "/" + Menu.Url;
                            }
                        }
                        break;
                    case 3:
                        MenuListe.Id = Menu.Id;
                        MenuListe.Text = Menu.MenuAdi;
                        MenuListe.NavigateUrl = Menu.Id.ToString();
                        break;
                }
                Liste.Add(MenuListe);
            }
            DataListUstMenuler.DataSource = Liste;
            DataListUstMenuler.DataBind();
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