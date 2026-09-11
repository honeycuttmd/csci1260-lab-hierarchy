namespace HierarchyLab;

// represents one completed change in stock quantity
public class StockMovement
{
    // private fields
    private int _seq;
    private string _kind;
    private int _count;

    // public read-only properties
    public int Seq
    {
        get { return _seq; }
    }

    public string Kind
    {
        get { return _kind; }
    }

    public int Count
    {
        get { return _count; }
    }

    // constructor
    public StockMovement(int seq, string kind, int count)
    {
        _seq = seq;
        _kind = kind;
        _count = count;
    }

    // methods
    public string Describe()
    {
        return String.Format("    move {0}: {1} {2}", Seq, Kind, Count);
    }
}