using Ardalis.Specification;
using TicketBooking.Core.CartAggregate;

namespace Clean.Architecture.Core.ContributorAggregate.Specifications;

public class CartByIdSpec : Specification<Cart>
{
  public CartByIdSpec(int contributorId)
  {
    Query
        .Where(contributor => contributor.Id == contributorId);
  }
}
