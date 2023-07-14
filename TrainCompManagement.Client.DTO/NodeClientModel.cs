namespace TrainCompManagement.Client.DTO;

public record NodeClientModel
(
    long TrainId,
    string Name,
    string UniqueNumber,
    bool IsQuantityAllowed,
    long Quantity,
    NodeClientModel Ancestor,
    NodeClientModel Descendant
);
