using System;
using System.Collections.Generic;

namespace project.entity{

    public partial class BilgitasSellOutThreshold
    {
        public byte[] Timestamp { get; set; } = null!;

        public string CustomerNo { get; set; } = null!;

        public string ItemCategoryCode { get; set; } = null!;

        public string ProductGroupCode { get; set; } = null!;

        public string ItemNo { get; set; } = null!;

        public decimal Quantity { get; set; }
    }
}
