namespace Helper.Core.BusyEmployee.ValueObjects
{
    public sealed record BusyDatesRange
    {
        public DateOnly Start { get; }
        public DateOnly? End { get; }
    }
}
