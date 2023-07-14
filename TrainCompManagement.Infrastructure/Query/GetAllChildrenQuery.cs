using MediatR;
using TrainCompManagement.Client.DTO;

namespace TrainCompManagement.Infrastructure.Query;

public record GetAllChildrenQuery(long NodeId): IRequest<IEnumerable<NodeClientModel>>;