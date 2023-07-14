using MediatR;
using TrainCompManagement.Client.DTO;

namespace TrainCompManagement.Infrastructure.Command;

public record UpdateNodeCommand(NodeClientModel NewNode): IRequest<bool>;