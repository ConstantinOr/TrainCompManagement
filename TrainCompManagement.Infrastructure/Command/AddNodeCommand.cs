using MediatR;
using TrainCompManagement.Client.DTO;

namespace TrainCompManagement.Infrastructure.Command;

public record AddNodeCommand(NodeClientModel Parent, NodeClientModel Node):IRequest<NodeClientModel>;