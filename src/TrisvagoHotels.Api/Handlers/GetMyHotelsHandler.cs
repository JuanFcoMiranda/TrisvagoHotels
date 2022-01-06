using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrisvagoHotels.Api.Requests;
using TrisvagoHotels.DataContracts.IServices;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Handlers;

public class GetMyHotelsHandler : IRequestHandler<GetMyHotelsRequest, IEnumerable<Hotel>> {
    private readonly IHotelsServices hotelsServices;

    public GetMyHotelsHandler(IHotelsServices hotelsServices) {
        this.hotelsServices = hotelsServices;
    }

    public Task<IEnumerable<Hotel>> Handle(GetMyHotelsRequest request, CancellationToken cancellationToken) => hotelsServices.GetAllHotels();
}