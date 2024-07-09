using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CrudBlue.Application.Queries.GetAll;
using CrudBlue.Application.Queries.GetById;
using CrudBlue.Application.Commands.Create;
using CrudBlue.Application.Commands.Update;
using CrudBlue.Application.Commands.Delete;
using CrudBlue.Application.Commands.Inactivate;
using CrudBlue.Application.Models.InputModels;

namespace CrudBlue.API.Controllers;

[ApiController]
[Route("api/agenda")]
public class ContactController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<ContactViewModel>>>> GetAll()
    {
        var query = new GetAllContactsQuery();
        var response = await _mediator.Send(query);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<ContactViewModel>>> GetById(int id)
    {
        var query = new GetContactByIdQuery(id);
        var response = await _mediator.Send(query);
        if (response.Success)
        {
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<ContactViewModel>>> Create([FromBody] ContactInputModel inputModel)
    {
        var command = new CreateContactCommand(inputModel);
        var response = await _mediator.Send(command);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResponse<ContactViewModel>>> Update(int id, [FromBody] ContactInputModel contactInput)
    {
        var command = new UpdateContactCommand(contactInput, id);
        var response = await _mediator.Send(command);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<ContactViewModel>>> Delete(int id)
    {
        var command = new DeleteContactCommand(id);
        var response = await _mediator.Send(command);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("inactivate/{id}")]
    public async Task<ActionResult<ServiceResponse<ContactViewModel>>> Inactivate(int id)
    {
        var command = new InactivateContactCommand(id);
        var response = await _mediator.Send(command);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}
