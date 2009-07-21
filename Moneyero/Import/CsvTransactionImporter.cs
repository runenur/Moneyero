using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Moneyero.Models;

namespace Moneyero.Import
{
    /// <summary>
    /// Represents a transaction importer that can import transactions from a comma-separated values source.
    /// </summary>
    public class CsvTransactionImporter : ITransactionImporter
    {
        /// <summary>
        /// Creates a new <see cref="CsvTransactionImporter"/> instance.
        /// </summary>
        public CsvTransactionImporter()
        {
            AmountDecimalSeparator = '.';
            DateFormat = "yyyy-MM-dd";
            ColumnSeparator = ',';
        }

        /// <summary>
        /// Gets or sets the character to use as the decimal separator in the amount value.
        /// </summary>
        /// <remarks>
        /// The default value is '.' (a dot).
        /// </remarks>
        public char AmountDecimalSeparator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column that contains the date.
        /// </summary>
        public int DateColumn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the format of the date.
        /// </summary>
        /// <remarks>
        /// The default value is 'yyyy-MM-dd'.
        /// </remarks>
        public string DateFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column that contains the description.
        /// </summary>
        public int DescriptionColumn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column that contains the incoming amount.
        /// </summary>
        public int IncomingAmountColumn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column that contains the outgoing amount.
        /// </summary>
        /// <remarks>
        /// If <see cref="OutgoingAmountColumn"/> is different from <see cref="IncomingAmountColumn"/>,
        /// a positive value in the specified column represents an expense.
        /// </remarks>
        public int OutgoingAmountColumn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the character to use when separating the columns in the input file.
        /// </summary>
        public char ColumnSeparator
        {
            get;
            set;
        }

        /// <summary>
        /// Imports a collection of transactions from a comma-separated values (CSV) source.
        /// </summary>
        /// <param name="reader">a <see cref="TextReader"/> that represents the CSV source.</param>
        /// <returns>A collection of transactions.</returns>
        public ICollection<Transaction> Import(TextReader reader)
        {
            var transactions = new List<Transaction>();
            {
                var lines = new List<string>();

                while (true)
                {
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        lines.Add(line);
                    }
                    else
                    {
                        break;
                    }
                }

                foreach (string line in lines)
                {
                    string[] cells = line.Split(ColumnSeparator);
                    transactions.Add(new Transaction
                    {
                        Amount = GetAmount(cells),
                        Date = GetDate(cells),
                        Description = GetDescription(cells)
                    });
                }
            }
            return transactions;
        }

        /// <summary>
        /// Gets the amount.
        /// </summary>
        /// <param name="cells">the cells in a row.</param>
        /// <returns>The amount.</returns>
        private double GetAmount(string[] cells)
        {
            double amount = 0;

            var nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = AmountDecimalSeparator.ToString()
            };

            bool wasParsed;
            if (IncomingAmountColumn != OutgoingAmountColumn)
            {
                // Incoming
                double incomingAmount;
                wasParsed = double.TryParse(
                    GetCellValue(cells, IncomingAmountColumn),
                    NumberStyles.Any,
                    nfi,
                    out incomingAmount);
                if (wasParsed)
                {
                    amount += incomingAmount;
                }

                // Outgoing
                double outgoingAmount;
                wasParsed = double.TryParse(
                    GetCellValue(cells, OutgoingAmountColumn),
                    NumberStyles.Any,
                    nfi,
                    out outgoingAmount);
                if (wasParsed)
                {
                    amount -= outgoingAmount;
                }
            }
            else
            {
                // Incoming & outgoing
                double incomingAndOutgoingAmount;
                wasParsed = double.TryParse(
                    GetCellValue(cells, IncomingAmountColumn),
                    NumberStyles.Any,
                    nfi,
                    out incomingAndOutgoingAmount);
                if (wasParsed)
                {
                    amount = incomingAndOutgoingAmount;
                }
            }

            return amount;
        }

        /// <summary>
        /// Gets the value at the specified column.
        /// </summary>
        /// <param name="cells">the cells in a row.</param>
        /// <param name="column">the column that contains the value.</param>
        /// <returns>The value at the specified column.</returns>
        private static string GetCellValue(string[] cells, int column)
        {
            if (column < 0 || column >= cells.Length)
            {
                throw new ArgumentException("The column index was larger than the number of columns", "column");
            }
            return cells[column];
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="cells">the cells in a row.</param>
        /// <returns>The date.</returns>
        private DateTime GetDate(string[] cells)
        {
            DateTime date;
            bool wasParsed = DateTime.TryParseExact(
                GetCellValue(cells, DateColumn),
                DateFormat,
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out date);
            return wasParsed ? date : default(DateTime);
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="cells">the cells in a row.</param>
        /// <returns>The description.</returns>
        private string GetDescription(string[] cells)
        {
            return GetCellValue(cells, DescriptionColumn);
        }
    }
}