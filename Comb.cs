public static class Comb
{
    private static readonly int V = 500_000 + 10;
    private static MInt[] fact = new MInt[V], inv_fact = new MInt[V];
    static Comb()
    {
        fact[0] = 1;
        for (int i = 1; i < V; ++i) fact[i] = fact[i - 1] * i;
        inv_fact[^1] = MInt.Inv(fact[^1]);
        for (int i = V - 2; i >= 0; --i) inv_fact[i] = inv_fact[i + 1] * (i + 1);
    }
    public static MInt Choose(int n, int k) => fact[n] * inv_fact[k] * inv_fact[n - k];
    public static MInt Arrange(int n, int k) => fact[n] * inv_fact[n - k];
}

public static class CombEx {
    public static MInt Comb(this int n, int k) => Comb.Choose(n, k);
    public static MInt Arrange(this int n, int k) => Comb.Arrange(n, k);
}