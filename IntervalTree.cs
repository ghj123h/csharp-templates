public class IntervalTree { // store all interval in [0,n)
    public class Node {
        public SortedSet<(int, int)> begin = new();
        public SortedSet<(int, int)> end = new();
    }
    
    private int n;
    private Node[] tree;

    public IntervalTree(int n) {
        tree = new Node[n]; // tree[i] indicates center = i
        this.n = n;
        while (n-- > 0) tree[n] = new();
    }

    public void Insert(int left, int right) { // insert [l,r)
        if (left >= right || right > n) return;
        int L = 0, R = n;
        while (L < R) {
            int m = L + (R - L) / 2;
            if (m >= left && m < right) {
                tree[m].begin.Add((left, right));
                tree[m].end.Add((-right, -left));
                break;
            } else if (m < left) L = m + 1;
            else R = m;
        }
    }

    public void Remove(int left, int right) {
        if (left >= right || right > n) return;
        int L = 0, R = n;
        while (L < R) {
            int m = L + (R - L) / 2;
            if (m >= left && m < right) {
                tree[m].begin.Remove((left, right));
                tree[m].end.Remove((-right, -left));
                break;
            } else if (m < left) L = m + 1;
            else R = m;
        }
    }

    public bool Contains(int left, int right) {
        if (left >= right || right > n) return;
        int L = 0, R = n;
        while (L < R) {
            int m = L + (R - L) / 2;
            if (m >= left && m < right) {
                return tree[m].begin.Contains((left, right));
            } else if (m < left) L = m + 1;
            else R = m;
        }
        return false;
    }

    public List<(int, int)> OverLap(int p) {
        List<(int, int)> res = new();
        Sub(p, 0, n);
        return res;

        void Sub(int p, int l, int r) {
            if (l >= r) return;
            int m = l + (r - l) / 2;
            if (m < p) {
                foreach (var (rr, ll) in tree[m].end) {
                    if (rr >= -p) break;
                    res.Add((-ll, -rr));
                }
                Sub(p, m + 1, r);
            } else if (m > p) {
                foreach (var (ll, rr) in tree[m].begin) {
                    if (ll > p) break;
                    res.Add((ll, rr));
                }
                Sub(p, l, m);
            } else res.AddRange(tree[m].begin);
        }
    }
}
