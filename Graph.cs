public class Edge<T> {
    public int To { get; set; }
    public T Weight { get; set; }
    public int Next { get; set; }
}

public class Graph<T> {
    protected Edge<T>[] e;
    protected int[] head;
    protected int cnt, n;
    public Graph(int n, int m) {
        head = new int[n];
        Array.Fill(head, -1);
        e = new Edge<T>[m];
        cnt = -1;
        this.n = n;
    }

    public void Add(int u, int v) => Add(u, v, default(T));
    public void Add(int u, int v, T w) {
        e[++cnt] = new Edge<T> { To = v, Weight = w, Next = head[u] };
        head[u] = cnt;
    }

    public IEnumerable<Edge<T>> this[int u] {
        get {
            for (int v = head[u]; v >= 0; v = e[v].Next) yield return e[v];
        }
    }

    public int Count { get => n; }
}