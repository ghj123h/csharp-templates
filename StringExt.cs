public static class StringExt {
    public static int[] GetNext(string s) {
        int n = s.Length;
        int[] res = new int[n];
        for (int i = 1; i < n; ++i) {
            int j = res[i-1];
            while (j > 0 && s[i] != s[j]) j = res[i-1];
            if (s[i] == s[j]) ++j;
            res[i] = j;
        }
        return res;
    }

    public static int[] GetZ(this string s) {
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

    public static (int[], int[]) GetSuffixArray(this string s) {
        int m = 127, n = s.Length, p;
        s = "0" + s;
        int[] rk = new int[2*n+1], sa = new int[2*n+1], id = new int[n+1];
        int[] cnt = new int[Math.Max(128,n+1)], oldrk = new int[2*n+1];
        for (int i = 1; i <= n; ++i) ++cnt[rk[i] = (int)s[i]];
        for (int i = 1; i <= m; ++i) cnt[i] += cnt[i-1];
        for (int i = n; i >= 1; --i) sa[cnt[rk[i]]--] = i;

        for (int w = 1; ; w <<= 1, m = p) {
            int cur = 0;
            for (int i = n - w + 1; i <= n; ++i) id[++cur] = i;
            for (int i = 1; i <= n; ++i) if (sa[i] > w) id[++cur] = sa[i] - w;
            Array.Fill(cnt, 0);
            for (int i = 1; i <= n; ++i) ++cnt[rk[i]];
            for (int i = 1; i <= m; ++i) cnt[i] += cnt[i-1];
            for (int i = n; i >= 1; --i) sa[cnt[rk[id[i]]]--] = id[i];
            p = 0;
            (oldrk, rk) = (rk, oldrk);
            for (int i = 1; i <= n; ++i)
                if (oldrk[sa[i]] == oldrk[sa[i-1]] && oldrk[sa[i]+w] == oldrk[sa[i-1]+w]) rk[sa[i]] = p;
                else rk[sa[i]] = ++p;
            if (p == n) break;
        }
        return (sa[1..(n+1)].Select(x => x - 1).ToArray(), rk[1..(n+1)].Select(x => x - 1).ToArray());
    }

    public static (int[], int[], int[]) GetHeight(this string s) {
        var (sa, rk) = s.GetSuffixArray();
        int n = s.Length;
        int[] h = new int[n];
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