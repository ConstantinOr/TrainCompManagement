using MediatR;
using TrainCompManagement.Client.DTO;

namespace TrainCompManagement.Infrastructure.Command;

public record MoveNodeCommand(NodeClientModel Parent, NodeClientModel Node) : IRequest<bool>;
