public class SparseTable {
    private int[,] st;
    private int[] logn;
    private Func<int, int, int> func;
    public SparseTable(int[] nums, Func<int, int, int> func) {
        this.func = func;
        int n = nums.Length;
        logn = new int[n+1]; logn[1] = 0; logn[2] = 1;
        for (int i = 3; i <= n; ++i) logn[i] = logn[i/2] + 1;
        st = new int[n,21];
        for (int i = 0; i < n; ++i) st[i,0] = nums[i];
        for (int j = 1; j <= 20; ++j) {
            for (int i = 0; i+(1<<j)-1 < n; ++i) st[i,j] = func(st[i,j-1], st[i+(1<<j-1),j-1]);
        }
    }

    public int Query(int l, int r) {
        int lg = logn[r-l+1];
        return func(st[l,lg], st[r-(1<<lg)+1,lg]);
    }
}