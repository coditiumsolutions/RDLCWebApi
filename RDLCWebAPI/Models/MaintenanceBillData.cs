using System;

namespace RDLCWebAPI.Models
{
    public class MaintenanceBillData
    {
        // ========== CustomersMaintenance Table Fields ==========
        public int CM_uid { get; set; }
        public string? CM_KuickPayNo { get; set; }
        public string? CM_CustomerName { get; set; }
        public string? CM_BTNo { get; set; }
        public string? CM_History { get; set; }
        public string? GeneratedMonthYear { get; set; }
        public string? LocationSeqNo { get; set; }
        public string? CNICNo { get; set; }
        public string? FatherName { get; set; }
        public string? MobileNo { get; set; }
        public string? City { get; set; }
        public string? Project { get; set; }
        public string? PhaseName { get; set; }           // ← CHANGE: PhaseName se Phase
        public string? Category { get; set; }
        public string? Size { get; set; }
        public string? Sector { get; set; }
        public string? PlotNo { get; set; }          // ← CHANGE: PloNo se PlotNo
        public string? BillGenerationStatus { get; set; }
        public string? ConnectionStatus { get; set; }
        public string? CM_PlotStatus { get; set; }
        public string? StreetNo { get; set; }        // ← CHANGE: StreetNumber se StreetNo
        public string? UnitType { get; set; }

        // Extra charges from CustomersMaintenance
        public double? Maint { get; set; }
        public double? Misc { get; set; }
        public double? Water { get; set; }
        public double? Rent { get; set; }
        public double? Generator { get; set; }
        public double? Other { get; set; }
        public double? foodsafety { get; set; }
        public double? trollytrip { get; set; }
        public double? extrawork { get; set; }

        // ========== MaintenanceBills Table Fields (as per your schema) ==========
        public int MB_uid { get; set; }
        public string? MB_KuickPayNo { get; set; }
        public string? MB_CustomerName { get; set; }
        public string? MB_BTNo { get; set; }
        public string? MB_History { get; set; }
        public string? Plot_Number { get; set; }
        public string? Street_Number { get; set; }
        public string? MB_PhaseName { get; set; }        // ← CHANGE: MB_PhaseName se MB_Phase
        public string? MB_Category { get; set; }
        public string? MB_Project { get; set; }
        public string? MB_PlotStatus { get; set; }
        public string? BillingMonth { get; set; }
        public string? BillingYear { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? BankDetail { get; set; }
        public string? PAIDBYOPERATOR { get; set; }
        public int? AMOUNTPAID { get; set; }
        public int? MaintCharges { get; set; }
        public int? WaterCharges { get; set; }
        public int? OtherCharges { get; set; }
        public int? MiscCharges { get; set; }
        public int? installamount { get; set; }
        public int? current_gst { get; set; }
        public int? Arrears { get; set; }
        public int? PreviousArrears { get; set; }
        public int? advance_payment { get; set; }
        public int? AdvanceAmount { get; set; }
        public int? BillAmountInDueDate { get; set; }
        public int? BillSurcharge { get; set; }
        public int? BillAmountAfterDueDate { get; set; }
        public int? GTotal { get; set; }
        public string? compute { get; set; }
        public DateTime? conndate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string? PushedBy { get; set; }
        public DateTime? PushedOn { get; set; }
    }
}