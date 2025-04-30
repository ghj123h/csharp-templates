public class Discrete<T> {
    private readonly List<T> list;
    private readonly IComparer<T> comparer;
    public Discrete(IEnumerable<T> elements) : this(elements, null) { }
    public Discrete(IEnumerable<T> elements, IComparer<T> comparer) {
        this.list = elements.ToList();
        this.comparer = comparer ?? Comparer<T>.Default;
        if (list.Count == 0) {
            throw new ArgumentException("You cannot discretize zero elements!");
        }
        list.Sort(this.comparer);
        int i, j;
        for (i = 0, j = 1; j < list.Count; ++j) {
            if (this.comparer.Compare(list[i], list[j]) != 0) {
                list[++i] = list[j];
            }
        }
        ++i;
        while (i < list.Count) {
            list.RemoveAt(list.Count - 1);
        }
    }
    public int Count { get => list.Count; }
    public int this[T index] {
        get {
            int j = list.BinarySearch(index, comparer);
            if (j < 0) {
                j = ~j;
            }
            return j;
        }
    }
}