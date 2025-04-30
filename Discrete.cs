public class Discrete<T> {
    private List<T> list;
    private IComparer<T> comparer;
    public Discrete(IEnumerable<T> lst) : this(lst, null) {}
    public Discrete(IEnumerable<T> lst, IComparer<T> comparer) {
        this.list = lst.ToList();
        this.comparer = comparer ?? Comparer<T>.Default;
        if (list.Count == 0) {
            throw new ArgumentException("You cannot discretize zero elements!");
        }
        list.Sort(comparer);
        int i, j;
        for (i = 0, j = 1; j < list.Count; ++j) {
            if (comparer.Compare(list[i], list[j]) != 0) {
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
