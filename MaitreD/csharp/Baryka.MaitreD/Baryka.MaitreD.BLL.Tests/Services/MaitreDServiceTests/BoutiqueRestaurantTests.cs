using System;
using System.Collections.Generic;
using Baryka.MaitreD.BLL.Models;
using Baryka.MaitreD.BLL.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Baryka.MaitreD.BLL.Tests.Services.MaitreDServiceTests
{
    [TestFixture]
    public class BoutiqueRestaurantTests
    {
        private MaitreDService _sut;

        private static object[] BoutiqueCases =
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
                    new Reservation { Quantity = 3, Date = new DateTime(2023, 9, 14) }
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

        [TestCaseSource(nameof(BoutiqueCases))]
        [Test]
        public void TestBoutiqueRestaurant(int capacity, List<Reservation> existingReservations,
            Reservation reservation, bool expected)
        {
            // Arrange
            _sut = new MaitreDService(capacity);

            foreach (var existingReservation in existingReservations)
            {
                _sut.Accept(existingReservation);
            }

            // Act
            var actual = _sut.CanAccept(reservation);

            // Assert
            actual.Should().Be(expected);
        }
    }
}