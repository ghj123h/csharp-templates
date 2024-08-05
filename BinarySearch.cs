int UpperBound(IList<int> A, int u) {
    int L = 0, R = A.Count;
    while (L < R) {
        int m = L + (R - L) / 2;
        if (A[m] <= u) L = m + 1;
        else R = m;
    }
    return L;
}

int LowerBound(IList<int> A, int u) {
    int L = 0, R = A.Count;
    while (L < R) {
        int m = L + (R - L) / 2;
        if (A[m] < u) L = m + 1;
        else R = m;
    }
    return L;
}

long LowerBound(long L, long R, Func<long, long> func) {
    while (L < R) {
        long m = L + (R - L) / 2;
        if (func(m) < 0) L = m + 1;
        else R = m;
    }
    return L;
}

long UpperBound(long L, long R, Func<long, long> func) {
    while (L < R) {
        long m = L + (R - L) / 2;
        if (func(m) <= 0) L = m + 1;
        else R = m;
    }
    return L;
}