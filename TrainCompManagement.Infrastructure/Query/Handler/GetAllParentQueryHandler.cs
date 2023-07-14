using MediatR;
using TrainCompManagement.Client.DTO;
using TrainCompManagement.Domain;

namespace TrainCompManagement.Infrastructure.Query.Handler;

public class GetAllParentQueryHandler: IRequestHandler<GetAllChildrenQuery, IEnumerable<NodeClientModel>>
{
    private ITrainTreePathService TrailService { get; }
    public GetAllParentQueryHandler(ITrainTreePathService trailService)
    {
        TrailService = trailService;
    }

    public Task<IEnumerable<NodeClientModel>> Handle(GetAllChildrenQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(TrailService.GetAllParents(request.NodeId));
    }
}