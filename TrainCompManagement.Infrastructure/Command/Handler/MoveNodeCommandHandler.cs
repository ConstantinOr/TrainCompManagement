using MediatR;
using TrainCompManagement.Client.DTO;
using TrainCompManagement.Domain;

namespace TrainCompManagement.Infrastructure.Command.Handler;

public class MoveNodeCommandHandler: IRequestHandler<MoveNodeCommand, bool>
{
    private ITrainTreePathService TrailService { get; }
    public MoveNodeCommandHandler(ITrainTreePathService trailService)
    {
        TrailService = trailService;
    }

    public Task<bool> Handle(MoveNodeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(TrailService.MoveNode(request.Parent, request.Node));
    }
}