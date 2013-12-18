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
    public partial class BaskanMenuKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuHizmetMenu.DataSource = null;
                MenuHizmetMenu.DataBind();
                BaskanMenuVer(0, MenuHizmetMenu.Items);
            }
        }

        public void BaskanMenuVer(Int32 UstMenuId, MenuItemCollection items)
        {
            var oList = new List<Menuler>();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var Menuler =
                Veriler.Menuler.Where(
                    p => p.MenuLokasyonId == 3 && p.MenudeGoster == true && p.Durum == true && p.DilId == DilId).OrderBy
                    (p => p.SiraNo).ToList();
            foreach (Menuler Menu in Menuler)
            {
                if (Menu.UstMenuId == UstMenuId)
                {
                    Menuler oMenu = new Menuler();
                    oMenu.UstMenuId = Menu.UstMenuId;
                    oMenu.MenuTipId = Menu.MenuTipId;
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
                        if (menu.Url.StartsWith("http"))
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
                        oItem.NavigateUrl = "/sayfa/" + menu.Id + "/" + MenuUrl.MenuUrlDuzenle(menu.MenuAdi);
                        break;
                }
                items.Add(oItem);
                BaskanMenuVer(Convert.ToInt32(menu.Id), oItem.ChildItems);
            }
        }
    }
}