using System;
using System.Collections.Generic;

namespace project.entity{

    public partial class BilgitasCustomer
    {
        public byte[] Timestamp { get; set; } = null!;

        public string No { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string SearchName { get; set; } = null!;

        public string Name2 { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Address2 { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Contact { get; set; } = null!;

        public string PhoneNo { get; set; } = null!;

        public string TelexNo { get; set; } = null!;

        public string DocumentSendingProfile { get; set; } = null!;

        public string OurAccountNo { get; set; } = null!;

        public string TerritoryCode { get; set; } = null!;

        public string GlobalDimension1Code { get; set; } = null!;

        public string GlobalDimension2Code { get; set; } = null!;

        public string ChainName { get; set; } = null!;

        public decimal BudgetedAmount { get; set; }

        public decimal CreditLimitLcy { get; set; }

        public string CustomerPostingGroup { get; set; } = null!;

        public string CurrencyCode { get; set; } = null!;

        public string CustomerPriceGroup { get; set; } = null!;

        public string LanguageCode { get; set; } = null!;

        public int StatisticsGroup { get; set; }

        public string PaymentTermsCode { get; set; } = null!;

        public string FinChargeTermsCode { get; set; } = null!;

        public string SalespersonCode { get; set; } = null!;

        public string ShipmentMethodCode { get; set; } = null!;

        public string ShippingAgentCode { get; set; } = null!;

        public string PlaceOfExport { get; set; } = null!;

        public string InvoiceDiscCode { get; set; } = null!;

        public string CustomerDiscGroup { get; set; } = null!;

        public string CountryRegionCode { get; set; } = null!;

        public string CollectionMethod { get; set; } = null!;

        public decimal Amount { get; set; }

        public int Blocked { get; set; }

        public int InvoiceCopies { get; set; }

        public int LastStatementNo { get; set; }

        public byte PrintStatements { get; set; }

        public string BillToCustomerNo { get; set; } = null!;

        public int Priority { get; set; }

        public string PaymentMethodCode { get; set; } = null!;

        public DateTime LastDateModified { get; set; }

        public int ApplicationMethod { get; set; }

        public byte PricesIncludingVat { get; set; }

        public string LocationCode { get; set; } = null!;

        public string FaxNo { get; set; } = null!;

        public string TelexAnswerBack { get; set; } = null!;

        public string VatRegistrationNo { get; set; } = null!;

        public byte CombineShipments { get; set; }

        public string GenBusPostingGroup { get; set; } = null!;

        public byte[]? Picture { get; set; }

        public string Gln { get; set; } = null!;

        public string PostCode { get; set; } = null!;

        public string County { get; set; } = null!;

        public string EMail { get; set; } = null!;

        public string HomePage { get; set; } = null!;

        public string ReminderTermsCode { get; set; } = null!;

        public string NoSeries { get; set; } = null!;

        public string TaxAreaCode { get; set; } = null!;

        public byte TaxLiable { get; set; }

        public string VatBusPostingGroup { get; set; } = null!;

        public int Reserve { get; set; }

        public byte BlockPaymentTolerance { get; set; }

        public string IcPartnerCode { get; set; } = null!;

        public decimal Prepayment { get; set; }

        public int PartnerType { get; set; }

        public string PreferredBankAccount { get; set; } = null!;

        public string CashFlowPaymentTermsCode { get; set; } = null!;

        public string PrimaryContactNo { get; set; } = null!;

        public string ResponsibilityCenter { get; set; } = null!;

        public int ShippingAdvice { get; set; }

        public string ShippingTime { get; set; } = null!;

        public string ShippingAgentServiceCode { get; set; } = null!;

        public string ServiceZoneCode { get; set; } = null!;

        public byte AllowLineDisc { get; set; }

        public string BaseCalendarCode { get; set; } = null!;

        public int CopySellToAddrToQteFrom { get; set; }

        public int CustomerSegment { get; set; }

        public decimal Profit { get; set; }

        public string ParentReference { get; set; } = null!;

        public int WebVisibility { get; set; }

        public byte KaraListe { get; set; }

        public string FinansEMail { get; set; } = null!;

        public string MuhasebeEMail { get; set; } = null!;

        public string BilgiİşlemEMail { get; set; } = null!;

        public string SatınalmaEMail { get; set; } = null!;

        public string InfoEMail { get; set; } = null!;

        public string BaBsAgreementMail { get; set; } = null!;

        public string Iban { get; set; } = null!;

        public string KepEMail { get; set; } = null!;

        public byte PayeeOnEInvoice { get; set; }

        public decimal CreditLimitCurr { get; set; }

        public string CreditLimitCurrency { get; set; } = null!;

        public string EInvoiceCodeList { get; set; } = null!;

        public string FinancialAffairsMailAdress { get; set; } = null!;

        public byte SasInfoMandOnEInvoice { get; set; }

        public string CssdDealerCat { get; set; } = null!;

        public byte KurFarkıHesaplanmasın { get; set; }

        public byte ManualBlockageTrack { get; set; }

        public byte KbaFaturasıKesilebilir { get; set; }

        public int ServiceBlockType { get; set; }

        public int ItemBlockType { get; set; }

        public int CounterCollectionDay { get; set; }

        public string CustomerGroupNo { get; set; } = null!;

        public int IdType { get; set; }

        public int ServicePriority { get; set; }

        public string SectorCode { get; set; } = null!;

        public string CustomerType { get; set; } = null!;

        public int EmployeeCount { get; set; }

        public int ServiceDefinition { get; set; }

        public string IdcSectorDesc { get; set; } = null!;

        public string SectorDesc { get; set; } = null!;

        public string MdsLocation { get; set; } = null!;

        public string EArchiveEMailAddress { get; set; } = null!;

        public string CustomerCategory { get; set; } = null!;

        public byte DontIncludePromNoteAmount { get; set; }

        public string Bpmno { get; set; } = null!;

        public string KippsGroup { get; set; } = null!;

        public DateTime ModifyDatetime { get; set; }

        public string UnrealizedGainsAcc { get; set; } = null!;

        public string RealizedGainsAcc { get; set; } = null!;

        public string UnrealizedLossesAcc { get; set; } = null!;

        public string RealizedLossesAcc { get; set; } = null!;

        public byte ForExport { get; set; }

        public byte DonTIncludeToBaBs { get; set; }

        public string PersonalIdentityNo { get; set; } = null!;

        public decimal ChequeRiskLimit { get; set; }

        public decimal PromNoteLimit { get; set; }

        public string TaxRegistrationNo { get; set; } = null!;

        public string VendorNo { get; set; } = null!;

        public int ExchNoteRiskControl { get; set; }

        public byte IncludeInErcInv { get; set; }

        public int Reconcile { get; set; }

        public DateTime CreatedDate { get; set; }

        public byte CompatibilityAdditional { get; set; }

        public byte InvoiceOver90Days { get; set; }

        public string SapCompanyNo { get; set; } = null!;
    }
}
