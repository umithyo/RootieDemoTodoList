using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Queries.GetTodos
{
    public class GetTodosQuery : IRequest<List<TodoDto>>
    {
        public string Query { get; set; }
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, List<TodoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Todos
                .Where(x => request.Query == null ||
                    (
                        x.Content.Contains(request.Query) ||
                        x.Email.Contains(request.Query)
                    )
                )
                .OrderBy(x => x.Date)
                .ProjectTo<TodoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
