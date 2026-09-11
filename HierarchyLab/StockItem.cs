namespace HierarchyLab;

// stores shared data and behavior for all stock item types
public abstract class StockItem : IReportable
{
    // private fields
    private string _sku;
    private string _name;
    private decimal _unitPrice;
    private int _quantityOnHand;
    private List<StockMovement> _history;
    private int _nextSeq;

    // public read-only properties
    public string Sku
    {
        get { return _sku; }
    }

    public string Name
    {
        get { return _name; }
    }

    public decimal UnitPrice
    {
        get { return _unitPrice; }
    }

    public int QuantityOnHand
    {
        get { return _quantityOnHand; }
    }

    public int MoveCount
    {
        get { return _history.Count; }
    }

    // constructor
    protected StockItem(string sku, string name, decimal unitPrice, int quantityOnHand)
    {
        _sku = sku;
        _name = name;

        if (unitPrice < 0)
        {
            _unitPrice = 0;
        }
        else
        {
            _unitPrice = unitPrice;
        }

        if (quantityOnHand < 0)
        {
            _quantityOnHand = 0;
        }
        else
        {
            _quantityOnHand = quantityOnHand;
        }

        _history = new List<StockMovement>();
        _nextSeq = 1;
    }

    // abstract methods
    public abstract string Category();
    public abstract decimal HandlingFee();

    // public methods
    public decimal ExtendedValue()
    {
        return (UnitPrice + HandlingFee()) * QuantityOnHand;
    }

    public bool Receive(int count)
    {
        if (count <= 0)
        {
            return false;
        }

        _quantityOnHand += count;

        StockMovement movement = new StockMovement(_nextSeq, "Received", count);
        _history.Add(movement);

        _nextSeq++;

        return true;
    }

    public bool Release(int count)
    {
        if (count <= 0 || count > _quantityOnHand)
        {
            return false;
        }

        _quantityOnHand -= count;

        StockMovement movement = new StockMovement(_nextSeq, "Released", count);
        _history.Add(movement);

        _nextSeq++;

        return true;
    }

    public string MovementLines()
    {
        string lines = "";

        for (int i = 0; i < _history.Count; i++)
        {
            lines += _history[i].Describe();

            if (i < _history.Count - 1)
            {
                lines += "\n";
            }
        }

        return lines;
    }

    public virtual string Describe()
    {
        return String.Format("{0} {1} ({2})", Sku, Name, Category());
    }

    public string ReportLine()
    {
        return String.Format(
            " {0,-7} {1,-21} {2,-10} {3,4} ${4,11:N2}",
            Sku,
            Name,
            Category(),
            QuantityOnHand,
            ExtendedValue()
        );
    }

    public override string ToString()
    {
        return Describe();
    }
}