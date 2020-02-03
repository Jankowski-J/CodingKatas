using System;
using System.Collections.Generic;
using System.Linq;
using Baryka.MaitreD.BLL.Models;

namespace Baryka.MaitreD.BLL.Services
{
    public class MaitreDService : IMaitreDService
    {
        private readonly IList<Table> _tables;

        public MaitreDService(params int[] capacities)
        {
            _tables = capacities
                .OrderBy(x => x)
                .Select(x => new Table(x)).ToList();
        }
        
        public bool CanAccept(Reservation reservation)
        {
            var canReservationBeAccepted = _tables.Any(x => x.CanAccept(reservation));

            return canReservationBeAccepted;
        }

        public void Accept(Reservation reservation)
        {
            if(!CanAccept(reservation)) throw new InvalidOperationException();

            var eligibleTable = _tables.First(x => x.CanAccept(reservation));
            eligibleTable.Accept(reservation);
        }
    }
}