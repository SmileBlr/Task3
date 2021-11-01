using System;
using System.Collections.Generic;
using System.Data;
using DataTablePrettyPrinter;

namespace Task3
{
    static class HelpMenu
    {
        public static void BuildTable()
        {
            DataTable table = new DataTable("Rules");
            var moves = Rules.GetAvailableMovesList();

            AddColums();
            AddRows();

            Console.WriteLine(table.ToPrettyPrintedString());

            #region Work with rows and colums

            void AddColums()
            {
                table.Columns.Add("Player/Computer");

                foreach (var move in moves)
                {
                    table.Columns.Add(move);
                }
            }
            void AddRows()
            {
                foreach (var move in moves)
                {
                    AddRow(move);
                }
            }
            void AddRow(string baseMove)
            {
                var rowParams = new List<string>();

                rowParams.Add(baseMove);

                foreach (var move in moves)
                {
                    rowParams.Add(Rules.CheckGameResult(baseMove, move).ToString());
                }

                table.Rows.Add(rowParams.ToArray());
            }

            #endregion
        }
    }
}
