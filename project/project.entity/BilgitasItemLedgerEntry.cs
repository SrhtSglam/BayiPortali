using System;
using System.Collections.Generic;

namespace project.entity{
    public partial class BilgitasItemLedgerEntry
    {
        public byte[] Timestamp { get; set; } = null!;

        public int EntryNo { get; set; }

        public string ItemNo { get; set; } = null!;

        public DateTime PostingDate { get; set; }

        public int EntryType { get; set; }

        public string SourceNo { get; set; } = null!;

        public string DocumentNo { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string LocationCode { get; set; } = null!;

        public decimal Quantity { get; set; }

        public decimal RemainingQuantity { get; set; }

        public decimal InvoicedQuantity { get; set; }

        public int AppliesToEntry { get; set; }

        public byte Open { get; set; }

        public string GlobalDimension1Code { get; set; } = null!;

        public string GlobalDimension2Code { get; set; } = null!;

        public byte Positive { get; set; }

        public int SourceType { get; set; }

        public byte DropShipment { get; set; }

        public string TransactionType { get; set; } = null!;

        public string TransportMethod { get; set; } = null!;

        public string CountryRegionCode { get; set; } = null!;

        public string EntryExitPoint { get; set; } = null!;

        public DateTime DocumentDate { get; set; }

        public string ExternalDocumentNo { get; set; } = null!;

        public string Area { get; set; } = null!;

        public string TransactionSpecification { get; set; } = null!;

        public string NoSeries { get; set; } = null!;

        public int DocumentType { get; set; }

        public int DocumentLineNo { get; set; }

        public int OrderType { get; set; }

        public string OrderNo { get; set; } = null!;

        public int OrderLineNo { get; set; }

        public int DimensionSetId { get; set; }

        public byte AssembleToOrder { get; set; }

        public string JobNo { get; set; } = null!;

        public string JobTaskNo { get; set; } = null!;

        public byte JobPurchase { get; set; }

        public string VariantCode { get; set; } = null!;

        public decimal QtyPerUnitOfMeasure { get; set; }

        public string UnitOfMeasureCode { get; set; } = null!;

        public byte DerivedFromBlanketOrder { get; set; }

        public string CrossReferenceNo { get; set; } = null!;

        public string OriginallyOrderedNo { get; set; } = null!;

        public string OriginallyOrderedVarCode { get; set; } = null!;

        public byte OutOfStockSubstitution { get; set; }

        public string ItemCategoryCode { get; set; } = null!;

        public byte Nonstock { get; set; }

        public string PurchasingCode { get; set; } = null!;

        public string ProductGroupCode { get; set; } = null!;

        public byte CompletelyInvoiced { get; set; }

        public DateTime LastInvoiceDate { get; set; }

        public byte AppliedEntryToAdjust { get; set; }

        public byte Correction { get; set; }

        public decimal ShippedQtyNotReturned { get; set; }

        public int ProdOrderCompLineNo { get; set; }

        public string SerialNo { get; set; } = null!;

        public string LotNo { get; set; } = null!;

        public DateTime WarrantyDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int ItemTracking { get; set; }

        public string ReturnReasonCode { get; set; } = null!;

        public string LeasingNo { get; set; } = null!;

        public int LeasingLineNo { get; set; }

        public byte ReserveForContract { get; set; }

        public string ContractCompanyName { get; set; } = null!;

        public int ContractDocumentType { get; set; }

        public string ContractDocumentNo { get; set; } = null!;

        public int ContractDocumentLineNo { get; set; }

        public string BankLoanNo { get; set; } = null!;

        public int BankLoanLineNo { get; set; }

        public int BankLoanTransactionType { get; set; }

        public string AntrepoDocumentNo { get; set; } = null!;

        public int AntrepoDocLineNo { get; set; }

        public byte WhseReclassified { get; set; }

        public string CustomerNo { get; set; } = null!;

        public string AgreementNo { get; set; } = null!;

        public string DummyVariantCode { get; set; } = null!;

        public byte DummyCheck { get; set; }

        public string ServiceDeviceNo { get; set; } = null!;

        public string FaNo { get; set; } = null!;

        public byte Accesory { get; set; }

        public string UniqueId { get; set; } = null!;

        public string ShipToCode { get; set; } = null!;

        public string SalespersonCode { get; set; } = null!;

        public int PargeProdType { get; set; }

        public string SapTenderNo { get; set; } = null!;

        public byte ItemFaTransfered { get; set; }

        public DateTime FaturaTarihi { get; set; }

        public string NavFatNo { get; set; } = null!;

        public byte Reversed { get; set; }

        public int AppliedDocType { get; set; }

        public string AppliedDocNo { get; set; } = null!;

        public string SourceName { get; set; } = null!;

        public string ForeignTradeNo { get; set; } = null!;

        public string ForeignTradeDeclarationNo { get; set; } = null!;
    }
}

