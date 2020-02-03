using Baryka.MaitreD.BLL.Models;

namespace Baryka.MaitreD.BLL.Services
{
    public interface IMaitreDService
    {
        bool CanAccept(Reservation reservation);
    }
}