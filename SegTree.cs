public class SegTree<S> {
    private S[] d;
    private int n, size, log;
    private Func<S, S, S> op;
    private Func<S> e;
    private void Update(int i) => d[i] = op(d[i<<1], d[i<<1|1]);
    public SegTree(int n, Func<S, S, S> op, Func<S> e) : this(Enumerable.Repeat(e(), n).ToArray(), op, e) { }
    public SegTree(IList<S> v, Func<S, S, S> op, Func<S> e) {
        n = v.Count;
        this.op = op;
        this.e = e;
        log = 0;
        while ((1 << log) < n) ++log;
        size = 1 << log;
        d = new S[size << 1];
        Array.Fill(d, e());
        for (int i = 0; i < n; ++i) d[size+i] = v[i];
        for (int i = size - 1; i >= 1; --i) Update(i);
    }
    public void Set(int p, S x) {
        p += size;
        d[p] = x;
        for (int i = 1; i <= log; i++) Update(p >> i);
    }
    public S Get(int p) {
        return d[p+size];
    }
    public S Prod(int l, int r) {
        S sml = e(), smr = e();
        l += size;
        r += size;
        while (l < r) {
            if ((l & 1) == 1) sml = op(sml, d[l++]);
            if ((r & 1) == 1) smr = op(d[--r], smr);
            l >>= 1;
            r >>= 1;
        }
        return op(sml, smr);
    }
    public S AllProd() => d[1];
    public int MaxRight(int l, Func<S, bool> f) {
        if (l == n) return n;
        l += size;
        S sm = e();
        do {
            while (l % 2 == 0) l >>= 1;
            if (!f(op(sm, d[l]))) {
                while (l < size) {
                    l = (2 * l);
                    if (f(op(sm, d[l]))) {
                        sm = op(sm, d[l]);
                        ++l;
                    }
                }
                return l - size;
            }
            sm = op(sm, d[l]);
            l++;
        } while ((l & -l) != l);
        return n;
    }
    public int MinLeft(int r, Func<S, bool> f) {
        if (r == 0) return 0;
        r += size;
        S sm = e();
        do {
            r--;
            while (r > 1 && r % 2 == 1) r >>= 1;
            if (!f(op(d[r], sm))) {
                while (r < size) {
                    r = (2 * r + 1);
                    if (f(op(d[r], sm))) {
                        sm = op(d[r], sm);
                        --r;
                    }
                }
                return r + 1 - size;
            }
            sm = op(d[r], sm);
        } while ((r & -r) != r);
        return 0;
    }
}
