public static class ArrayExt {
    public static Span<T> AsSpan<T>(this T[,] array) => asSpan(array);
    public static Span<T> AsSpan<T>(this T[,,] array) => asSpan(array);
    static Span<T> asSpan<T>(Array array)
        => System.Runtime.InteropServices.MemoryMarshal.CreateSpan(
            ref System.Runtime.CompilerServices.Unsafe.As<byte, T>(
                ref System.Runtime.InteropServices.MemoryMarshal.GetArrayDataReference(array)
            ), array.Length);
}