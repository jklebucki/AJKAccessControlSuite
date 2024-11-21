using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AJKAccessControl.Tests.Providers;
public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;

    public TestAsyncQueryProvider(IQueryProvider inner)
    {
        // Walidacja, aby upewni� si�, �e _inner nie jest TestAsyncQueryProvider
        if (inner is TestAsyncQueryProvider<TEntity>)
        {
            throw new InvalidOperationException("Nesting TestAsyncQueryProvider instances is not allowed.");
        }
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
    }

    public IQueryable CreateQuery(Expression expression)
    {
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        return new TestAsyncEnumerable<TEntity>(expression);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        return new TestAsyncEnumerable<TElement>(expression);
    }

    public object Execute(Expression expression)
    {
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        return _inner.Execute(expression)!;
    }

    public TResult Execute<TResult>(Expression expression)
    {
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        return _inner.Execute<TResult>(expression);
    }

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        // Bezpo�rednie wykonanie na _inner
        return _inner.Execute<TResult>(expression);
    }
}
