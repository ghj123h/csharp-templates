public class MultiSet<T> {
    private Dictionary<T, int> mp;
    private IEqualityComparer<T> comparer;
    public MultiSet() : this(EqualityComprer<T>.Default) {}

    public MultiSet(IEqualityComparer<T> comparer) {
        this.comparer = comparer;
        mp = new();
    }
}