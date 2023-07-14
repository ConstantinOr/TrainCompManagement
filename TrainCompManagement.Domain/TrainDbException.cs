using Newtonsoft.Json;
using TrainCompManagement.Client.DTO;

namespace TrainCompManagement.Domain;

[Serializable]
public class TrainDbException: Exception
{
    public TrainDbException()
    {
    }

    public TrainDbException(string? message) : base(message)
    {
    }

    public static TrainDbException FireNodeNotSavedException(NodeClientModel parent, NodeClientModel node)
    {
        return new TrainDbException( GetFormattedInformation(parent, node, "Node not saved."));
    }
   
    public static TrainDbException FireNodeNotUpdatedException(NodeClientModel node)
    {
        return new TrainDbException(  $"Node not saved.{Environment.NewLine} Node: {JsonConvert.SerializeObject(node)}");
    }
    
    public static TrainDbException FireNodeNotMovedException(NodeClientModel parent, NodeClientModel node)
    {
        return new TrainDbException( GetFormattedInformation(parent, node, "Node not moved."));
    }
    
    public static TrainDbException FireLoopPresentException(NodeClientModel parent, NodeClientModel node)
    {
        return new TrainDbException( GetFormattedInformation(parent, node, "Loop present"));
    }
    
    private static string GetFormattedInformation(NodeClientModel parent, NodeClientModel node, string description)
    {
        return @$"{description} {Environment.NewLine} Parent:
                  {JsonConvert.SerializeObject(parent)} {Environment.NewLine}
                  Node: {JsonConvert.SerializeObject(node)}";

    } 
}