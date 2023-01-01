namespace Saas.Business.Constants
{
    public static class Messages
    {
        public static string CompanyAdded = "Firma Başarı ile eklendi";
        public static string CompanyUpdated = "Firma Başarı ile güncellendi";
        public static string CompanyDeleted = "Firma Başarı ile silindi";

        //public static string DatabaseAdded = "Database Bilgisi Başarı ile eklendi";
        //public static string DatabaseUpdated = "Database Bilgisi Başarı ile güncellendi";
        //public static string DatabaseDeleted = "Database Bilgisi Başarı ile silindi";

        //public static string LicenceAdded = "Lisans Bilgisi Başarı ile eklendi";
        //public static string LicenceUpdated = "Lisans Bilgisi Başarı ile güncellendi";
        //public static string LicenceDeleted = "Lisans Bilgisi Başarı ile silindi";

        public static string LocalsAdded = "Şube Bilgisi Başarı ile eklendi";
        public static string LocalsUpdated = "Şube Bilgisi Başarı ile güncellendi";
        public static string LocalsDeleted = "Şube Bilgisi Başarı ile silindi";

        //public static string rolesAdded = "Rol Bilgisi Başarı ile eklendi";
        //public static string rolesUpdated = "Rol Bilgisi Başarı ile güncellendi";
        //public static string rolesDeleted = "Rol Bilgisi Başarı ile silindi";


        public static readonly string UsersAdded = "Kullanici Başarı ile eklendi";
        public static readonly string UsersUpdated = "Kullanici Bilgisi Başarı ile güncellendi";
        public static readonly string UsersDeleted = "Kullanici Bilgisi Başarı ile silindi";

        public static readonly string UserNotFound = "Kullanıcı Bulunamadı.";
        public static readonly string PasswordError = "Kullanıcı veya Sifre Hatali";
        public static readonly string SuccessfullLogin = "Sisteme Giris Basarili";
        public static readonly string UserAlreadyExist = "Kullanıcı daha önce kayıt olmuştur.";
        public static readonly string AccessTokenCreated = "Token Oluşturuldu.";


        #region ValidationMessages

        public static string TaxNumberValidationError = "Vergi Numarasi Boş Bırakılamaz.";
        public static string TaxNumberLengtValidationError = "Vergi Numarasi Uzunluğu hatası";

        #endregion


        public static readonly string AuthorizationDenied = "Yetkiniz Bu İşlemi Yapmaya Uygun Değil.";

        public static readonly string CompanyTaxNumberExistError = "Firma Daha Önce Kayıt Edilmiştir.";

        public static readonly string EmailCanNotBlank = "Mail Adresi Bos Birakilamaz";

        public static readonly string rolesAdded = "Rol Eklendi.";
        public static readonly string rolesDeleted = "Rol Silindi.";
        public static readonly string rolesUpdated = "Rol Güncellendi.";


        public static readonly string WareHouseUpdated = "Ambar Güncellendi";
        public static readonly string WareHouseDeleted = "Ambar Silindi";
        public static readonly string WareHouseAdded = "Ambar Eklendi";

        public static readonly string WareHouseFloorAdded = "Kat Eklendi";
        public static readonly string WareHouseFloorDeleted = "Kat Silindi";
        public static readonly string WareHouseFloorUpdated = "Kat Güncellendi";

        public static readonly string WareHouseShelfAdded = "Raf Eklendi";
        public static readonly string WareHouseShelfDeleted = "Raf Silindi";
        public static readonly string WareHouseShelfUpdated = "Raf Güncellendi";


        public static readonly string WareHouseBenchAdded = "Sıra Eklendi";
        public static readonly string WareHouseBenchDeleted = "Sıra Silindi";
        public static readonly string WareHouseBenchUpdated = "Sıra Güncellendi";

        public static readonly string WareHouseCorridorAdded = "Koridor Eklendi";
        public static readonly string WareHouseCorridorDeleted = "Koridor Silindi";
        public static readonly string WareHouseCorridorUpdated = "Koridor Güncellendi";
        public static readonly string MailValidationError = "Mail Adresi Dogru Formatta Değil.";


        public static string UserValidationEmailError = "Email Alanı boş bırakılamaz";
        public static string UserValidatonPasswordError = "Şifre alanı boş bırakılamaz";
        


    }
}