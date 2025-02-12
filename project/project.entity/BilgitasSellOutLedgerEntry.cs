using System;
using System.Collections.Generic;

namespace project.entity{
    public partial class BilgitasSellOutLedgerEntry
    {
        public byte[] Timestamp { get; set; } = null!;

        public int EntryNo { get; set; }

        public int ItemLedgEntryNo { get; set; }

        public string CustomerNo { get; set; } = null!;

        public string ItemNo { get; set; } = null!;

        public string ItemCategoryCode { get; set; } = null!;

        public string ProductGroupCode { get; set; } = null!;

        public decimal Quantity { get; set; }

        public string SoldToName { get; set; } = null!;

        public string SoldToContactName { get; set; } = null!;

        public string SoldToAddress { get; set; } = null!;

        public string SoldToAddress2 { get; set; } = null!;

        public string SoldToPhoneNo { get; set; } = null!;

        public DateTime InvoiceDate { get; set; }

        public byte Positive { get; set; }

        public string SerialNo { get; set; } = null!;

        public string TaxAreaCode { get; set; } = null!;

        public string TaxRegistrationNo { get; set; } = null!;

        public string SoldToCity { get; set; } = null!;

        public string SoldToFaxNo { get; set; } = null!;

        public string SoldToEMail { get; set; } = null!;

        public string SoldToWebSite { get; set; } = null!;

        public DateTime SetupDate { get; set; }

        public string InvoiceNo { get; set; } = null!;

        public DateTime BilgitaşInvoiceDate { get; set; }

        public string BilgitaşInvoiceNo { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte GovenmentAgency { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}

