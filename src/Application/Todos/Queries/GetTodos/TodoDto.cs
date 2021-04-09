using System;
using AutoMapper;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Todos.Queries.GetTodos
{
    public class TodoDto : IMapFrom<Todo>
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public DateTime? Date { get; set; }
        public string TimeZone { get; set; }
        public int ScheduleType { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Todo, TodoDto>()
                .ForMember(d => d.ScheduleType, opt => opt.MapFrom(s => (int)s.ScheduleType));
        }
    }
}
