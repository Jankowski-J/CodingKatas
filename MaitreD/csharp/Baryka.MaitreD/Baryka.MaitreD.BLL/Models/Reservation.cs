using System;

namespace Baryka.MaitreD.BLL.Models
{
    public class Reservation
    {
        public DateTime Date { get; set; }
        public uint Quantity { get; set; }

        public override string ToString()
        {
            return $"{nameof(Date)}: {Date}, {nameof(Quantity)}: {Quantity}";
        }
    }
}