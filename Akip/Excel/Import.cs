using System;
using System.Collections.ObjectModel;
using System.Windows;
using IExcel = Microsoft.Office.Interop.Excel;

namespace Akip.Excel
{
    public class Import
    {
        public static void ImportFromExcel(string fileName, ObservableCollection<ProgramViewModel> collection)
        {
            IExcel.Application ExcelExample;
            IExcel.Workbook objWorkbook;
            IExcel.Worksheet objWorksheet;
            IExcel.Range range;

            int RowCount,
                ColumnCount;

            try
            {
                ExcelExample = new IExcel.Application {
                    Visible = false
                };
                objWorkbook = ExcelExample.Workbooks.Open(fileName);
                objWorksheet = objWorkbook.Sheets[1];
                range = objWorksheet.UsedRange;

                if(objWorksheet != null)
                {
                    RowCount = range.Rows.Count;
                    ColumnCount = range.Columns.Count;

                    for(int i = 1; i <= RowCount; i++)
                    {
                        collection.Add(new ProgramViewModel()
                        {
                            NameMode = range.Cells[i, 1].Value2.ToString(),
                            NameType = range.Cells[i, 2].Value2.ToString(),
                            AmperageValue = range.Cells[i, 3].Value2.ToString(),
                            TimeValue = range.Cells[i, 4].Value2.ToString()
                        });
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Import excel file error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
