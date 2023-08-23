﻿using Helper.Application.Abstraction.Commands;
using Helper.Core.Offer.ValueObjects;

namespace Helper.Application.Offer.Commands
{
    public sealed record RejectOffer(OfferId OfferId, string ClientsReason) : ICommand;
}