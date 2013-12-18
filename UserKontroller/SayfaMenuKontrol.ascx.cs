using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class SayfaMenuKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                    {
                        MenuleriVer(Convert.ToInt32(Request.QueryString["code"]), MenuHizmetMenu.Items);
                    }
                }
            }
        }

        public void MenuleriVer(int MenuId, MenuItemCollection items)
        {
            items.Clear();
            Menuler M = Veriler.Menuler.Where(p => p.Id == MenuId).First();
            if (M.UstMenuId != 0)
            {
                MenuleriVerBir(M.UstMenuId.Value, items);
            }
            else
            {
                MenuleriVerIki(M.MenuLokasyonId.Value, items);
            }
        }

        private void MenuleriVerBir(int UstMenuId, MenuItemCollection items)
        {
            var oList = new List<Menuler>();
            LabelBaslik.Text = Veriler.Menuler.Where(p => p.Id == UstMenuId).First().MenuAdi;
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var Menuler =
                Veriler.Menuler.Where(
                    p => p.UstMenuId == UstMenuId && p.MenudeGoster == true && p.Durum == true && p.DilId == DilId).
                    OrderBy(p => p.SiraNo).ToList();
            foreach (Menuler Menu in Menuler)
            {
                Menuler oMenu = new Menuler();
                oMenu.UstMenuId = Menu.UstMenuId;
                oMenu.MenuTipId = Menu.MenuTipId;
                switch (Menu.MenuTipId)
                {
                    case 1:
                        oMenu.Url = Menu.MenuAdi;
                        break;
                    case 2:
                        oMenu.Url = Menu.Url;
                        break;
                    case 3:
                        oMenu.Url = Menu.Id.ToString();
                        break;
                }

                if (Menu.MenuTipId != 1)
                {
                    oMenu.Url = Menu.Url;
                }
                else
                {
                    oMenu.Url = oMenu.MenuAdi;
                }
                oMenu.MenuAdi = Menu.MenuAdi;
                oMenu.Id = Menu.Id;
                oMenu.MenuGorsel = Menu.MenuGorsel;
                oMenu.MenuGorselHover = Menu.MenuGorselHover;
                oList.Add(oMenu);
            }
            foreach (Menuler menu in oList)
            {
                MenuItem oItem = new MenuItem();
                if (!string.IsNullOrEmpty(menu.MenuGorsel))
                {
                    string MenuGorselHover = menu.MenuGorselHover.Replace("~/", "/");
                    string MenuGorsel = menu.MenuGorsel.Replace("~/", "/");
                    string src = MenuGorsel;
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htmWriter = new HtmlTextWriter(sw);
                    HtmlImage image = new HtmlImage();
                    image.Src = src;
                    image.Style.Add("border-style", "none");
                    if (MenuGorselHover != "" && MenuGorselHover != null)
                    {
                        image.Attributes["onMouseOver"] = "this.src='" + MenuGorselHover + "';";
                        image.Attributes["onMouseDown"] = "this.src='" + MenuGorselHover + "';";
                    }
                    image.Attributes["onMouseOut"] = "this.src='" + MenuGorsel + "';";
                    image.RenderControl(htmWriter);
                    oItem.Text = Server.HtmlDecode(sw.ToString());
                }
                else
                {
                    oItem.Text = menu.MenuAdi;
                }
                switch (menu.MenuTipId)
                {
                    case 1:
                        oItem.Selectable = false;
                        break;
                    case 2:
                        if (menu.Url.Contains("http"))
                        {
                            oItem.NavigateUrl = menu.Url;
                            oItem.Target = "_blank";
                        }
                        else
                        {
                            oItem.NavigateUrl = "/" + menu.Url;
                        }
                        break;
                    case 3:
                        string yeniUrl = MenuUrl.MenuUrlDuzenle(menu.MenuAdi);
                        oItem.NavigateUrl = "/sayfa/" + menu.Id + "/" + yeniUrl;
                        break;
                }
                items.Add(oItem);
            }
        }

        private void MenuleriVerIki(int LokasyonId, MenuItemCollection items)
        {
            var oList = new List<Menuler>();
            switch (LokasyonId)
            {
                case 1:
                    LabelBaslik.Text = "Pendik Belediyesi";
                    break;
                case 2:
                    LabelBaslik.Text = "Pendik Belediyesi";
                    break;
                case 3:
                    LabelBaslik.Text = "Başkan KENAN ŞAHİN";
                    break;
                case 4:
                    LabelBaslik.Text = "Pendik Belediyesi";
                    break;
                case 5:
                    LabelBaslik.Text = string.Empty;
                    break;
            }
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var Menuler =
                Veriler.Menuler.Where(
                    p => p.MenuLokasyonId == LokasyonId && p.MenudeGoster == true && p.Durum == true && p.DilId == DilId)
                    .OrderBy(p => p.SiraNo).ToList();
            foreach (Menuler Menu in Menuler)
            {
                Menuler oMenu = new Menuler();
                oMenu.UstMenuId = Menu.UstMenuId;
                oMenu.MenuTipId = Menu.MenuTipId;
                switch (Menu.MenuTipId)
                {
                    case 1:
                        oMenu.Url = Menu.MenuAdi;
                        break;
                    case 2:
                        oMenu.Url = Menu.Url;
                        break;
                    case 3:
                        oMenu.Url = Menu.Id.ToString();
                        break;
                }

                if (Menu.MenuTipId != 1)
                {
                    oMenu.Url = Menu.Url;
                }
                else
                {
                    oMenu.Url = oMenu.MenuAdi;
                }
                oMenu.MenuAdi = Menu.MenuAdi;
                oMenu.Id = Menu.Id;
                oMenu.MenuGorsel = Menu.MenuGorsel;
                oMenu.MenuGorselHover = Menu.MenuGorselHover;
                oList.Add(oMenu);
            }

            foreach (Menuler menu in oList)
            {
                MenuItem oItem = new MenuItem();
                if (!string.IsNullOrEmpty(menu.MenuGorsel))
                {
                    string MenuGorselHover = menu.MenuGorselHover.Replace("~/", "/");
                    string MenuGorsel = menu.MenuGorsel.Replace("~/", "/");
                    string src = MenuGorsel;
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htmWriter = new HtmlTextWriter(sw);
                    HtmlImage image = new HtmlImage();
                    image.Src = src;
                    image.Style.Add("border-style", "none");
                    if (MenuGorselHover != "" && MenuGorselHover != null)
                    {
                        image.Attributes["onMouseOver"] = "this.src='" + MenuGorselHover + "';";
                        image.Attributes["onMouseDown"] = "this.src='" + MenuGorselHover + "';";
                    }
                    image.Attributes["onMouseOut"] = "this.src='" + MenuGorsel + "';";
                    image.RenderControl(htmWriter);
                    oItem.Text = Server.HtmlDecode(sw.ToString());
                }
                else
                {
                    oItem.Text = menu.MenuAdi;
                }
                switch (menu.MenuTipId)
                {
                    case 1:
                        oItem.Selectable = false;
                        break;
                    case 2:
                        if (menu.Url.Contains("http"))
                        {
                            oItem.NavigateUrl = menu.Url;
                            oItem.Target = "_blank";
                        }
                        else
                        {
                            oItem.NavigateUrl = "/" + menu.Url;
                        }
                        break;
                    case 3:
                        string yeniUrl = MenuUrl.MenuUrlDuzenle(menu.MenuAdi);
                        oItem.NavigateUrl = "/sayfa/" + menu.Id + "/" + yeniUrl;
                        break;
                }
                items.Add(oItem);
            }
        }
    }
}