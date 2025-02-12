using System;
using System.Collections.Generic;

namespace project.entity{
    public partial class BilgitasProductGroup
    {
        public byte[] Timestamp { get; set; } = null!;

        public string ItemCategoryCode { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string WarehouseClassCode { get; set; } = null!;

        public string EkolCode { get; set; } = null!;
    }
}

