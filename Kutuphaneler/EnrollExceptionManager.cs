using System;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Kutuphaneler
{
    public class EnrollExceptionManager
    {
        public static void ManageException(Exception Ex, string Sayfa)
        {
            try
            {
                EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();
                Hatalar Hata = new Hatalar();
                Hata.HataMesaji = Ex.Message;
                Hata.Kaynak = Ex.StackTrace;
                Hata.Sayfa = Sayfa;
                Hata.Tarih = DateTime.Now;
                Veriler.AddToHatalar(Hata);
                Veriler.SaveChanges();
            }
            catch (Exception)
            {
                //
            }
        }
    }
}