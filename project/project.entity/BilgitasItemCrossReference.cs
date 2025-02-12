using System;
using System.Collections.Generic;

namespace project.entity{
    public partial class BilgitasItemCrossReference
    {
        public byte[] Timestamp { get; set; } = null!;

        public string ItemNo { get; set; } = null!;

        public string VariantCode { get; set; } = null!;

        public string UnitOfMeasure { get; set; } = null!;

        public int CrossReferenceType { get; set; }

        public string CrossReferenceTypeNo { get; set; } = null!;

        public string CrossReferenceNo { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte DiscontinueBarCode { get; set; }

        public DateTime LastModifyDateTime { get; set; }

        public byte Purchase { get; set; }

        public string SapCode { get; set; } = null!;

        public byte SapCodeConfirmed { get; set; }

        public string SapCodeConfirmUserId { get; set; } = null!;

        public DateTime SapCodeConfirmDatetime { get; set; }
    }
}
