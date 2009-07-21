using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moneyero.Import;
using Moneyero.Models;
using NUnit.Framework;

namespace Moneyero.Tests.Import
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    public class CsvTransactionImporterTests
    {
        [Test]
        public void Import_DateColumnLargerThanNumberOfColumns_ThrowsArgumentException()
        {
            const string input = "2000-01-01";
            var importer = new CsvTransactionImporter
            {
                DateColumn = 1,
                DateFormat = "yyyy-MM-dd"
            };
            Assert.Throws<ArgumentException>(
                () => importer.Import(new StringReader(input)));
        }

        [Test]
        public void Import_DateColumnLessThanZero_ThrowsArgumentException()
        {
            const string input = "2000-01-01";
            var importer = new CsvTransactionImporter
            {
                DateColumn = -1,
                DateFormat = "yyyy-MM-dd"
            };
            Assert.Throws<ArgumentException>(
                () => importer.Import(new StringReader(input)));
        }

        [Test]
        public void Import_DescriptionColumnLargerThanNumberOfColumns_ThrowsArgumentException()
        {
            const string input = "ABC";
            var importer = new CsvTransactionImporter
            {
                DescriptionColumn = 1
            };
            Assert.Throws<ArgumentException>(
                () => importer.Import(new StringReader(input)));
        }

        [Test]
        public void Import_DescriptionColumnLessThanZero_ThrowsArgumentException()
        {
            const string input = "ABC";
            var importer = new CsvTransactionImporter
            {
                DescriptionColumn = -1
            };
            Assert.Throws<ArgumentException>(
                () => importer.Import(new StringReader(input)));
        }

        [Test]
        public void Import_IncomingAmountColumnLargerThanNumberOfColumns_ThrowsArgumentException()
        {
            const string input = "12.34";
            var importer = new CsvTransactionImporter
            {
                IncomingAmountColumn = 1
            };
            Assert.Throws<ArgumentException>(
                () => importer.Import(new StringReader(input)));
        }

        [Test]
        public void Import_IncomingAmountColumnLessThanZero_ThrowsArgumentException()
        {
            const string input = "12.34";
            var importer = new CsvTransactionImporter
            {
                IncomingAmountColumn = -1
            };
            Assert.Throws<ArgumentException>(
                () => importer.Import(new StringReader(input)));
        }

        [Test]
        public void Import_InputIncludesDate_SetsDateCorrectly()
        {
            const string input = "2000-01-01";

            var importer = new CsvTransactionImporter
            {
                DateColumn = 0,
                DateFormat = "yyyy-MM-dd"
            };

            ICollection<Transaction> transactions = importer.Import(new StringReader(input));

            Assert.AreEqual(new DateTime(2000, 1, 1), transactions.Single().Date);
        }

        [Test]
        public void Import_InputIncludesDescription_SetsDescriptionCorrectly()
        {
            const string input = "ABC";

            var importer = new CsvTransactionImporter
            {
                DescriptionColumn = 0
            };

            ICollection<Transaction> transactions = importer.Import(new StringReader(input));

            Assert.AreEqual("ABC", transactions.Single().Description);
        }

        [Test]
        public void Import_InputIncludesIncomingAmount_SetsAmountCorrectly()
        {
            const string input = "12.34";

            var importer = new CsvTransactionImporter
            {
                IncomingAmountColumn = 0,
                AmountDecimalSeparator = '.'
            };

            ICollection<Transaction> transactions = importer.Import(new StringReader(input));

            Assert.AreEqual(12.34, transactions.Single().Amount);
        }

        [Test]
        public void Import_InputIncludesOutgoingAmount_SetsAmountCorrectly1()
        {
            const string input = "-12.34";

            var importer = new CsvTransactionImporter
            {
                IncomingAmountColumn = 0,
                OutgoingAmountColumn = 0,
                AmountDecimalSeparator = '.'
            };

            ICollection<Transaction> transactions = importer.Import(new StringReader(input));

            Assert.AreEqual(-12.34, transactions.Single().Amount);
        }

        [Test]
        public void Import_InputIncludesOutgoingAmount_SetsAmountCorrectly2()
        {
            const string input = "0;12.34";

            var importer = new CsvTransactionImporter
            {
                IncomingAmountColumn = 0,
                OutgoingAmountColumn = 1,
                AmountDecimalSeparator = '.',
                ColumnSeparator = ';'
            };

            ICollection<Transaction> transactions = importer.Import(new StringReader(input));

            Assert.AreEqual(-12.34, transactions.Single().Amount);
        }

        [Test]
        public void Import_OutgoingAmountColumnLargerThanNumberOfColumns_ThrowsArgumentException()
        {
            const string input = "12.34";
            var importer = new CsvTransactionImporter
            {
                OutgoingAmountColumn = 1
            };
            Assert.Throws<ArgumentException>(
                () => importer.Import(new StringReader(input)));
        }

        [Test]
        public void Import_OutgoingAmountColumnLessThanZero_ThrowsArgumentException()
        {
            const string input = "12.34";
            var importer = new CsvTransactionImporter
            {
                OutgoingAmountColumn = -1
            };
            Assert.Throws<ArgumentException>(
                () => importer.Import(new StringReader(input)));
        }
    }

    // ReSharper restore InconsistentNaming
}