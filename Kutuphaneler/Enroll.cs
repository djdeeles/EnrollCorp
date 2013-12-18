using System.Linq;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Kutuphaneler
{
    public class Enroll
    {
        public static string IlkHarfBuyuk(string Kelime)
        {
            Kelime = Kelime.ToLower();
            var stra = Kelime.ToCharArray();
            for (int i = 0; i < stra.Length; i++)
            {
                if (i == 0)
                {
                    Kelime = string.Empty;
                    Kelime += stra[i].ToString().ToUpper();
                }
                else
                {
                    Kelime += stra[i].ToString();
                }
            }
            return Kelime;
        }

        public static bool YetkiAlaniKontrol(int KullaniciId, int YetkiAlaniId)
        {
            EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();
            bool Durum = false;
            var KRList = Veriler.KullaniciRolleri.Where(p => p.KullaniciId == KullaniciId).ToList();
            foreach (KullaniciRolleri K in KRList)
            {
                var RYAList =
                    Veriler.RolYetkiAlanlari.Where(p => p.RolId == K.RolId && p.YetkiAlaniId == YetkiAlaniId).ToList();
                if (RYAList.Count != 0)
                {
                    Durum = true;
                }
            }
            return Durum;
        }
    }
}