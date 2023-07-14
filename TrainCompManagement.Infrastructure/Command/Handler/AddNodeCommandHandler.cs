using MediatR;
using TrainCompManagement.Client.DTO;
using TrainCompManagement.Domain;

namespace TrainCompManagement.Infrastructure.Command.Handler;

public class AddNodeCommandHandler: IRequestHandler<AddNodeCommand, NodeClientModel>
{
    private ITrainTreePathService TrailService { get; }
    public AddNodeCommandHandler(ITrainTreePathService trailService)
    {
        TrailService = trailService;
    }

    public Task<NodeClientModel> Handle(AddNodeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(TrailService.AddNode(request.Parent, request.Node));
    }
}