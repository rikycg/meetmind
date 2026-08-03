using MediatR;
using MeetMind.Application.Meetings.Commands.CancelActionItem;
using MeetMind.Application.Meetings.Commands.CompleteActionItem;
using MeetMind.Application.Meetings.Commands.CreateActionItem;
using MeetMind.Application.Meetings.Commands.DeleteActionItem;
using MeetMind.Application.Meetings.Commands.StartActionItem;
using MeetMind.Application.Meetings.Common;
using MeetMind.Application.Meetings.Queries.GetActionItemsByAssignedTo;
using MeetMind.Application.Meetings.Queries.GetActionItemsBySummaryId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/action-items")]
[Authorize]
public class ActionItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActionItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateActionItemCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? summaryId, [FromQuery] Guid? assignedTo, CancellationToken cancellationToken = default)
    {
        IEnumerable<ActionItemResponse> items;

        if (summaryId is not null)
        {
            items = await _mediator.Send(new GetActionItemsBySummaryIdQuery(summaryId.Value), cancellationToken);
        }
        else if (assignedTo is not null)
        {
            items = await _mediator.Send(new GetActionItemsByAssignedToQuery(assignedTo.Value), cancellationToken);
        }
        else
        {
            return BadRequest("You must provide either 'summaryId' or 'assignedTo' as a query parameter");
        }

        return Ok(items);
    }

    [HttpPatch("{id}/start")]
    public async Task<IActionResult> Start(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new StartActionItemCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new CompleteActionItemCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new CancelActionItemCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteActionItemCommand(id), cancellationToken);
        return NoContent();
    }
}
