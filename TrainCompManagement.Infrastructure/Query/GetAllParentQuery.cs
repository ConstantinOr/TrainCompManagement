using MediatR;
using TrainCompManagement.Client.DTO;

namespace TrainCompManagement.Infrastructure.Query;

public record GetAllParentQuery(long NodeId): IRequest<IEnumerable<NodeClientModel>>;