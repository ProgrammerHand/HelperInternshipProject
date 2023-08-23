using Helper.Application.Abstraction.Commands;
using Helper.Application.Exceptions;
using Helper.Application.Security;
using Helper.Application.User.Commands;
using Helper.Core.User.Value_objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class AcceptOfferHandler : ICommandHandler<AcceptOffer>
    {
        public async Task HandleAsync(AcceptOffer command)
        {
        }
    }
}
