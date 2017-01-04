using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.MediatR
{
    public interface ICommandResult
    {
        bool IsSuccess { get; }
        object Result { get; set; }
    }
}
