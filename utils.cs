public static class EnumerableExtension {
    public static IEnumerable<(TSource, TSource)> PairWise<TSource>(this IEnumerable<TSource> source)
        => PairWise(source, (a, b) => (a, b));

    public static IEnumerable<TResult> PairWise<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> func) {
        using var p = source.GetEnumerator();
        if (!p.MoveNext()) yield break;
        var q = p.Current;
        while (p.MoveNext()) {
            yield return func(q, p.Current);
            q = p.Current;
        }
    }

    public static IEnumerable<(TFirst, TSecond)> Product<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second) {
        foreach (var p in first) foreach (var q in second) yield return (p, q);
    }

    public static Dictionary<TSource, int> Counter<TSource>(this IEnumerable<TSource> source)
        => Counter(source, x => x);

    public static Dictionary<TKey, int> Counter<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        => source.GroupBy(keySelector).ToDictionary(g => g.Key, g => g.Count());
    
    public static IEnumerable<(TSource, int)> ConsecutiveCounter<TSource> (this IEnumerable<TSource> source) {
        if (!source.Any()) yield break;
        TSource element = source.First();
        int count = 1;
        foreach (var next in source.Skip(1)) {
            if (EqualityComparer<TSource>.Default.Equals(element, next)) count++;
            else {
                yield return (element, count);
                element = next;
                count = 1;
            }
        }
        yield return (element, count);
    }
}
