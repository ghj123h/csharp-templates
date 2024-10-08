public static class StringExt {
    public static int[] GetPi(string s) {
        int n = s.Length;
        int[] res = new int[n];
        for (int i = 1; i < n; ++i) {
            int j = res[i-1];
            while (j > 0 && s[i] != s[j]) j = res[j-1];
            if (s[i] == s[j]) ++j;
            res[i] = j;
        }
        return res;
    }

    public static int[] GetZ(this string s) {
        int n = s.Length;
        int[] z = new int[n];
        for (int i = 1, l = 0, r = 0; i < n; ++i) {
            if (i <= r && z[i - l] < r - i + 1) z[i] = z[i-l];
            else {
                z[i] = Math.Max(0, r - i + 1);
                while (i + z[i] < n && s[z[i]] == s[i+z[i]]) ++z[i];
            }
            if (i + z[i] - 1 > r) { l = i; r = i + z[i] - 1; }
        }
        return z;
    }

    public static int[] GetSuffixArray(this string s) => s.SAIS(26, c => c - 'a')[1..^0];
    public static int[] SAIS(this string s, int sig, Func<char, int> h) {
        return Sub(s.Select(c => h(c) + 1).Append(0).ToArray(), s.Length + 1, sig);

        int[] Sub(int[] s, int len, int sig) {
            int n = len - 1;
            bool[] type = new bool[n+1]; // true: S, false: L
            int[] name = new int[n+1], sa = new int[n+1];
            int[] b = new int[sig+1], lb = new int[sig+1], sb = new int[sig+1];
            for (int i = 0; i <= n; ++i) b[s[i]]++;
            for (int i = 1; i <= sig; ++i) {
                b[i] += b[i-1];
                lb[i] = b[i-1];
                sb[i] = b[i] - 1;
            }
            type[n] = true;
            for (int i = n - 1; i >= 0; --i) {
                if (s[i] < s[i+1]) type[i] = true;
                else if (s[i] == s[i+1]) type[i] = type[i+1];
            }
            var pos = Enumerable.Range(1, n).Where(i => IsLMS(i)).ToArray();
            int cnt = pos.Length;
            Array.Fill(sa, -1);
            for (int i = 0; i < cnt; ++i) sa[sb[s[pos[i]]]--] = pos[i];
            InducedSort();
            Array.Fill(name, -1);
            int last = -1, names = 1;
            bool rep = false;
            for (int i = 1; i <= n; ++i) {
                int x = sa[i];
                if (IsLMS(x)) {
                    if (last >= 0 && !SubstringEqual(x, last)) ++names;
                    if (last >= 0 && names == name[last]) rep = true;
                    name[x] = names;
                    last = x;
                }
            }
            name[n] = 0;

            var s1 = name.Where(na => na >= 0).ToArray();
            int[] sa1;
            if (!rep) {
                sa1 = new int[cnt+1];
                for (int i = 0; i < cnt; ++i) sa1[s1[i]] = i;
            } else sa1 = Sub(s1, cnt, names);
            lb[0] = sb[0] = 0;
            for (int i = 1; i <= sig; ++i) {
                lb[i] = b[i-1]; sb[i] = b[i] - 1;
            }
            Array.Fill(sa, -1);
            for (int i = cnt - 1; i >= 0; --i) sa[sb[s[pos[sa1[i]]]]--] = pos[sa1[i]];
            InducedSort();
            return sa;

            bool IsLMS(int i) => i > 0 && type[i] && !type[i-1];
            bool SubstringEqual(int i, int j) {
                do {
                    if (s[i] != s[j]) return false;
                    ++i; ++j;
                } while (!IsLMS(i) && !IsLMS(j));
                return s[i] == s[j];
            }
            void InducedSort() {
                for (int i = 0; i <= n; ++i) if (sa[i] > 0 && !type[sa[i]-1]) sa[lb[s[sa[i]-1]]++] = sa[i] - 1;
                for (int i = 1; i <= sig; ++i) sb[i] = b[i] - 1;
                for (int i = n; i >= 0; --i) if (sa[i] > 0 && type[sa[i]-1]) sa[sb[s[sa[i]-1]]--] = sa[i] - 1;
            }
        }
    }

    public static (int[], int[], int[]) GetHeight(this string s) {
        var sa = s.SAIS(26, c => c - 'a')[1..^0];
        int n = s.Length;
        int[] h = new int[n], rk = new int[n];
        for (int i = 0; i < n; ++i) rk[sa[i]] = i;
        for (int i = 0, k = 0; i < s.Length; ++i) if (rk[i] != 0) {
            if (k > 0) --k;
            try {
                while (s[i+k] == s[sa[rk[i]-1]+k]) ++k;
            } catch (Exception) {}
            h[rk[i]] = k;
        }
        return (sa, rk, h);
    }
}