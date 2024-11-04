namespace GestionInventario.src.Modules.Movements.Domains.DTOs
{
    public class SupplierReceiptRequest
    {
    public Guid SupplierId { get; set; }
    public List<SupplierReceiptItem> Items { get; set; }       
    }
}