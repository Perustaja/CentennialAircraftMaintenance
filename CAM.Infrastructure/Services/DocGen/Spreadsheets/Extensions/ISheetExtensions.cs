using System.Collections.Generic;
using CAM.Core.Entities;
using CAM.Core.Entities.DiscrepancyAggregate;
using CAM.Infrastructure.Services.DocGen.Helpers;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace CAM.Infrastructure.DocGen.Spreadsheets.Extensions
{
    public static class ISheetExtensions
    {
        ///<summary>
        /// Sets the sheet up with default sizes, print settings, etc. for discrepancies.
        ///</summary>
        public static void SetupForDiscrepancy(this ISheet sheet)
        {
            // Default sizes
            sheet.DefaultColumnWidth = 8;
            sheet.DefaultRowHeight = 350;
            // Print setup
            sheet.PrintSetup.Landscape = true;
            sheet.SetMargin(MarginType.BottomMargin, 0.2d);
            sheet.SetMargin(MarginType.TopMargin, 0.5d);
            sheet.SetMargin(MarginType.LeftMargin, 0.2d);
            sheet.SetMargin(MarginType.RightMargin, 0.2d);
        }

        ///<summary>
        /// Writes a thin buffer row to the sheet at a given index.
        ///</summary>
        public static void WriteBufferRow(this ISheet sheet, int startIndex)
        {
            IRow bufferRow = sheet.CreateRow(startIndex);
            bufferRow.HeightInPoints = 5;
        }

        ///<summary>
        /// Writes the header with information based on the Discrepancy's properties.
        ///</summary>
        /// See <see cref="DiscrepancySheetExtensions.IWorkbookExtensions"/> for dictionaries.
        public static void WriteHeader(this ISheet sheet, Dictionary<string, ICellStyle> styleDict,
        Dictionary<string, IFont> fontDict, Discrepancy discrep)
        {
            /// CellStyles
            // headerLabelStyle
            ICellStyle labelStyle;
            styleDict.TryGetValue("headerLabelStyle", out labelStyle);

            // headerValueStyle
            ICellStyle valueStyle;
            styleDict.TryGetValue("headerValueStyle", out valueStyle);

            /// Fonts
            // headerLabelFont
            IFont labelFont;
            fontDict.TryGetValue("headerLabelFont", out labelFont);

            // headerValueFont
            IFont valueFont;
            fontDict.TryGetValue("headerValueFont", out valueFont);

            // Sizing, AutoSizeColumn() has issues with performance
            sheet.SetColumnWidth(5, 2500);
            sheet.SetColumnWidth(6, 2000);
            sheet.SetColumnWidth(7, 2000);
            sheet.SetColumnWidth(8, 2000);

            //// First Row
            /// WO
            IRow row = sheet.CreateRow(0);
            row.Height = 400;
            ICell cell = row.CreateCell(0);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 1));
            cell.SetCellValue(RichText.CreateRichTextString(
                "Work Order #: ",
                labelFont
            ));
            cell.CellStyle = labelStyle;

            // value
            string workOrder = discrep.WorkOrderId > 1 ? discrep.WorkOrderId.ToString() : "N/A";

            cell = row.CreateCell(2);
            cell.SetCellValue(RichText.CreateRichTextString(
                workOrder,
                valueFont
            ));
            cell.CellStyle = valueStyle;

            /// RegistrationNum
            cell = row.CreateCell(3);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 3, 4));
            cell.SetCellValue(RichText.CreateRichTextString(
                "Registration #:",
                labelFont
            ));
            cell.CellStyle = labelStyle;

            // value
            cell = row.CreateCell(5);
            cell.SetCellValue(RichText.CreateRichTextString(
                $"{discrep.AircraftId}",
                valueFont
            ));
            cell.CellStyle = valueStyle;

            /// Hobbs
            cell = row.CreateCell(9);
            cell.SetCellValue(RichText.CreateRichTextString(
                "Hobbs:",
                labelFont
            ));
            cell.CellStyle = labelStyle;

            // value
            string hobbs = discrep.Hobbs > 1 ? discrep.Hobbs.ToString() : "N/A";

            cell = row.CreateCell(10);
            cell.SetCellValue(RichText.CreateRichTextString(
                hobbs,
                valueFont
            ));
            cell.CellStyle = valueStyle;

            /// Tach
            cell = row.CreateCell(11);
            cell.SetCellValue(RichText.CreateRichTextString(
                "Tach:",
                labelFont
            ));
            cell.CellStyle = labelStyle;

            // value
            string tach = discrep.Tach1 > 0 ? discrep.Tach1.ToString() : "N/A";

            cell = row.CreateCell(12);
            cell.SetCellValue(RichText.CreateRichTextString(
                "tach",
                valueFont
            ));
            cell.CellStyle = valueStyle;

            /// AircraftTotalTime
            cell = row.CreateCell(13);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 13, 14));
            cell.SetCellValue(RichText.CreateRichTextString(
                "Aircraft Total:",
                labelFont
            ));
            cell.CellStyle = labelStyle;

            // value
            cell = row.CreateCell(15);
            cell.SetCellValue(RichText.CreateRichTextString(
                $"{discrep.AircraftTotal}",
                valueFont
            ));
            cell.CellStyle = valueStyle;
        }

        ///<summary>
        /// Writes the area necessary for a discrepancy, starting at the given index.
        ///</summary>
        /// See <see cref="DiscrepancySheetExtensions.IWorkbookExtensions"/> for dictionaries.

        public static void WriteDiscrepancy(this ISheet sheet, Dictionary<string, ICellStyle> styleDict
        , Dictionary<string, IFont> fontDict, int startIndex, Discrepancy discrep)
        {
            /// CellStyles
            // tagStyle - for main discrepancy cell
            ICellStyle tagStyle;
            styleDict.TryGetValue("tagStyle", out tagStyle);

            // subTagStyle - for corrective action cell
            ICellStyle subTagStyle;
            styleDict.TryGetValue("subTagStyle", out subTagStyle);

            // laborStyle - for the labor field cells
            ICellStyle laborStyle;
            styleDict.TryGetValue("laborStyle", out laborStyle);

            // partsStyle - for the parts cells
            ICellStyle partsStyle;
            styleDict.TryGetValue("partsStyle", out partsStyle);

            // textAreaStyle - for merged text areas
            ICellStyle textAreaStyle;
            styleDict.TryGetValue("textAreaStyle", out textAreaStyle);


            // fieldStyle - for entered fields under parts and labor
            ICellStyle fieldStyle;
            styleDict.TryGetValue("fieldStyle", out fieldStyle);

            /// Fonts
            // labelFont - for large main row text
            IFont labelFont;
            fontDict.TryGetValue("labelFont", out labelFont);

            // textAreaFont - for text areas
            IFont textAreaFont;
            fontDict.TryGetValue("textAreaFont", out textAreaFont);

            // techFont - for technician field (bold)
            IFont techFont;
            fontDict.TryGetValue("techFont", out techFont);

            // fieldFont - for use in text fields e.g. hours, qty, part number
            IFont fieldFont;
            fontDict.TryGetValue("fieldFont", out fieldFont);

            /// Main
            // Discrepancy tag
            IRow row = sheet.CreateRow(startIndex);
            ICell cell = row.CreateCell(0);
            sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, 0, 9));
            cell.SetCellValue(RichText.CreateRichTextString(
                $" Discrepancy #{discrep.Id}:",
                labelFont
            ));
            cell.CellStyle = tagStyle;

            // LaborRecord tags
            for (int i = 10; i < 15; i += 3)
            {
                cell = row.CreateCell(i);
                sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, i, i + 1));
                cell.SetCellValue(RichText.CreateRichTextString(
                    "Technician",
                    labelFont
                ));
                cell.CellStyle = laborStyle;

                cell = row.CreateCell(i + 2);
                cell.SetCellValue(RichText.CreateRichTextString(
                    "Hours",
                    labelFont
                ));
                cell.CellStyle = laborStyle;
            }


            // Discrepancy text area
            row = sheet.CreateRow(startIndex + 1);
            cell = row.CreateCell(0);
            sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum + 3, 0, 9));
            cell.SetCellValue(RichText.CreateRichTextString(
                discrep.Description,
                textAreaFont
            ));
            cell.CellStyle = textAreaStyle;

            // In order to not overwrite our bottom border later
            row = sheet.CreateRow(startIndex + 4);
            cell = row.CreateCell(0);
            cell.CellStyle = textAreaStyle;

            // Labor record fields
            // Variables for checks
            var recordCount = discrep.LaborRecords.Count;
            var recordIndex = 0;
            for (int i = startIndex + 1; i < startIndex + 5; i++)
            {
                // Create the row unless it's the first one created earlier
                row = (i == startIndex + 1 || i == startIndex + 4)
                    ? sheet.GetRow(i) : sheet.CreateRow(i);

                // Create the cells and fill them if necessary
                sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, 10, 11));
                cell = row.CreateCell(10);
                if (recordIndex <= recordCount - 1)
                {
                    cell.SetCellValue(RichText.CreateRichTextString(
                    $"{discrep.LaborRecords[recordIndex].Employee.Initials} " +
                    $"{discrep.LaborRecords[recordIndex].Employee.CertificationNum}",
                    fieldFont
                ));
                }
                cell.CellStyle = fieldStyle;

                cell = row.CreateCell(12);
                if (recordIndex <= recordCount - 1)
                {
                    cell.SetCellValue(RichText.CreateRichTextString(
                       $"{discrep.LaborRecords[recordIndex].LaborInHours}",
                       fieldFont
                   ));
                }
                cell.CellStyle = fieldStyle;

                sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, 13, 14));
                cell = row.CreateCell(13);
                if (recordIndex + 4 <= recordCount - 1)
                {
                    cell.SetCellValue(RichText.CreateRichTextString(
                    $"{discrep.LaborRecords[recordIndex + 4].Employee.Initials} " +
                    $"{discrep.LaborRecords[recordIndex + 4].Employee.CertificationNum}",
                    fieldFont
                ));
                }
                cell.CellStyle = fieldStyle;

                cell = row.CreateCell(15);
                if (recordIndex + 4 <= recordCount - 1)
                {
                    cell.SetCellValue(RichText.CreateRichTextString(
                       $"{discrep.LaborRecords[recordIndex + 4].LaborInHours}",
                       fieldFont
                   ));
                }
                cell.CellStyle = fieldStyle;
            }

            // Corrective Action tag
            row = sheet.CreateRow(startIndex + 5);
            cell = row.CreateCell(0);
            sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, 0, 9));
            cell.SetCellValue(RichText.CreateRichTextString(
                " Corrective Action:",
                labelFont
            ));
            cell.CellStyle = subTagStyle;

            // Parts tags
            for (int i = 10; i < 15; i += 3)
            {
                cell = row.CreateCell(i);
                sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, i, i + 1));
                cell.SetCellValue(RichText.CreateRichTextString(
                    "Part Number",
                    labelFont
                ));
                cell.CellStyle = partsStyle;

                cell = row.CreateCell(i + 2);
                cell.SetCellValue(RichText.CreateRichTextString(
                    "Qty",
                    labelFont
                ));
                cell.CellStyle = partsStyle;
            }


            // Corrective Action text area
            row = sheet.CreateRow(startIndex + 6);
            cell = row.CreateCell(0);
            var mergedTextArea = new CellRangeAddress(row.RowNum, row.RowNum + 3, 0, 9);
            sheet.AddMergedRegion(mergedTextArea);
            cell.SetCellValue(RichText.CreateRichTextString(
                discrep.Resolution,
                textAreaFont
            ));
            cell.CellStyle = textAreaStyle;

            // In order to not overwrite our bottom border later
            row = sheet.CreateRow(startIndex + 9);
            cell = row.CreateCell(0);
            cell.CellStyle = textAreaStyle;

            // Variables for checks
            var partsCount = discrep.DiscrepancyParts.Count;
            var partsIndex = 0;
            for (int i = startIndex + 6; i < startIndex + 10; i++)
            {
                // Create the row if necessary, if not get it so we don't overwrite
                row = (i == startIndex + 6 || i == startIndex + 9)
                    ? sheet.GetRow(i) : sheet.CreateRow(i);

                // Create the cells to be filled, merging if necessary
                cell = row.CreateCell(10);
                sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, 10, 11));
                if (partsIndex <= partsCount - 1)
                {
                    cell.SetCellValue(RichText.CreateRichTextString(
                    $"{discrep.DiscrepancyParts[partsIndex].Part.PartNumber}",
                    fieldFont
                ));
                }
                cell.CellStyle = fieldStyle;

                cell = row.CreateCell(12);
                if (partsIndex <= partsCount - 1)
                {
                    cell.SetCellValue(RichText.CreateRichTextString(
                       $"{discrep.DiscrepancyParts[partsIndex].Qty}",
                       fieldFont
                   ));
                }
                cell.CellStyle = fieldStyle;

                cell = row.CreateCell(13);
                sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, 13, 14));
                if (partsIndex + 4 <= partsCount - 1)
                {
                    cell.SetCellValue(RichText.CreateRichTextString(
                    $"{discrep.DiscrepancyParts[partsIndex + 4].Part.PartNumber}",
                    fieldFont
                ));
                }
                cell.CellStyle = fieldStyle;


                cell = row.CreateCell(15);
                if (partsIndex + 4 <= partsCount - 1)
                {
                    cell.SetCellValue(RichText.CreateRichTextString(
                       $"{discrep.DiscrepancyParts[partsIndex + 4].Qty}",
                       fieldFont
                   ));
                }
                cell.CellStyle = fieldStyle;
            }
        }
    }
}