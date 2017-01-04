using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.MediatR
{
    public class SuccessResult : ICommandResult
    {
        private int id;

        public SuccessResult(object result)
        {
            this.Result = result;
        }

        public bool IsSuccess
        {
            get
            {
                return true;
            }
        }

        public object Result
        {
            get;
            set;
        }
    }
}
