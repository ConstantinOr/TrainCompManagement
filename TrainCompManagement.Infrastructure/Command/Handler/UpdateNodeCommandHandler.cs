using MediatR;
using TrainCompManagement.Domain;

namespace TrainCompManagement.Infrastructure.Command.Handler;

public class UpdateNodeCommandHandler: IRequestHandler<UpdateNodeCommand, bool>
{
    private ITrainTreePathService TrailService { get; }
    public UpdateNodeCommandHandler(ITrainTreePathService trailService)
    {
        TrailService = trailService;
    }

    public Task<bool> Handle(UpdateNodeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(TrailService.UpdateNode(request.NewNode));
    }
}