using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AJKAccessControl.Domain.Tests.Providers;
public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;

    public TestAsyncQueryProvider(IQueryProvider inner)
    {
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
        Console.WriteLine($"Expression: {expression}");
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        return _inner.Execute<TResult>(expression);
    }

    public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
    {
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        var result = _inner.Execute<IEnumerable<TResult>>(expression);
        return new TestAsyncEnumerable<TResult>(result);
    }

    public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        var result = Execute<TResult>(expression);
        return Task.FromResult(result);
    }

    TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        return Execute<TResult>(expression);
    }
}


