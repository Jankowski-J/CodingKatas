using System;
using System.Collections.Generic;
using System.Linq;

namespace Baryka.MaitreD.BLL.Models
{
    public class Table
    {
        private readonly int _capacity;
        private readonly IList<Reservation> _reservations;
        
        public Table(int capacity)
        {
            _capacity = capacity;
            _reservations = new List<Reservation>();
        }

        public bool CanAccept(Reservation reservation)
        {
            var reservationForSameDate = _reservations.FirstOrDefault(x => x.Date == reservation.Date);

            if (reservationForSameDate != null)
            {
                return false;
            }

            return reservation.Quantity <= _capacity;
        }

        public void Accept(Reservation reservation)
        {
            if (!CanAccept(reservation)) throw new InvalidOperationException();

            _reservations.Add(reservation);
        }
    }
}