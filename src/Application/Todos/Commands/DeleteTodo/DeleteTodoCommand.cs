using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Todos.Commands.DeleteTodo
{
    public class DeleteTodoCommand : IRequest
    {
        public string Id { get; set; }
    }

    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Todos.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Todo), request.Id);
            }

            _context.Todos.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
