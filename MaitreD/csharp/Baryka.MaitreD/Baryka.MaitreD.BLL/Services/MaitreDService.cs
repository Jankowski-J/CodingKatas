using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Baryka.MaitreD.BLL.Models;

namespace Baryka.MaitreD.BLL.Services
{
    public class MaitreDService : IMaitreDService
    {
        private readonly int _capacity;
        private readonly IList<Reservation> _reservations;

        public MaitreDService(int capacity)
        {
            _capacity = capacity;
            _reservations = new List<Reservation>();
        }
        
        public bool CanAccept(Reservation reservation)
        {
            var reservedSeatsForSameDay = _reservations
                .Where(x => x.Date == reservation.Date)
                .Sum(x => x.Quantity);

            return reservedSeatsForSameDay + reservation.Quantity <= _capacity;
        }

        public void Accept(Reservation reservation)
        {
            _reservations.Add(reservation);
        }
    }
}