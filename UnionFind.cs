public class DisjointSet {
    private int[] fa;
    private void Update() {
        for (int i = 0; i < fa.Length; ++i) Find(i);
    }

    public DisjointSet(int n) => fa = Enumerable.Range(0, n).ToArray();
    public int Find(int n) => fa[n] == n ? fa[n] : fa[n] = Find(fa[n]);
    public void Merge(int x, int y) {
        x = Find(x);
        y = Find(y);
        fa[x] = y;
    }
    public int Count { get { Update(); return fa.Distinct().Count(); }}
    public int Size(int x) { Update(); return fa.Where(t => t == fa[x]).Count(); }
}