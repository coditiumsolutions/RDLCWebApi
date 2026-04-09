using System;

namespace RDLCWebAPI.Models
{
    public class MaintenanceBillData
    {
        // CustomersMaintenance fields
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
        public string? PhaseName { get; set; }
        public string? Category { get; set; }
        public string? Size { get; set; }
        public string? Sector { get; set; }
        public string? PloNo { get; set; }
        public string? BillGenerationStatus { get; set; }
        public string? ConnectionStatus { get; set; }
        public string? CM_PlotStatus { get; set; }
        public string? StreetNumber { get; set; }
        public string? UnitType { get; set; }

        // MaintenanceBills fields
        public int MB_uid { get; set; }
        public string? MB_KuickPayNo { get; set; }
        public string? MB_CustomerName { get; set; }
        public string? MB_BTNo { get; set; }
        public string? MB_History { get; set; }
        public string? Plot_Number { get; set; }
        public string? Street_Number { get; set; }
        public string? MB_PhaseName { get; set; }    // ✅ Instead of PhaseName1
        public string? MB_Category { get; set; }     // ✅ Instead of Category1
        public string? MB_Project { get; set; }      // ✅ Instead of Project1
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

        // New Fields
        public int? RentAmount { get; set; }
        public int? FoodSafety { get; set; }
        public int? TrollyTrip { get; set; }
        public int? ExtraWork { get; set; }
        public int? DieselCost { get; set; }
    }
}