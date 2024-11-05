using GestionInventario.src.Data;
using GestionInventario.src.Modules.Movements.Domains.DTOs;
using OfficeOpenXml;

namespace GestionInventario.src.Modules.Movements.Services
{
    public class MovementSupplierService(MyDbContext myDbContext) : IMovementSupplierService
    {
        private readonly MyDbContext _context = myDbContext;
        public void UpdateBySupplierReceipt(IFormFile formFile)
            {
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    using var stream = new MemoryStream();
                    formFile.CopyTo(stream);
                    stream.Position = 0;

                    using var package = new ExcelPackage(stream);
                    var worksheet = package.Workbook.Worksheets[0];

                    var receiptRequest = new SupplierReceiptRequest
                    {
                        SupplierId = Guid.Parse(worksheet.Cells[1, 2].Text),
                        Items = []
                    };

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var item = new SupplierReceiptItem
                        {
                            ProductId = Guid.Parse(worksheet.Cells[row, 1].Text),
                            Quantity = int.Parse(worksheet.Cells[row, 2].Text)
                        };
                        receiptRequest.Items.Add(item);
                    }

                    // Lógica para actualizar el inventario basado en receiptRequest
                    

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
    }
}