IEnumerable<(int, int)> neighbors(int r, int c) {
    if (r > 0) yield return (r - 1, c);
    if (c > 0) yield return (r, c - 1);
    if (r < m - 1) yield return (r + 1, c);
    if (c < n - 1) yield return (r, c + 1);
}

void Inc<T>(IDictionary<T, int> mp, T key) {
    if (mp.TryGetValue(key, out var v)) mp[key] = v + 1;
    else mp.Add(key, 1);
}

void Dec<T>(IDictionary<T, int> mp, T key) {
    if (--mp[key] == 0) mp.Remove(key);
}