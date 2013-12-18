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
    public partial class HizmetMenuKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuHizmetMenu.Items.Clear();
                HizmetMenuleriVer(0, MenuHizmetMenu.Items);
            }
        }

        private void HizmetMenuleriVer(int UstMenuId, MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var MenulerList =
                Veriler.Menuler.Where(
                    p =>
                    p.MenuLokasyonId == 5 && p.MenudeGoster == true && p.Durum == true && p.UstMenuId == 0 &&
                    p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            var oList = new List<Menuler>();
            foreach (Menuler M in MenulerList)
            {
                Menuler oMenu = new Menuler();
                oMenu.UstMenuId = M.UstMenuId;
                oMenu.MenuLokasyonId = M.MenuLokasyonId;
                oMenu.MenuTipId = M.MenuTipId.Value;
                if (oMenu.MenuTipId.Value != 1)
                {
                    oMenu.Url = M.Url;
                }
                else
                {
                    oMenu.Url = oMenu.MenuAdi;
                }
                oMenu.MenuAdi = M.MenuAdi;
                oMenu.Id = M.Id;
                oMenu.MenuGorsel = M.MenuGorsel;
                oMenu.MenuGorselHover = M.MenuGorselHover;
                oList.Add(oMenu);
            }
            foreach (Menuler menu in oList)
            {
                MenuItem oItem = new MenuItem();
                if (!string.IsNullOrEmpty(menu.MenuGorsel))
                {
                    string onImage = menu.MenuGorsel.Replace("~/", "/");
                    string offImage = menu.MenuGorselHover.Replace("~/", "/");
                    string src = offImage;
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htmWriter = new HtmlTextWriter(sw);
                    HtmlImage image = new HtmlImage();
                    image.Src = src;
                    image.Style.Add("border-style", "none");
                    if (offImage != "" && offImage != null)
                    {
                        image.Attributes["onMouseOver"] = "this.src='" + offImage + "';";
                    }
                    if (offImage != "" && offImage != null)
                    {
                        image.Attributes["onMouseDown"] = "this.src='" + offImage + "';";
                    }
                    image.Attributes["onMouseOut"] = "this.src='" + onImage + "';";
                    image.Src = onImage;
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
                Items.Add(oItem);
            }
        }
    }
}