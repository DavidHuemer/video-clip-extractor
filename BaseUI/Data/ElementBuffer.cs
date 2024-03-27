namespace BaseUI.Data;

public class ElementBuffer<T>
{
    private List<T> _buffer;
    private Action<List<T>> _flush;
    private int _size;

    public ElementBuffer(int size, Action<List<T>> flush)
    {
        _size = size;
        _buffer = new List<T>();
        _flush = flush;
    }

    public void Add(T element)
    {
        _buffer.Add(element);

        if (_buffer.Count >= _size)
            Flush();
    }

    public void Flush()
    {
        _flush(_buffer);
        _buffer.Clear();
    }
}