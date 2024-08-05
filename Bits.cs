public static class Bits {
    public static IEnumerable<int> GetBits(this int x) {
        while (x > 0) {
            int low = x & -x;
            yield return low;
            x ^= low;
        }
    }

    public static IEnumerable<int> Gosper(int n, int k) {
        int s = (1 << k) - 1, m = 1 << n;
        while (s < m) {
            yield return s;
            int low = s & -s;
            int r = s + low;
            s = ((s ^ r) >> (BitOperations.TrailingZeroCount(low) + 2)) | r;
        }
    }
}