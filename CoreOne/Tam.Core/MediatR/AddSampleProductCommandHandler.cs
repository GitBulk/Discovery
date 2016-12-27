using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.MediatR
{
    public class AddSampleProductCommandHandler : ICommandResultAsyncRequestHandler<AddSampleProductCommand>
    {
        private readonly DbContext context;

        public AddSampleProductCommandHandler(DbContext context)
        {
            this.context = context;
        }

        public Task<ICommandResult> Handle(AddSampleProductCommand message)
        {
            if (message.ModelState.IsValid == false)
            {
                return Task.FromResult<ICommandResult>(new FailureResult());
            }

            // save to database
            //return Task.FromResult(new SuccessResult(message.Product.Id));
            return Task.FromResult<ICommandResult>(new SuccessResult(message.Product.Id));
        }
    }
}
