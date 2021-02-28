using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constains
{
    public static class Messages
    {
        public static string CarEdded = "Araba Eklendi";
        public static string CarNameInvalid = "Lütfen Description alanı en az 1 karekter olmalı ve DailyPrice sıfırdan büyük olmalı..";
        internal static string Maintenacetime = "Sistem bakımda";


        internal static string RegistrationNameInvalid = "Geçersiz Kayıt İsmi (en az 2 karekter olmalı)";
        internal static string RegistrationAdded = "Kayıt Başarıyla Eklendi";
        internal static string RegistrationListed = "Kayıtlar Başarıyla Listelendi";
        internal static string RegistrationDeleted = "Kayıt Başarıyla Silindi";
        internal static string RegistrationUpped="Kayıt Başarıyla Güncellendi";

        public static string Added = "Add process OK";
        public static string NotAdded = "Add process NOT OK";

        public static string Deleted = "Delete process OK";
        public static string NotDeleted = "Delete process NOT OK";

        public static string Updated = "Update process OK";
        public static string NotUpdated = "Update process NOT OK";

        public static string Listed = "List process OK";
        public static string NotListed = "List process NOT OK";

        public static string MaintenanceTime = "System is under maintenance";
        public static string FailedRental = "The car has not yet been delivered";
        public static string CarImageLimitExceeded = "More than 5 images cannot be added";
        public static string NoCarImages = "The car does NOT have any images";
    }
}
