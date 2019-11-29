using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using CAM.Core.Interfaces;
using CAM.Core.Entities;
using CAM.Infrastructure.Services.DocGen.Spreadsheets.Extensions;

namespace CAM.Infrastructure.Services
{
    public class DocumentGenerator : IDocumentGenerator
    {
        ///<summary>
        /// Generates the documents and spreadsheets necessary after being passed a WorkOrder.
        ///</summary>
        public void GenerateWorkOrder(WorkOrder workOrder)
        {
            // Initialize with path and filestream
            var newFile = $"~/Documents/WorkOrder{workOrder.Id}.xlsx";

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                workbook.CreateDiscrepancySheets(workOrder.Discrepancies);

                workbook.Write(fs);
            }
        }
        ///<summary>
        /// Generates the documents and spreadsheet necessary after being passed a Discrepancy.
        ///</summary>
        public void GenerateDiscrepancySingle(Discrepancy discrep)
        {
            // Initialize with path and filestream
            var newFile = $"~/Documents/Discrepancy{discrep.Id}.xlsx";

            var list = new List<Discrepancy>();
            list.Add(discrep);

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                workbook.CreateDiscrepancySheets(list);
                
                workbook.Write(fs);
            }
        }
    }
}