﻿using Helper.Application.Abstraction.Commands;
using Helper.Core.Offer.ValueObjects;
using System.Text.Json.Serialization;

namespace Helper.Application.Offer.Commands
{
    public sealed record SpecifyOfferPrice([property:JsonIgnore]OfferId OfferId, double price) : ICommand;
}