using MediatR;
using TicketBooking.Core.EventAggregate;

namespace TicketBooking.UseCases.Events.CreateSeatLayout
{
    public class CreateSeatLayoutCommand : IRequest<List<Seat>>
    {
        public int EventId { get; set; }

        public List<SeatDto> Seats { get; set; }
    }

    public class SeatDto
    {
        public string SectorCode { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }

        public int CategoryId { get; set; }
        public SeatState? State { get; set; }
    }
}
