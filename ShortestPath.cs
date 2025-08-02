public static class ShortestPath {
    public static bool SPFA(this Graph<long> G, long[] dis, int s) {
        int n = G.Count;
        bool[] vis = new bool[n];
        int[] cnt = new int[n];
        Array.Fill(dis, long.MaxValue / 2);
        dis[s] = 0;
        vis[s] = true;
        Queue<int> q = new();
        q.Enqueue(s);
        while (q.Count > 0) {
            int u = q.Dequeue();
            vis[u] = false;
            foreach (var e in G[u]) {
                var (v, w) = (e.To, e.Weight);
                if (dis[v] > dis[u] + w) {
                    dis[v] = dis[u] + w;
                    cnt[v] = cnt[u] + 1;
                    if (cnt[v] >= n) return false;
                    if (!vis[v]) {
                        q.Enqueue(v);
                        vis[v] = true;
                    }
                }
            }
        }
        return true;
    }

    public static long[] Dijkstra(this Graph<long> G, int s) {
        int n = G.Count;
        bool[] vis = new bool[n];
        long[] dis = new long[n];
        Array.Fill(dis, long.MaxValue / 2);
        dis[s] = 0;
        PriorityQueue<int, long> q = new();
        q.Enqueue(s, dis[s]);
        while (q.Count > 0) {
            var u = q.Dequeue();
            if (vis[u]) continue;
            vis[u] = true;
            foreach (var e in G[u]) {
                var (v, w) = (e.To, e.Weight);
                if (dis[v] > dis[u] + w) {
                    dis[v] = dis[u] + w;
                    q.Enqueue(v, dis[v]);
                }
            }
        }
        return dis;
    }

    public static long[,] Floyd(this Graph<long> G) {
        int n = G.Count;
        long[,] f = new long[n,n];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                if (i == j) continue;
                f[i,j] = long.MaxValue / 2;
            }
            foreach (var e in G[i]) {
                var (v, w) = (e.To, e.Weight);
                f[i,v] = w;
            }
        }
        for (int k = 0; k < n; ++k) {
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < n; ++j) {
                    f[i,j] = Math.Min(f[i,j], f[i,k] + f[k,j]);
                }
            }
        }
        return f;
    }
}