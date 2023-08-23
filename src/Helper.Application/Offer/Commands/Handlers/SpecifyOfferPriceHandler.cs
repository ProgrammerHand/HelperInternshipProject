using Helper.Application.Abstraction.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class SpecifyOfferPriceHandler : ICommandHandler<SpecifyOfferPrice>
    {
        public async Task HandleAsync(SpecifyOfferPrice command)
        {
        }
    }
}
