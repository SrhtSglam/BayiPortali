using System;
using System.Collections.Generic;

namespace project.entity{

    public partial class BilgitasSalesPrice
    {
        public byte[] Timestamp { get; set; } = null!;

        public string ItemNo { get; set; } = null!;

        public int SalesType { get; set; }

        public string SalesCode { get; set; } = null!;

        public DateTime StartingDate { get; set; }

        public string CurrencyCode { get; set; } = null!;

        public string VariantCode { get; set; } = null!;

        public string UnitOfMeasureCode { get; set; } = null!;

        public decimal MinimumQuantity { get; set; }

        public decimal UnitPrice { get; set; }

        public byte PriceIncludesVat { get; set; }

        public byte AllowInvoiceDisc { get; set; }

        public string VatBusPostingGrPrice { get; set; } = null!;

        public DateTime EndingDate { get; set; }

        public byte AllowLineDisc { get; set; }
    }
}
