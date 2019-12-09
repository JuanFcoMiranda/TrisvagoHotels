using System.Collections.Generic;
using MediatR;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Requests {
    public class GetMyHotelsRequest : IRequest<IEnumerable<Hotel>> {
    }
}