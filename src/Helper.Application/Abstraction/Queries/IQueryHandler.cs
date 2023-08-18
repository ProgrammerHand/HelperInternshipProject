namespace Helper.Application.Abstraction.Queries
{
    public interface IQueryHandler<in TQuerry, TResult> where TQuerry : class, IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuerry querry);
    }
}
