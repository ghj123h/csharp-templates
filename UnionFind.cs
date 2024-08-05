public class DisjointSet {
    private int n;
    private int[] fa;
    private void Update() {
        for (int i = 0; i < fa.Length; ++i) Find(i);
    }

    public DisjointSet(int n) {
        this.n = n;
        fa = Enumerable.Range(0, n).ToArray();
    }

    public int Find(int u) => fa[u] == u ? fa[u] : fa[u] = Find(fa[u]);
    public void Merge(int x, int y) {
        x = Find(x);
        y = Find(y);
        if (x != y) { fa[x] = y; --n; }
    }
    public int Count { get => n; }
    public int Size(int x) { Update(); return fa.Where(t => t == fa[x]).Count(); }
}