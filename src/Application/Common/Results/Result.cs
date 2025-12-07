using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Results
{
    public class Result<TError, TValue>
    {
        public bool IsSuccess { get; }
        public TError? Error { get; }
        public TValue? Value { get; }

        private Result(bool isSuccess, TError? error, TValue? value)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public static Result<TError, TValue> Success(TValue value)
            => new(true, default, value);

        public static Result<TError, TValue> Fail(TError error)
            => new(false, error, default);
    }
}
