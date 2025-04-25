public struct MInt {
    private int val;
    public static readonly int P = 998244353;
    public MInt() { this.val = 0; }
    public MInt(long v) { this.val = (int)(v % P + P) % P; }
    public static long Inv(long a) {
        long u = 0, v = 1, m = P;
        while (a > 0) {
            long t = m / a;
            m -= t * a; (a, m) = (m, a);
            u -= t * v; (u, v) = (v, u);
        }
        return new MInt(u).val;
    }
    public static MInt operator +(MInt a, MInt b) { return a.val + b.val; }
    public static MInt operator -(MInt a, MInt b) { return a.val - b.val; }
    public static MInt operator *(MInt a, MInt b) { return 1L * a.val * b.val; }
    public static MInt operator /(MInt a, MInt b) { return 1L * a.val * Inv(b.val); }
    public static MInt operator ^(MInt a, long b) {
        MInt res = 1;
        while (b > 0) {
            if (b % 2 == 1) {
                res *= a;
            }
            a *= a;
            b >>= 1;
        }
        return res;
    }
    public static implicit operator int(MInt a) { return a.val; }
    public static implicit operator MInt(long a) { return new(a); }
    public override string ToString() {
        return val.ToString();
    }
}