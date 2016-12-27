using System;

namespace Tam.Core.MediatR
{
    public class FailureResult : ICommandResult
    {
        public bool IsSuccess
        {
            get
            {
                return false;
            }
        }

        public object Result
        {
            get
            {
                return null;
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
