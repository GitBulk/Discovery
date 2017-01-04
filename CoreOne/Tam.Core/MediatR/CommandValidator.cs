using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.MediatR
{
    public abstract class CommandValidator<T> where T: ICommandResultAsyncRequest
    {
        public Action<AddSampleProductCommand>[] Validators { get; set; }
    }
}
