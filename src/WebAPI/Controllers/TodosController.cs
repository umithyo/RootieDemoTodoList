using Application.Todos.Commands.CreateTodo;
using Application.Todos.Commands.DeleteTodo;
using Application.Todos.Commands.UpdateTodo;
using Application.Todos.Queries.GetTodoById;
using Application.Todos.Queries.GetTodos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebUI.Controllers
{
    public class TodosController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TodoDto>>> GetTodos([FromQuery] GetTodosQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetById(string id)
        {
            return await Mediator.Send(new GetTodoByIdQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateTodoCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, UpdateTodoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteTodoCommand { Id = id });

            return NoContent();
        }
    }
}
