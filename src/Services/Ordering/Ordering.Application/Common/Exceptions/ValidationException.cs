using FluentValidation.Results;

namespace Ordering.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
            Failures = failures;
        }

        public IDictionary<string, string[]> Errors { get; }

        public IEnumerable<ValidationFailure> Failures { get; }
    }
}
