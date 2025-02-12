using System;
using System.Collections.Generic;

namespace project.entity{
    public partial class BilgitasServiceDeviceCounter
    {
        public byte[] Timestamp { get; set; } = null!;

        public string ServiceDeviceNo { get; set; } = null!;

        public int Year { get; set; }

        public int Month { get; set; }

        public DateTime Period { get; set; }

        public DateTime Date { get; set; }

        public string Explanation { get; set; } = null!;

        public int FirstCounterSb { get; set; }

        public int LastCounterSb { get; set; }

        public int FirstCounterRk { get; set; }

        public int LastCounterRk { get; set; }

        public string BayiNo { get; set; } = null!;

        public int ServiceDeviceEntryNoSb { get; set; }

        public int ServiceDeviceEntryNoRk { get; set; }

        public string PersonResponsible { get; set; } = null!;

        public string CustomerNo { get; set; } = null!;

        public DateTime EntryDate { get; set; }
    }
}

