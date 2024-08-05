public class SimpleSegTree {
    private int[] tree;
    private int n;

    private void Build(int[] arr, int v, int l, int r) {
        if (l == r) tree[v] = arr[l];
        else {
            int m = l + (r - l) / 2;
            Build(arr, v * 2 + 1, l, m);
            Build(arr, v * 2 + 2, m + 1, r);
            tree[v] = tree[v*2+1] + tree[v*2+2];
        }
    }
    private void Update(int v, int i, int l, int r, int value) {
        if (l == r && i == l) tree[v] = value;
        else {
            int m = l + (r - l) / 2;
            if (i <= m) Update(v * 2 + 1, i, l, m, value);
            else Update(v * 2 + 2, i, m + 1, r, value);
            tree[v] = tree[v*2+1] + tree[v*2+2];
        }
    }
    private int Query(int v, int L, int R, int l, int r) {
        if (L > R) return 0;
        else if (l == L && r == R) return tree[v];
        int m = l + (r - l) / 2;
        int left = Query(v * 2 + 1, L, Math.Min(R, m), l, m);
        int right = Query(v * 2 + 2, Math.Max(L, m + 1), R, m + 1, r);
        return left + right;
    }

    public SimpleSegTree(int[] arr) {
        n = arr.Length;
        tree = new int[n * 4];
        Build(arr, 0, 0, n - 1);
    }
    public void Update(int i, int add) => Update(0, i, 0, n - 1, add);
    public int Query(int L, int R) => Query(0, L, R, 0, n - 1);
}

public class LazySegTree {
    private int[] tree, lazy;
    private int n;

    private void Build(int[] arr, int v, int l, int r) {
        if (l == r) tree[v] = arr[l];
        else {
            int m = l + (r - l) / 2;
            Build(arr, v * 2 + 1, l, m);
            Build(arr, v * 2 + 2, m + 1, r);
            tree[v] = Math.Max(tree[v*2+1], tree[v*2+2]);
        }
    }
    private void Push(int v) {
        tree[v*2+1] += lazy[v]; lazy[v*2+1] += lazy[v];
        tree[v*2+2] += lazy[v]; lazy[v*2+2] += lazy[v];
        lazy[v] = 0;
    }
    private void Update(int v, int L, int R, int l, int r, int add) {
        if (l == L && r == R) {
            tree[v] += add;
            lazy[v] += add;
        } else if (L <= R) {
            Push(v);
            int m = l + (r - l) / 2;
            Update(v * 2 + 1, L, Math.Min(R, m), l, m, add);
            Update(v * 2 + 2, Math.Max(L, m + 1), R, m + 1, r, add);
            tree[v] = Math.Max(tree[v*2+1], tree[v*2+2]);
        }
    }
    private int Query(int v, int L, int R, int l, int r) {
        if (L > R) return -0x3f3f3f3f;
        else if (l == L && r == R) return tree[v];
        Push(v);
        int m = l + (r - l) / 2;
        int left = Query(v * 2 + 1, L, Math.Min(R, m), l, m);
        int right = Query(v * 2 + 2, Math.Max(L, m + 1), R, m + 1, r);
        return Math.Max(left, right);
    }

    public RangeSegTree(int[] arr) {
        n = arr.Length;
        tree = new int[n * 4]; lazy = new int[n * 4];
        Build(arr, 0, 0, n - 1);
    }
    public void Update(int L, int R, int add) => Update(0, L, R, 0, n - 1, add);
    public int Query(int L, int R) => Query(0, L, R, 0, n - 1);
}

