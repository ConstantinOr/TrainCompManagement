using Mapster;
using TrainCompManagement.Client.DTO;
using TrainCompManagement.DAL;
using TrainCompManagement.DAL.Entities;

namespace TrainCompManagement.Domain;

public class TrainTreePathService: ITrainTreePathService
{
    private TrainCompManagementDbContext _dbContext;

    public TrainTreePathService(TrainCompManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<NodeClientModel> GetAllChildren(long nodeId)
    {
        List<NodeClientModel> result;
        using (_dbContext)
        {
           result =
                (from nod in _dbContext.TrainInformation
                    join path in _dbContext.TrainTreePath on nod.TrainId equals path.Ancestor.TrainId
                    where path.Ancestor.TrainId == nodeId
                    select nod.Adapt<NodeClientModel>()).ToList();
           
        }
        return result;
    }

    public IEnumerable<NodeClientModel> GetAllParents(long nodeId)
    {
        List<NodeClientModel> result;
        using (_dbContext)
        {
            result =
                (from nod in _dbContext.TrainInformation
                    join path in _dbContext.TrainTreePath on nod.TrainId equals path.Descendant.TrainId
                    where path.Ancestor.TrainId == nodeId && path.Descendant.TrainId != path.Ancestor.TrainId
                    select nod.Adapt<NodeClientModel>()).ToList();
        }
        return result;
    }

    public NodeClientModel AddNode(NodeClientModel parent, NodeClientModel node)
    {
        if (IsLoopPresent(parent, node))
        {
            throw TrainDbException.FireLoopPresentException(parent, node);
        }
        
        TrainInformation? trainInformation;
        using (_dbContext)
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                trainInformation = node.Adapt<TrainInformation>();
                _dbContext.TrainInformation.Add(trainInformation);
                _dbContext.TrainTreePath.Add
                (
                    new TrainTreePath(){AncestorId = parent.TrainId, DescendantId = trainInformation.TrainId}
                );
                if (_dbContext.SaveChanges() == 0)
                {
                    dbContextTransaction.Rollback();
                    throw TrainDbException.FireNodeNotSavedException(parent, node);
                } 
                dbContextTransaction.Commit();
            }
        }

        return trainInformation.Adapt<NodeClientModel>();
    }

    public bool UpdateNode(NodeClientModel node)
    {
        using (_dbContext)
        {
           var nodeToEdit = _dbContext.TrainInformation.FirstOrDefault(f => f.TrainId == node.TrainId);
           if (nodeToEdit == null)
           {
               throw new TrainDbException($"Node with id={node.TrainId} not found"); 
           }
           nodeToEdit.Name = node.Name;
           nodeToEdit.IsQuantityAllowed = node.IsQuantityAllowed;
           nodeToEdit.Quantity =  node.IsQuantityAllowed ? node.Quantity : 0;

           return _dbContext.SaveChanges() > 0 ? true : throw TrainDbException.FireNodeNotUpdatedException(node);
        }
    }

    public bool MoveNode(NodeClientModel parent, NodeClientModel node)
    {
        if (IsLoopPresent(parent, node))
        {
            throw TrainDbException.FireLoopPresentException(parent, node);
        }
        
        using (_dbContext)
        {
           var  pathToDelete = _dbContext.TrainTreePath.Where(w => w.DescendantId == node.TrainId).ToList();
           _dbContext.TrainTreePath.RemoveRange(pathToDelete);
           _dbContext.TrainTreePath.Add
           (
               new TrainTreePath() { AncestorId = parent.TrainId, DescendantId = node.TrainId }
           );

           return _dbContext.SaveChanges() > 0 ? true : throw TrainDbException.FireNodeNotMovedException(parent, node);
        }
    }

    public bool DeleteNode(NodeClientModel node)
    {
        using (_dbContext)
        {
           var pathToDelete = _dbContext.TrainTreePath.Where(w => w.AncestorId == node.TrainId).ToList();
           _dbContext.TrainTreePath.RemoveRange(pathToDelete);
           _dbContext.TrainInformation
                .Remove(_dbContext.TrainInformation.FirstOrDefault(r => r.TrainId == node.TrainId));

           return  _dbContext.SaveChanges()>0;
        }
    }
    
    private bool IsLoopPresent(NodeClientModel parent, NodeClientModel node)
    { 
        var parentNodeId = new List<long> { parent.TrainId };
        using (_dbContext)
        {
               var  topParent = _dbContext.TrainTreePath
                .Where(w => w.DescendantId == parent.TrainId)
                .Select(w => w.AncestorId??0)
                .ToArray();
               
               parentNodeId.AddRange(topParent);

               return parentNodeId.Contains(node.TrainId);
        }
    }
}