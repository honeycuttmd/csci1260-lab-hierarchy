namespace HierarchyLab;

// adds weight and shipping behavior for physical stock items
public abstract class PhysicalGood : StockItem
{
    // private field
    private double _weightPounds;

    // public constant
    public const decimal HandlingRate = 0.60m;

    // public read-only property
    public double WeightPounds
    {
        get { return _weightPounds; }
    }

    // constructor
    protected PhysicalGood(
        string sku,
        string name,
        decimal unitPrice,
        int quantityOnHand,
        double weightPounds)
        : base(sku, name, unitPrice, quantityOnHand)
    {
        if (weightPounds < 0)
        {
            _weightPounds = 0;
        }
        else
        {
            _weightPounds = weightPounds;
        }
    }

    // methods
    public decimal ShippingCost()
    {
        return (decimal)WeightPounds * HandlingRate;
    }

    public override string Describe()
    {
        return base.Describe() + String.Format(", {0:N1} lb", WeightPounds);
    }
}