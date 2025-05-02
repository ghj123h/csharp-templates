public class MFGraph : Graph<long> {
    public record MFEdge(int From, int To, long Cap, long Flow);
    public MFGraph(int n, int m) : base(n, m << 1) {}
    public Add(int u, int v, long cap) {
        Add(u, v, cap);
        Add(v, u, 0);
    }
    public MFEdge GetEdge(int i) {
        var _e = e[i << 1];
        var re = e[i << 1 | 1];
        return new MFEdge(re.To, _e.To, _e.Weight + re.Weight, re.Weight);
    }
    public IEnumerable<MFEdge> GetEdges() {
        for (int i = 0; i <= cnt / 2; ++i) {
            yield return GetEdge(i);
        }
    }
    public void ChangeEdge(int i, long newCap, long newFlow) {
        e[i << 1].Weight = newCap - newFlow;
        e[i << 1 | 1].Weight = newFlow;
    }
    public long MaxFlow(int s, int t) {
        return MaxFlow(s, t, long.MaxValue);
    }
    public long MaxFlow(int s, int t, long flowLimit) {
        int[] level = new int[n], iter = new int[n];
        Queue<int> q = new();
        long flow = 0;
        while (flow < flowLimit) {
            bfs();
            if (level[t] == -1) break;
            Array.Fill(iter, 0);
            while (flow < flowLimit) {
                long f = dfs(t, flowLimit - flow);
                if (f == 0) break;
                flow += f;
            }
        }
        return flow;

        void bfs() {
            Array.Fill(level, -1);
            level[s] = 0;
            q.Clear();
            q.Enqueue(s);
            while (q.Count > 0) {
                int v = q.Dequeue();
                foreach (var _e in this[v]) {
                    if (_e.Weight == 0 || level[_e.To] > 0) continue;
                    level[_e.To] = level[v] + 1;
                    if (_e.To == t) return;
                    q.Enqueue(_e.To);
                }
            }
        }
        long dfs(int v, long up) {
            if (v == s) return up;
            long res = 0;
            int level_v = level[v];
            for (int i = head[v]; i >= 0; i = e[v].Next) {
                var _e = e[i];
                if (level[v] <= level[_e.To] || e[i ^ 1].Weight == 0) continue;
                long d = dfs(_e.To, Math.Min(up - res, e[i ^ 1].Weight));
                if (d <= 0) continue;
                _e.Weight += d;
                e[i ^ 1].Weight -= d;
                res += d;
                if (res == up) break;
            }
            return res;
        }
    }
    bool[] minCut(int s) {
        bool[] vis = new bool[n];
        Queue<int> q = new();
        q.Enqueue(s);
        while (q.Count > 0) {
            int p = q.Dequeue();
            vis[p] = true;
            foreach (var _e in this[p]) {
                if (_e.Weight > 0 && !vis[_e.To]) {
                    vis[_e.To] = true;
                    q.Enqueue(_e.To);
                }
            }
        }
        return vis;
    }
}