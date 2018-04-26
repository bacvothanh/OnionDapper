using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Infrastructure.Extensions;

namespace Dapper.Infrastructure
{
    public class ApplicationResult
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public List<string> Errors { get; private set; }

        protected ApplicationResult(bool isSuccess, List<string> errors)
        {
            if (isSuccess && errors.Count > 0)
                throw new InvalidOperationException();

            if (!isSuccess && errors.Count == 0)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public void AddError(string error)
        {
            Errors.Add(error);
            IsSuccess = false;
        }

        public void AddError(ApplicationErrorCode errorCode, params object[] args)
        {
            Errors.Add(string.Format(errorCode.GetDescription(), args));
            IsSuccess = false;
        }

        public void AddError(List<string> errors)
        {
            this.Errors.AddRange(errors);
            IsSuccess = false;
        }

        public static ApplicationResult Fail(string message)
        {
            return new ApplicationResult(false, new List<string> { message });
        }

        public static ApplicationResult Fail(List<string> messages)
        {
            return new ApplicationResult(false, messages);
        }

        public static ApplicationResult Fail(ApplicationErrorCode errorCode, params object[] args)
        {
            return new ApplicationResult(false, new List<string> { string.Format(errorCode.GetDescription(), args) });
        }
        public static ApplicationResult<T> Fail<T>(ApplicationErrorCode errorCode, params object[] args)
        {
            return new ApplicationResult<T>(default(T), false, new List<string> { string.Format(errorCode.GetDescription(), args) });
        }

        public static ApplicationResult<T> Fail<T>(List<string> messages)
        {
            return new ApplicationResult<T>(default(T), false, messages);
        }
        public static ApplicationResult<T> Fail<T>(string message)
        {
            return new ApplicationResult<T>(default(T), false, new List<string>() { message });
        }

        public static ApplicationResult Ok()
        {
            return new ApplicationResult(true, new List<string>());
        }

        public static ApplicationResult<T> Ok<T>()
        {
            return new ApplicationResult<T>(default(T), true, new List<string>());
        }

        public static ApplicationResult<T> Ok<T>(T value)
        {
            return new ApplicationResult<T>(value, true, new List<string>());
        }

        public static ApplicationResult<T> Convert<T>(ApplicationResult source)
        {
            return new ApplicationResult<T>(default(T), source.IsSuccess, source.Errors);
        }
    }

    public class ApplicationResult<T> : ApplicationResult
    {
        public T Value { get; }

        protected internal ApplicationResult(T value, bool isSuccess, List<string> errors) : base(isSuccess, errors)
        {
            Value = value;
        }

    }
}
