
IEnumerable<(int, int)> neighbors(int r, int c) {
    if (r > 0) yield return (r - 1, c);
    if (c > 0) yield return (r, c - 1);
    if (r < m - 1) yield return (r + 1, c);
    if (c < n - 1) yield return (r, c + 1);
}
