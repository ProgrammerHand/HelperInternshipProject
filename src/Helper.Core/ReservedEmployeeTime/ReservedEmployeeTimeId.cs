using Helper.Core.Exceptions;

namespace Helper.Core.ReservedEmployeeTime
{
    public sealed record ReservedEmployeeTimeId
    {
        public Guid Value { get; }

        public ReservedEmployeeTimeId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ReservedEmployeeTimeId data) => data.Value;

        public static implicit operator ReservedEmployeeTimeId(Guid value) => new(value);
    }
}
