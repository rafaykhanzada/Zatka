namespace Core.Data.DTO
{

    public class InvoiceVM
    {
        public string? InvoiceNo { get; set; }
        public string? Manufacture { get; set; }
        public string? Customer { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? TypeOfProduct { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; }
        public string? LotNumber { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public decimal? LotQty { get; set; }
        public decimal? Qty { get; set; }
        public string? QtyUnit { get; set; }
        public string? ShipToCustomer { get; set; }
        public string? Truckinfo { get; set; }
    }
    public class InvoiceHeaderVM
    {
        public string? InvoiceNo { get; set; }
        public string? Manufacture { get; set; }
        public string? Customer { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? TypeOfProduct { get; set; }
        public string? ShipToCustomer { get; set; }
        public string? Truckinfo { get; set; }
        public List<LinesVM>? Lines { get; set; }
    }
    public class LinesVM
    {
  
        public string? TypeOfProduct { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; }
        public List<LotsVM>? Lots { get; set; }

    }
    public class LotsVM
    {
        public string? LotNumber { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public decimal? LotQty { get; set; }
    }
}
