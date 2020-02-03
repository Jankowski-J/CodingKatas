using System;
using System.Collections.Generic;
using Baryka.MaitreD.BLL.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Baryka.MaitreD.BLL.Tests.Models
{
    [TestFixture]
    public class TableTests
    {
        [TestCaseSource(nameof(NoReservationsTestCases))]
        [Test]
        public void CanAccept_ForNoPriorReservations(int capacity, Reservation reservation, bool expected)
        {
            // Arrange
            var table = new Table(capacity);

            // Act
            var actual = table.CanAccept(reservation);

            // Assert
            actual.Should().Be(expected);
        }

        [TestCaseSource(nameof(ExistingReservationsCases))]
        [Test]
        public void TestBoutiqueRestaurant(int capacity, List<Reservation> existingReservations,
            Reservation reservation, bool expected)
        {
            // Arrange
            var table = new Table(capacity);

            foreach (var existingReservation in existingReservations)
            {
                table.Accept(existingReservation);
            }

            // Act
            var actual = table.CanAccept(reservation);

            // Assert
            actual.Should().Be(expected);
        }

        [TestCaseSource(nameof(ThrowsCases))]
        [Test]
        public void Accept_ForExistingReservationTheSameDay_ThrowInvalidOperationException(int capacity,
            List<Reservation> existingReservations,
            Reservation reservation)
        {
            // Arrange
            var table = new Table(capacity);
            foreach (var existingReservation in existingReservations)
            {
                table.Accept(existingReservation);
            }

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => table.Accept(reservation));
        }

        private static object[] ThrowsCases =
        {
            new object[]
            {
                4, new List<Reservation>
                {
                    new Reservation { Quantity = 2, Date = new DateTime(2023, 9, 14) }
                },
                new Reservation { Quantity = 3, Date = new DateTime(2023, 9, 14) },
            },
            new object[]
            {
                10, new List<Reservation>
                {
                    new Reservation { Quantity = 2, Date = new DateTime(2023, 9, 14) }
                },
                new Reservation { Quantity = 3, Date = new DateTime(2023, 9, 14) }
            },
            new object[]
            {
                4, new List<Reservation>
                {
                    new Reservation { Quantity = 2, Date = new DateTime(2023, 9, 14) }
                },
                new Reservation { Quantity = 3, Date = new DateTime(2023, 9, 14) }
            }
        };

        private static object[] NoReservationsTestCases =
        {
            new object[] { 4, new Reservation { Quantity = 1 }, true },
            new object[] { 4, new Reservation { Quantity = 4 }, true },
            new object[] { 4, new Reservation { Quantity = 5 }, false }
        };

        private static object[] ExistingReservationsCases =
        {
            new object[] { 12, new List<Reservation>(), new Reservation { Quantity = 1 }, true },
            new object[] { 12, new List<Reservation>(), new Reservation { Quantity = 13 }, false },
            new object[] { 12, new List<Reservation>(), new Reservation { Quantity = 12 }, true },
            new object[]
            {
                4, new List<Reservation>
                {
                    new Reservation { Quantity = 2, Date = new DateTime(2023, 9, 14) }
                },
                new Reservation { Quantity = 3, Date = new DateTime(2023, 9, 14) }, false
            },
            new object[]
            {
                10, new List<Reservation>
                {
                    new Reservation { Quantity = 2, Date = new DateTime(2023, 9, 14) }
                },
                new Reservation { Quantity = 3, Date = new DateTime(2023, 9, 14) }, false
            },
            new object[]
            {
                10, new List<Reservation>
                {
                    new Reservation { Quantity = 3, Date = new DateTime(2023, 9, 14) },
                    new Reservation { Quantity = 2, Date = new DateTime(2023, 9, 15) },
                    new Reservation { Quantity = 3, Date = new DateTime(2023, 9, 16) }
                },
                new Reservation { Quantity = 3, Date = new DateTime(2023, 9, 14) }, false
            },
            new object[]
            {
                4, new List<Reservation>
                {
                    new Reservation { Quantity = 2, Date = new DateTime(2023, 9, 15) }
                },
                new Reservation { Quantity = 3, Date = new DateTime(2023, 9, 14) }, true
            }
        };
    }
}