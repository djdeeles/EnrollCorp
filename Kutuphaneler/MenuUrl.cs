namespace EnrollKurumsal.Kutuphaneler
{
    public class MenuUrl
    {
        public static string MenuUrlDuzenle(string MenuAdi)
        {
            MenuAdi = MenuAdi.ToLower();
            MenuAdi = MenuAdi.Replace(" ", "_");
            MenuAdi = MenuAdi.Replace("ş", "s");
            MenuAdi = MenuAdi.Replace("ü", "u");
            MenuAdi = MenuAdi.Replace("ı", "i");
            MenuAdi = MenuAdi.Replace("ğ", "g");
            MenuAdi = MenuAdi.Replace("ö", "o");
            MenuAdi = MenuAdi.Replace("ç", "c");
            MenuAdi = MenuAdi.Replace("&", "");
            MenuAdi = MenuAdi.Replace(":", "");
            MenuAdi = MenuAdi.Replace("'", "");
            MenuAdi = MenuAdi.Replace("’", "_");
            MenuAdi = MenuAdi.Replace("\"", "_");
            MenuAdi = MenuAdi.Replace(",", "_");
            return MenuAdi;
        }
    }
}