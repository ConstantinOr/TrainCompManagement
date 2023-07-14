using MediatR;
using TrainCompManagement.Client.DTO;
using TrainCompManagement.Domain;
using TrainCompManagement.Infrastructure.Command;

namespace TrainCompManagement.Infrastructure.Query.Handler;

public class GetAllChildrenQueryHandler: IRequestHandler<GetAllChildrenQuery, IEnumerable<NodeClientModel>>
{
    private ITrainTreePathService TrailService { get; }
    public GetAllChildrenQueryHandler(ITrainTreePathService trailService)
    {
        TrailService = trailService;
    }
    public Task<IEnumerable<NodeClientModel>> Handle(GetAllChildrenQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(TrailService.GetAllChildren(request.NodeId));
    }
}