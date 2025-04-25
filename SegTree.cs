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

public class LazySegTree<S,F> {
    private S[] d;
    private F[] lz;
    private int n, size, log;
    private Func<S, S, S> op;
    private Func<S> e;
    private Func<F, F, F> composition;
    private Func<F, S, S> mapping;
    private Func<F> id;
    private void Update(int i) => d[i] = op(d[i<<1], d[i<<1|1]);
    private void AllApply(int k, F f) {
        d[k] = mapping(f, d[k]);
        if (k < size) lz[k] = composition(f, lz[k]);
    }
    private void Push(int k) {
        AllApply(k << 1, lz[k]);
        AllApply(k << 1 | 1, lz[k]);
        lz[k] = id();
    }
    public LazySegTree(IList<S> v, Func<S, S, S> op, Func<S> e,
        Func<F, F, F> composition, Func<F, S, S> mapping, Func<F> id) {
        this.composition = composition;
        this.mapping = mapping;
        this.id = id;
        n = v.Count;
        this.op = op;
        this.e = e;
        log = 0;
        while ((1 << log) < n) ++log;
        size = 1 << log;
        d = new S[size << 1]; lz = new F[size];
        Array.Fill(d, e()); Array.Fill(lz, id());
        for (int i = 0; i < n; ++i) d[size+i] = v[i];
        for (int i = size - 1; i >= 1; --i) Update(i);
    }
    public LazySegTree(int n, Func<S, S, S> op, Func<S> e,
        Func<F, F, F> composition, Func<F, S, S> mapping, Func<F> id)
        : this(Enumerable.Repeat(e(), n).ToArray(), op, e, composition, mapping, id) {}
    public void Set(int p, S x) {
        p += size;
        for (int i = log; i >= 1; --i) Push(p >> i);
        d[p] = x;
        for (int i = 1; i <= log; ++i) Update(p >> i);
    }
    public S Get(int p) {
        p += size;
        for (int i = log; i >= 1; i--) Push(p >> i);
        return d[p];
    }
    public S Prod(int l, int r) {
        if (l == r) return e();
        l += size;
        r += size;
        for (int i = log; i >= 1; i--) {
            if (((l >> i) << i) != l) Push(l >> i);
            if (((r >> i) << i) != r) Push(r >> i);
        }
        S sml = e(), smr = e();
        while (l < r) {
            if (l % 2 == 1) sml = op(sml, d[l++]);
            if (r % 2 == 1) smr = op(d[--r], smr);
            l >>= 1;
            r >>= 1;
        }
        return op(sml, smr);
    }
    public S AllProd() => d[1];
    public void Apply(int p, F f) {
        p += size;
        for (int i = log; i >= 1; i--) Push(p >> i);
        d[p] = mapping(f, d[p]);
        for (int i = 1; i <= log; i++) Update(p >> i);
    }
    public void Apply(int l, int r, F f) {
        if (l == r) return;
        l += size;
        r += size;
        for (int i = log; i >= 1; i--) {
            if (((l >> i) << i) != l) Push(l >> i);
            if (((r >> i) << i) != r) Push((r - 1) >> i);
        }
        int l2 = l, r2 = r;
        while (l < r) {
            if (l % 2 == 1) AllApply(l++, f);
            if (r % 2 == 1) AllApply(--r, f);
            l >>= 1;
            r >>= 1;
        }
        l = l2;
        r = r2;
        for (int i = 1; i <= log; i++) {
            if (((l >> i) << i) != l) Update(l >> i);
            if (((r >> i) << i) != r) Update((r - 1) >> i);
        }
    }
    public int MaxRight(int l, Func<S, bool> g) {
        if (l == n) return n;
        l += size;
        for (int i = log; i >= 1; i--) Push(l >> i);
        S sm = e();
        do {
            while (l % 2 == 0) l >>= 1;
            if (!g(op(sm, d[l]))) {
                while (l < size) {
                    Push(l);
                    l = (2 * l);
                    if (g(op(sm, d[l]))) {
                        sm = op(sm, d[l]);
                        l++;
                    }
                }
                return l - size;
            }
            sm = op(sm, d[l]);
            l++;
        } while ((l & -l) != l);
        return n;
    }

    public int MinLeft(int r, Func<S, bool> g) {
        if (r == 0) return 0;
        r += size;
        for (int i = log; i >= 1; i--) Push((r - 1) >> i);
        S sm = e();
        do {
            r--;
            while (r > 1 && r % 2 == 1) r >>= 1;
            if (!g(op(d[r], sm))) {
                while (r < size) {
                    Push(r);
                    r = (2 * r + 1);
                    if (g(op(d[r], sm))) {
                        sm = op(d[r], sm);
                        r--;
                    }
                }
                return r + 1 - size;
            }
            sm = op(d[r], sm);
        } while ((r & -r) != r);
        return 0;
    }
}
