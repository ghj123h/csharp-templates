public class DLX {
    private int n, m, tot;
    private int[] first, siz;
    private List<int> L, R, U, D, col, row;

    public DLX(int r, int c) {
        n = r; m = c;
        first = new int[r+1]; siz = new int[c+1];
        L = new(); R = new(); U = new(); D = new(); col = new(); row = new();
        for (int i = 0; i <= c; ++i) {
            L.Add(i - 1); R.Add(i + 1); U.Add(i); D.Add(i);
            col.Add(-1); row.Add(-1);
        }
        L[0] = c; R[c] = 0; tot = c;
    }

    public void Insert(int r, int c) {
        ++tot;
        col.Add(c); row.Add(r); ++siz[c];
        D.Add(D[c]); U[D[c]] = tot; U.Add(c); D[c] = tot;
        if (first[r] == 0) {
            first[r] = tot; L.Add(tot); R.Add(tot);
        } else {
            R.Add(R[first[r]]); L[R[first[r]]] = tot;
            L.Add(first[r]); R[first[r]] = tot;
        }
    }

    private void Remove(int c) {
        L[R[c]] = L[c]; R[L[c]] = R[c];
        for (int i = D[c]; i != c; i = D[i])
            for (int j = R[i]; j != i; j = R[j]) {
                U[D[j]] = U[j]; D[U[j]] = D[j]; --siz[col[j]];
            }
    }

    private void Recover(int c) {
        for (int i = U[c]; i != c; i = U[i])
            for (int j = L[i]; j != i; j = L[j]) {
                U[D[j]] = D[U[j]] = j; ++siz[col[j]];
            }
        L[R[c]] = R[L[c]] = c;
    }

    public int[] Dance() {
        int[] ans = new int[n];
        int res = dance(0, ans);
        if (res < 0) return null;
        else return ans.Take(res).ToArray();
    }

    private int dance(int dep, int[] res) {
        if (R[0] == 0) return dep;
        int c = R[0], ans = -1;
        for (int i = R[0]; i != 0; i = R[i]) 
            if (siz[i] < siz[c]) c = i;
        Remove(c);
        for (int i = D[c]; i != c; i = D[i]) {
            res[dep] = row[i];
            for (int j = R[i]; j != i; j = R[j]) Remove(col[j]);
            ans = dance(dep + 1, res);
            for (int j = L[i]; j != i; j = L[j]) Recover(col[j]);
            if (ans >= 0) break;
        }
        Recover(c);
        return ans;
    }
}