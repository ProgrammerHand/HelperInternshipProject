using Helper.Application.Abstraction.Commands;
using System.Text.Json.Serialization;

namespace Helper.Application.Offer.Commands
{
    public sealed record SpecifyOfferPrice([property:JsonIgnore] Guid OfferId, double price) : ICommand;
}