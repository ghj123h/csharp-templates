public class BITree {
    private int n;
    private int[] C;
    private static int lb(int x) => x & -x;
    public BITree(int n) : this(Enumerable.Repeat(0, n).ToArray()) { }
    public BITree(int[] arr) { // note that the index of arr starts at 0, while C starts at 1
        this.n = arr.Length;
        C = new int[n+1];
        for (int i = 1; i <= n; ++i) {
            C[i] += arr[i-1];
            int j = i + lb(i);
            if (j <= n) C[j] += C[i];
        }
    }
    public void Add(int i, int value) { // note that this i starts at 1
        for (; i <= n; i += lb(i)) C[i] += value;
    }

    public int Query(int i) {
        int res = 0;
        for (; i > 0; i -= lb(i)) res += C[i];
        return res;
    }
}