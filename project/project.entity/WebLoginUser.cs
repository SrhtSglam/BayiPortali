namespace project.entity{
    public class WebLoginUser
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public int WebVisibility { get; set; }
        public static string Company { get; set; }

        public static string AuthId {get; set;}
        public static string? CustomerNo {get; set;}
        public static int Visibility {get; set;}

        public static string? EMail {get; set;}
        
        public static class PageAuthorize
        {
            public static bool Siparis { get; set; }
            public static bool Siparis_Gec { get; set; }
            public static bool Siparis_Onayla { get; set; }
            public static bool Siparis_Kontrol { get; set; }
            public static bool Islevler { get; set; }
            public static bool Islevler_SellOutGiris { get; set; }
            public static bool Islevler_NumaratorGiris { get; set; }
            public static bool Islevler_SeriKontrol { get; set; }
            public static bool Islevler_Garanti { get; set; }
            public static bool Islevler_Garanti_IstekGiris { get; set; }
            public static bool Islevler_Garanti_SurecTakip { get; set; }
            public static bool Raporlar { get; set; }
            public static bool Raporlar_Mizan { get; set; }
            public static bool Raporlar_Ekstre { get; set; }
            public static bool DuyuruDuzenle { get; set; }
            public static bool Download { get; set; }
        }
    }
}

