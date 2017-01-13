﻿namespace ContosoUniversityCore.Features.Course
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentValidation;
    using Infrastructure;
    using MediatR;
    using AutoMapper;

    public class Delete
    {
        public class Query : IRequest<Command>
        {
            public int? Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(m => m.Id).NotNull();
            }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, Command>
        {
            private readonly SchoolContext _db;

            public QueryHandler(SchoolContext db)
            {
                _db = db;
            }

            public Task<Command> Handle(Query message)
            {
                return _db.Courses.Where(c => c.Id == message.Id).ProjectToSingleOrDefaultAsync<Command>();
            }
        }

        public class Command : IRequest
        {
            [Display(Name = "Number")]
            public int Id { get; set; }
            public string Title { get; set; }
            public int Credits { get; set; }
            public string DepartmentName { get; set; }
        }

        public class CommandHandler : IAsyncRequestHandler<Command>
        {
            private readonly SchoolContext _db;

            public CommandHandler(SchoolContext db)
            {
                _db = db;
            }

            public async Task Handle(Command message)
            {
                var course = await _db.Courses.FindAsync(message.Id);

                _db.Courses.Remove(course);
            }
        }
    }
}