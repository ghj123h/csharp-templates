public class Trie {
    public class TrieNode {
        public int len = 0;
        public TrieNode[] son = new TrieNode[26];
        public TrieNode fail = null, last = null;
    }

    public TrieNode Head { get; }
    public Func<char, int> Hash { get; set; }

    public Trie() : this(new()) { }
    public Trie(TrieNode head) { Hash = c => c - 'a'; Head = head; }
    public bool Add(string s) {
        TrieNode p = Head;
        foreach (var c in s) {
            int i = Hash(c);
            if (p.son[i] == null) p.son[i] = new();
            p = p.son[i];
        }
        if (p.len > 0) return true;
        else { p.len = s.Length; return false; }
    }
}

public class ACAutomation {
    public Trie Trie { get; }
    public ACAutomation(string[] words) {
        Trie = new();
        foreach (var word in words) Trie.Add(word);
        Queue<Trie.TrieNode> q = new();
        var head = Trie.Head;
        head.fail = head.last = head;
        for (int i = 0; i < head.son.Length; ++i)
            if (head.son[i] == null) {
                head.son[i] = head;
            } else {
                head.son[i].fail = head.son[i].last = head;
                q.Enqueue(head.son[i]);
            }
        while (q.Count > 0) {
            var u = q.Dequeue();
            for (int i = 0; i < u.son.Length; ++i) {
                if (u.son[i] != null) {
                    var v = u.son[i];
                    v.fail = u.fail.son[i];
                    q.Enqueue(v);
                    v.last = v.fail.len > 0 ? v.fail : v.fail.last;
                } else u.son[i] = u.fail.son[i];
            }
        }
    }

    public List<(int start, int end)> Fit(string s) {
        var u = Trie.Head;
        var f = Trie.Hash;
        List<(int start, int end)> res = new();
        for (int i = 0; i < s.Length; ++i) {
            u = u.son[f(s[i])];
            for (var v = u; v != Trie.Head; v = v.last) if (v.len > 0) res.Add((i - v.len + 1, i));
        }
        return res;
    }
}