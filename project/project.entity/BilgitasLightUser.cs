using System;
using System.Collections.Generic;

namespace project.entity{
    public partial class BilgitasLightUser
    {
        public byte[] Timestamp { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string CustomerNo { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string SalespersonCode { get; set; } = null!;

        public byte ResetPassword { get; set; }

        public string EMail { get; set; } = null!;

        public int WebVisibility { get; set; }

        public string ConfirmEMail1 { get; set; } = null!;

        public string ConfirmEMail2 { get; set; } = null!;

        public string ConfirmEMail3 { get; set; } = null!;

        public string ConfirmEMail4 { get; set; } = null!;

        public string ConfirmEMail5 { get; set; } = null!;

        public string LocationCode { get; set; } = null!;

        public string VisiblePassword { get; set; } = null!;

        public byte SiparişGeç { get; set; }

        public byte SiparişOnayla { get; set; }

        public byte SiparişKontrol { get; set; }

        public byte SellOutGiriş { get; set; }

        public byte DuyuruDüzenle { get; set; }

        public byte AnaSayfa { get; set; }

        public byte Download { get; set; }

        public byte Garanti { get; set; }

        public byte Sipariş { get; set; }

        public byte GarantiİstekGiriş { get; set; }

        public byte GarantiSüreçTakibi { get; set; }

        public byte NumaratörGiriş { get; set; }

        public byte İşlevler { get; set; }

        public byte SeriKontrol { get; set; }

        public byte Mizan { get; set; }

        public byte Raporlar { get; set; }

        public byte Ekstre { get; set; }
    }
}

