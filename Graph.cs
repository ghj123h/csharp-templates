public class Edge {
    public int From { get; set; }
    public int To { get; set; }
    public int Weight { get; set; }
}

public class Graph {
    private Edge[] e;
    private int[] head, next;
    private int cnt, n;
    public Graph(int n, int m) {
        head = new int[n];
        Array.Fill(head, -1);
        next = new int[m];
        e = new Edge[m];
        cnt = -1;
        this.n = n;
    }

    public Graph(int n, int[][] edges) : this(n, edges.Length) {
        foreach (var e in edges) {
            if (e.Length == 2) Add(e[0], e[1]);
            else Add(e[0], e[1], e[2]);
        }
    }

    public void Add(int u, int v) => Add(u, v, 1);
    public virtual void Add(int u, int v, int w) {
        next[++cnt] = head[u];
        head[u] = cnt;
        e[cnt] = new Edge { From = u, To = v, Weight = w };
    }

    public IEnumerable<Edge> GetSon(int u) {
        for (int v = head[u]; v >= 0; v = next[v]) yield return e[v];
    }

    public int[] Dijkstra(int u) {
        int[] d = new int[n];
        PriorityQueue<int, int> q = new();
        bool[] vis = new bool[n];
        Array.Fill(d, 0x3f3f3f3f);
        q.Enqueue(u, d[u] = 0);
        while (q.Count > 0) {
            var p = q.Dequeue();
            if (vis[p]) continue;
            vis[p] = true;
            foreach (var e in GetSon(p)) {
                int v = e.To, w = e.Weight;
                if (d[v] > d[p] + w) {
                    d[v] = d[p] + w;
                    q.Enqueue(v, d[v]);
                }
            }
        }
        return d;
    }
}