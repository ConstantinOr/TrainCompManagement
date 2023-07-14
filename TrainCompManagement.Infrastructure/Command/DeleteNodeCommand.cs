using MediatR;
using TrainCompManagement.Client.DTO;

namespace TrainCompManagement.Infrastructure.Command;

public record DeleteNodeCommand(NodeClientModel NodeToDelete):IRequest<bool>;