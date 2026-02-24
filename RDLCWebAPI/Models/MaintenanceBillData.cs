using System;

namespace RDLCWebAPI.Models
{
    public class MaintenanceBillData
    {
        // CustomersMaintenance fields (with CM_ prefix for duplicates)
        public int CM_uid { get; set; }
        public string? CM_CustomerNo { get; set; }
        public string? CM_CustomerName { get; set; }
        public string? CM_BTNo { get; set; }
        public string? CM_MeterNo { get; set; }
        public string? CM_History { get; set; }

        // Non-duplicate CustomersMaintenance fields
        public string? GeneratedMonthYear { get; set; }
        public string? LocationSeqNo { get; set; }
        public string? CNICNo { get; set; }
        public string? FatherName { get; set; }
        public string? InstalledOn { get; set; }
        public string? MobileNo { get; set; }
        public string? TelephoneNo { get; set; }
        public string? MeterType { get; set; }
        public string? NTNNumber { get; set; }
        public string? City { get; set; }
        public string? Project { get; set; }
        public string? SubProject { get; set; }
        public string? TariffName { get; set; }
        public string? BankNo { get; set; }
        public string? BTNoMaintenance { get; set; }
        public string? Category { get; set; }
        public string? Block { get; set; }
        public string? PlotType { get; set; }
        public string? Size { get; set; }
        public string? Sector { get; set; }
        public string? PloNo { get; set; }
        public string? BillStatusMaint { get; set; }
        public string? BillStatus { get; set; }
        public string? BillGenerationStatus { get; set; }
        public string? ConnectionStatus { get; set; }

        // MaintenanceBills fields (with MB_ prefix for duplicates)
        public int MB_uid { get; set; }
        public string? MB_CustomerNo { get; set; }
        public string? MB_CustomerName { get; set; }
        public string? MB_BTNo { get; set; }
        public string? MB_MeterNo { get; set; }
        public string? MB_History { get; set; }

        // Non-duplicate MaintenanceBills fields
        public string? InvoiceNo { get; set; }
        public string? PlotStatus { get; set; }
        public string? BillingMonth { get; set; }
        public string? BillingYear { get; set; }
        public DateTime? BillingDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ValidDate { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? BankDetail { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? MaintCharges { get; set; }
        public int? BillAmountInDueDate { get; set; }
        public int? BillSurcharge { get; set; }
        public int? BillAmountAfterDueDate { get; set; }
        public int? Arrears { get; set; }
        public int? TaxAmount { get; set; }
        public int? Fine { get; set; }
        public int? OtherCharges { get; set; }
        public int? WaterCharges { get; set; }
        public string? FineDept { get; set; }
        public int? MiscCharges { get; set; }
    }
}