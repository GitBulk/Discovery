﻿namespace ContosoUniversityCore.Features.Student
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain;
    using Infrastructure;
    using MediatR;

    public class Details
    {
        public class Query : IRequest<Model>
        {
            public int Id { get; set; }
        }

        public class Model
        {
            public int ID { get; set; }
            [Display(Name = "First Name")]
            public string FirstMidName { get; set; }
            public string LastName { get; set; }
            public DateTime EnrollmentDate { get; set; }
            public List<Enrollment> Enrollments { get; set; } 

            public class Enrollment
            {
                public string CourseTitle { get; set; }
                public Grade? Grade { get; set; }
            }
        }

        public class Handler : IAsyncRequestHandler<Query, Model>
        {
            private readonly SchoolContext _db;

            public Handler(SchoolContext db)
            {
                _db = db;
            }

            public async Task<Model> Handle(Query message)
            {
                return await _db.Students.Where(s => s.Id == message.Id).ProjectToSingleOrDefaultAsync<Model>();
            }
        }
    }
}