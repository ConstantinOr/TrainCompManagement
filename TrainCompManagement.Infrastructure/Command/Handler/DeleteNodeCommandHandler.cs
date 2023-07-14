using MediatR;
using TrainCompManagement.Client.DTO;
using TrainCompManagement.Domain;

namespace TrainCompManagement.Infrastructure.Command.Handler;

public class DeleteNodeCommandHandler: IRequestHandler<DeleteNodeCommand, bool>
{
    private ITrainTreePathService TrailService { get; }
    public DeleteNodeCommandHandler(ITrainTreePathService trailService)
    {
        TrailService = trailService;
    }

    public Task<bool> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(TrailService.DeleteNode(request.NodeToDelete));
    }
}