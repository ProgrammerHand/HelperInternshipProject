namespace Helper.Core.Exceptions
{
    public sealed class NoDescriptionGivenException : CustomException
    {
        public NoDescriptionGivenException() : base("No description given")
        {
        }
    }
}
