using System;
using System.Collections.Generic;
using CAM.Core.Entities.DiscrepancyAggregate;
using CAM.Infrastructure.DocGen.Spreadsheets.Extensions;
using NPOI.SS.UserModel;

namespace CAM.Infrastructure.Services.DocGen.Spreadsheets.Extensions
{
    public static class IWorkbookExtensions
    {
        ///<summary>
        /// Creates a single-or-multi-sheet spreadsheet from a List of Discrepancies.
        ///</summary>
        public static void CreateDiscrepancySheets(this IWorkbook workbook, List<Discrepancy> discreps)
        {
            var discCount = discreps.Count; // Tracking our total number of discreps
            var discIndex = 0; // Tracking our current index across groups
            var sheetIndex = 1; // Tracking our current sheet number
            for (var groupCount = (decimal)discreps.Count / 3; groupCount > 0; groupCount--)
            {
                // Create a sheet 
                var sheet = workbook.CreateSheet($"DiscrepanciesPage{sheetIndex}");
                sheet.SetupForDiscrepancy();
                sheetIndex++;

                // Create the styles and font dictionaries to pass to extension methods
                var styleDict = workbook.CreateCellStyleDict();
                var fontDict = workbook.CreateFonts();

                // Write a header, always using our first discrepancy for the information
                sheet.WriteHeader(styleDict, fontDict, discreps[0]);
                sheet.WriteBufferRow(1);

                var startingIndex = 2; // How we will track our starting index

                // In each group, we write up to a maximum of 3 discrepancies.
                for (int i = 0; i < 2; i++)
                {

                    // If our current index is below or at the last index in the list
                    if (discIndex <= discCount - 1)
                    {
                        sheet.WriteDiscrepancy(styleDict, fontDict, startingIndex, discreps[discIndex]);
                        sheet.WriteBufferRow(startingIndex + 10);

                        discIndex++; // Update our index
                    }
                    // TODO: add an empty discrepancy object somewhere to fill out blank fields
                    else
                    {
                    }
                    startingIndex += 11; // Add 11 to the starting index for the next discrepancy
                }
            }
        }
        ///<summary>
        /// Creates the ICellStyles to be used and returns them in a Dictionary.
        ///</summary>
        public static Dictionary<string, ICellStyle> CreateCellStyleDict(this IWorkbook workbook)
        {
            // Color variables, for ease of use in changing the theme
            var mainColor = IndexedColors.Grey80Percent.Index;
            var subColor = IndexedColors.Grey25Percent.Index;
            var gridColor = IndexedColors.Black.Index;

            // Creation of the dictionary
            var dict = new Dictionary<string, ICellStyle>();

            // Header stuff
            // headerLabelStyle
            ICellStyle headerLabelStyle = workbook.CreateCellStyle();
            headerLabelStyle.Alignment = HorizontalAlignment.Center;
            dict.Add("headerLabelStyle", headerLabelStyle);

            // headerValueStyle
            ICellStyle headerValueStyle = workbook.CreateCellStyle();
            headerValueStyle.BorderBottom = BorderStyle.Medium;
            headerValueStyle.Alignment = HorizontalAlignment.Center;
            dict.Add("headerValueStyle", headerValueStyle);

            /// Main discrepancy stuff
            // tagStyle - for main discrepancy cell
            ICellStyle tagStyle = workbook.CreateCellStyle();
            tagStyle.Alignment = HorizontalAlignment.Left;
            tagStyle.FillForegroundColor = mainColor;
            tagStyle.FillPattern = FillPattern.SolidForeground;
            tagStyle.BorderLeft = BorderStyle.Hair;
            tagStyle.LeftBorderColor = gridColor;
            dict.Add("tagStyle", tagStyle);

            // subTagStyle - for corrective action cell
            ICellStyle subTagStyle = workbook.CreateCellStyle();
            subTagStyle.Alignment = HorizontalAlignment.Left;
            subTagStyle.FillPattern = FillPattern.SolidForeground;
            subTagStyle.FillForegroundColor = subColor;
            subTagStyle.BorderLeft = BorderStyle.Hair;
            subTagStyle.LeftBorderColor = gridColor;
            dict.Add("subTagStylee", subTagStyle);

            // laborStyle - for the labor field cells
            ICellStyle laborStyle = workbook.CreateCellStyle();
            laborStyle.Alignment = HorizontalAlignment.Center;
            laborStyle.FillForegroundColor = mainColor;
            laborStyle.FillPattern = FillPattern.SolidForeground;
            laborStyle.BorderLeft = BorderStyle.Hair;
            laborStyle.BorderRight = BorderStyle.Hair;
            laborStyle.LeftBorderColor = gridColor;
            laborStyle.RightBorderColor = gridColor;
            dict.Add("laborStyle", laborStyle);

            // partsStyle - for the parts cells
            ICellStyle partsStyle = workbook.CreateCellStyle();
            partsStyle.Alignment = HorizontalAlignment.Center;
            partsStyle.FillForegroundColor = subColor;
            partsStyle.FillPattern = FillPattern.SolidForeground;
            partsStyle.BorderLeft = BorderStyle.Hair;
            partsStyle.BorderRight = BorderStyle.Hair;
            partsStyle.LeftBorderColor = gridColor;
            partsStyle.RightBorderColor = gridColor;
            partsStyle.WrapText = true;
            dict.Add("partsStyle", partsStyle);

            // textAreaStyle - for merged text areas
            ICellStyle textAreaStyle = workbook.CreateCellStyle();
            textAreaStyle.BorderLeft = BorderStyle.Hair;
            textAreaStyle.BorderBottom = BorderStyle.Hair;
            textAreaStyle.LeftBorderColor = gridColor;
            textAreaStyle.BottomBorderColor = gridColor;
            textAreaStyle.Alignment = HorizontalAlignment.Left;
            textAreaStyle.VerticalAlignment = VerticalAlignment.Top;
            textAreaStyle.WrapText = true;
            dict.Add("textAreaStyle", textAreaStyle);

            // fieldStyle - for entered fields under parts and labor
            ICellStyle fieldStyle = workbook.CreateCellStyle();
            fieldStyle.BorderBottom = BorderStyle.Hair;
            fieldStyle.BorderLeft = BorderStyle.Hair;
            fieldStyle.BorderRight = BorderStyle.Hair;
            fieldStyle.BottomBorderColor = gridColor;
            fieldStyle.LeftBorderColor = gridColor;
            fieldStyle.RightBorderColor = gridColor;
            fieldStyle.Alignment = HorizontalAlignment.Center;
            fieldStyle.VerticalAlignment = VerticalAlignment.Center;
            dict.Add("fieldStyle", fieldStyle);

            return dict;
        }
        ///<summary>
        /// Creates the IFonts to be used and returns them in a Dictionary.
        ///</summary>
        public static Dictionary<string, IFont> CreateFonts(this IWorkbook workbook)
        {
            var dict = new Dictionary<string, IFont>();

            /// Header stuff
            // headerLabelFont
            IFont headerLabelFont = workbook.CreateFont();
            headerLabelFont.IsBold = true;
            headerLabelFont.FontHeightInPoints = 14;
            dict.Add("headerLabelFont", headerLabelFont);

            // headerValueFont
            IFont headerValueFont = workbook.CreateFont();
            headerValueFont.FontHeightInPoints = 14;
            dict.Add("headerValueFont", headerValueFont);

            /// Main discrepancy stuff
            // labelFont - for large main row text
            IFont labelFont = workbook.CreateFont();
            labelFont.FontHeightInPoints = 12;
            dict.Add("labelFont", labelFont);

            // textAreaFont - for text areas
            IFont textAreaFont = workbook.CreateFont();
            textAreaFont.FontHeightInPoints = 11;
            dict.Add("textAreaFont", textAreaFont);

            // techFont - for technician field (bold)
            IFont techFont = workbook.CreateFont();
            techFont.FontHeightInPoints = 11;
            techFont.IsBold = true;
            dict.Add("techFont", techFont);

            // fieldFont - for use in text fields e.g. hours, qty, part number
            IFont fieldFont = workbook.CreateFont();
            fieldFont.FontHeightInPoints = 12;
            dict.Add("fieldFont", fieldFont);

            return dict;
        }
    }
}