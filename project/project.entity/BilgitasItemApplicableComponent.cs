using System;
using System.Collections.Generic;

namespace project.entity{
    public partial class BilgitasItemApplicableComponent
    {
        public byte[] Timestamp { get; set; } = null!;

        public string ItemNo { get; set; } = null!;

        public string ComponentItemNo { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte ShortTermComponent { get; set; }

        public string ItemClass { get; set; } = null!;

        public int TonerLife { get; set; }

        public int Color { get; set; }

        public int KfsTonerGroup { get; set; }

        public string TonerInformation { get; set; } = null!;
    }
}

