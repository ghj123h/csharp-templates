IList<bool> GetResults(int[][] queries) {
    var M = queries.Max(q => q[1]);
    SortedSet<int> set = new();
    set.Add(0); set.Add(M + 1);
    var arr = Enumerable.Range(0, M + 2).ToArray();
    List<bool> ans = new();
    SegmentTreeRangeAddMax tree = new(arr);
    foreach (var q in queries) {
        if (q[0] == 1) {
            var next = set.GetViewBetween(q[1], set.Max).Min;
            var prev = set.GetViewBetween(set.Min, q[1]).Max;
            tree.Update(q[1] + 1, next, prev - q[1]);
        } else ans.Add(tree.Query(0, q[1]) >= q[2]);
    }
    System.Console.WriteLine(string.Join(", ", ans));
    return ans;
}

int[][] q = new int[][] { new int[]{1, 2}, new int[]{1, 3}, new int[]{2, 3, 6}, new int[]{2, 5, 5}, new int[]{2, 9, 5}};
_ = GetResults(q);