using Helper.Application.Abstraction.Commands;
using Helper.Core.Offer.ValueObjects;
using System.Text.Json.Serialization;

namespace Helper.Application.Offer.Commands
{
    public sealed record RejectOffer(Guid OfferId, string ClientsReason) : ICommand;
}