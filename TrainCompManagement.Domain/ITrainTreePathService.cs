using TrainCompManagement.Client.DTO;

namespace TrainCompManagement.Domain;

public interface ITrainTreePathService
{
   IEnumerable<NodeClientModel> GetAllChildren(long nodeId);
   IEnumerable<NodeClientModel> GetAllParents(long nodeId);
   NodeClientModel AddNode(NodeClientModel parent, NodeClientModel node);
   bool UpdateNode(NodeClientModel node);
   bool MoveNode(NodeClientModel parent, NodeClientModel node);
   bool DeleteNode(NodeClientModel node);
}