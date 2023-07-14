using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrainCompManagement.Client.DTO;
using TrainCompManagement.Infrastructure.Command;
using TrainCompManagement.Infrastructure.Query;

namespace TrainComponentManagementSystem.Controllers;

[ApiController]
[Route("train/api/v1")]
public class TrainMManagementController : ControllerBase
{
    private readonly ILogger<TrainMManagementController> _logger;
    private readonly IMediator _mediator; 
    public TrainMManagementController(ILogger<TrainMManagementController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("node/allChildren/{nodeId}")]
    public async Task<IEnumerable<NodeClientModel>> GetAllChildren(long nodeId)
    {
       return await _mediator.Send(new GetAllChildrenQuery(nodeId));
    }
    
    [HttpGet]
    [Route("node/allParent")]
    public async Task<IEnumerable<NodeClientModel>> GetAllParent(long nodeId)
    {
        return await _mediator.Send(new GetAllParentQuery(nodeId));
    }
    
    [HttpPost]
    [Route("node/add")]
    public async Task<NodeClientModel> AddNode([FromBody]NodeClientModel parent, [FromBody]NodeClientModel node)
    {
        return await _mediator.Send(new AddNodeCommand(parent, node));
    }
    
    [HttpPost]
    [Route("node/update")]
    public async Task<bool> UpdateNode([FromBody]NodeClientModel node)
    {
        return await _mediator.Send(new UpdateNodeCommand(node));
    } 
    
    [HttpPost]
    [Route("node/move")]
    public async Task<bool> MoveNode([FromBody]NodeClientModel parent,[FromBody]NodeClientModel node)
    {
        return await _mediator.Send(new MoveNodeCommand(parent, node));
    } 
}