using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constains
{
    public static class Messages
    {

        public static string Added = "Ekleme İşlemi Başarılı";
        public static string NotAdded = "Ekleme İşlemi Başarısız";

        public static string Deleted = "Silme İşlemi Başarılı";
        public static string NotDeleted = "Silme İŞlemi Başarısız";

        public static string Updated = "Güncellene İşlemi Başarılı";
        public static string NotUpdated = "Güncelleme İşlemi Başarısız";

        public static string Listed = "Listeleme İşlemi Başarılı";
        public static string NotListed = "Listeleme İşlemi Başarısız";

        public static string MaintenanceTime = "Sistem Bakımda";
        public static string FailedRental = "Araba Henüz Teslim Edilmedi";
        public static string CarImageLimitExceeded = "5'ten fazla resim eklenemez";
        public static string NoCarImages = "Arabadaya ait Herhangi bir resim bulunamadı";
    }
}
