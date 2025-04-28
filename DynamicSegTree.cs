public class DynamicSegTree<S> {
    private S[] d;
    private int[] ls, rs;
    private int n, size, tot;
    private Func<S, S, S> op;
    private Func<S> e;
    public DynamicSegTree(int n, Func<S, S, S> op, Func<S> e) {
        this.n = n;
        this.op = op;
        this.e = e;
        size = n;
        tot = 0;
        d = new S[size << 2];
        ls = new int[size << 2];
        rs = new int[size << 2];
        Array.Fill(d, e());
    }
    public void Set(int p, S s) {
        int l = 0, r = size - 1, i = 1;
        Stack<int> st = new();
        while (l < r) {
            int mid = (l + r) >> 1;
            st.Push(i);
            if (p > mid) {
                if (rs[i] == 0) {
                    rs[i] = ++tot;
                    fa[tot] = i;
                }
                i = rs[i];
                l = mid + 1;
            } else {
                if (ls[i] == 0) {
                    ls[i] = ++tot;
                    fa[tot] = i;
                }
                i = ls[i];
                r = mid;
            }
        }
        d[i] = s;
        while (st.Count > 0) {
            i = st.Pop();
            d[i] = op(d[ls[i]], d[rs[i]]);
        }
    }
    public S Get(int p) {
        int l = 0, r = size - 1, i = 1;
        while (l < r) {
            int mid = (l + r) >> 1;
            if (p > mid) {
                i = rs[i];
                l = mid + 1;
            } else {
                i = ls[i];
                r = mid;
            }
        }
        return d[i];
    }
    public S AllProd() => d[1];
    public S Prod(int l, int r) {
        return prod(1, 0, size - 1);

        S prod(int i, int cl, int cr) {
            if (i == 0 || cl > cr || l > cr || r < cl) {
                return e();
            } else if (cl >= l && cr <= r) {
                return d[i];
            }
            int mid = (cl + cr) >> 1;
            S left = prod(ls[i], cl, mid);
            S right = prod(rs[i], mid + 1, cr);
            return op(left, right);
        }
    }
}