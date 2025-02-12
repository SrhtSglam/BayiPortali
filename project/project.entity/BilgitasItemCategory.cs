using System;
using System.Collections.Generic;

namespace project.entity{
    public partial class BilgitasItemCategory
    {
        public byte[] Timestamp { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string DefGenProdPostingGroup { get; set; } = null!;

        public string DefInventoryPostingGroup { get; set; } = null!;

        public string DefTaxGroupCode { get; set; } = null!;

        public int DefCostingMethod { get; set; }

        public string DefVatProdPostingGroup { get; set; } = null!;

        public string WarrantyPeriod { get; set; } = null!;

        public byte SerialNoNecessary { get; set; }

        public string EkolCode { get; set; } = null!;

        public byte VirtualProducts { get; set; }

        public int ProductType { get; set; }
    }
}

