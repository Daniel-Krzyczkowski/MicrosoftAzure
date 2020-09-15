using NPOI.SS.UserModel;
using System;

namespace ExcelFileContentExtractor.Infrastructure.Extensions
{
    public static class NpoiExtensions
    {
        public static string GetFormattedCellValue(this ICell cell, IFormulaEvaluator eval = null)
        {
            if (cell != null)
            {
                switch (cell.CellType)
                {
                    case CellType.String:
                        return cell.StringCellValue;

                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            try
                            {
                                return cell.DateCellValue.ToString();
                            }
                            catch (NullReferenceException)
                            {
                                return DateTime.FromOADate(cell.NumericCellValue).ToString();
                            }
                        }
                        return cell.NumericCellValue.ToString();


                    case CellType.Boolean:
                        return cell.BooleanCellValue ? "TRUE" : "FALSE";

                    case CellType.Formula:
                        if (eval != null)
                            return GetFormattedCellValue(eval.EvaluateInCell(cell));
                        else
                            return cell.CellFormula;

                    case CellType.Error:
                        return FormulaError.ForInt(cell.ErrorCellValue).String;
                }
            }
            // null or blank cell, or unknown cell type
            return string.Empty;
        }
    }
}
