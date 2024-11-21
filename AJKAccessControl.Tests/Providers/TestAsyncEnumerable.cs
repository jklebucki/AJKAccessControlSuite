using System.Linq.Expressions;

namespace AJKAccessControl.Tests.Providers;
public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    public TestAsyncEnumerable(IEnumerable<T> enumerable)
        : base(enumerable)
    { }

    public TestAsyncEnumerable(Expression expression)
        : base(expression)
    { }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        var enumerator = this.AsEnumerable().GetEnumerator();

        if (enumerator is TestAsyncEnumerator<T>)
        {
            throw new InvalidOperationException("Rekurencyjne wywo�anie GetAsyncEnumerator.");
        }

        return new TestAsyncEnumerator<T>(enumerator);
    }

    IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);

    public IEnumerator<T> GetEnumerator()
    {
        var enumerator = this.AsEnumerable().GetEnumerator();

        if (enumerator is TestAsyncEnumerator<T>)
        {
            throw new InvalidOperationException("Rekurencyjne wywo�anie GetEnumerator.");
        }

        return enumerator;
    }
}
