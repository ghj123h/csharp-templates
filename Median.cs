public class MedianArray {
    PriorityQueue<int, int> low, high;
    public MedianArray() { low = new(); high = new(); }
    public void Add(int v) {
        if (low.Count == high.Count == 0) low.Enqueue(v, -v);
        else if (v < low.Peek()) low.Enqueue(v, -v);
        else high.Enqueue(v, v);
        update();
    }

    public int Median { get => low.Peek(); }

    private void update() {
        if (low.Count - high.Count > 1) {
            var v = low.Dequeue();
            high.Enqueue(v, v);
        } else if (low.Count - high.Count < 0) {
            var v = high.Dequeue();
            low.Enqueue(v, -v);
        }
    }
}