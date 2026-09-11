namespace HierarchyLab;

// represents a service stock item that can go on sale
public class ServiceItem : StockItem, IDiscountable
{
    // private field
    private double _laborHours;

    // public read-only properties
    public double LaborHours
    {
        get { return _laborHours; }
    }

    public bool IsOnSale
    {
        get { return LaborHours >= 2.0; }
    }

    // constructor
    public ServiceItem(
        string sku,
        string name,
        decimal unitPrice,
        int quantityOnHand,
        double laborHours)
        : base(sku, name, unitPrice, quantityOnHand)
    {
        _laborHours = laborHours;
    }

    // methods
    public override string Category()
    {
        return "Service";
    }

    public override decimal HandlingFee()
    {
        return 0m;
    }

    public decimal SalePrice()
    {
        if (IsOnSale)
        {
            return UnitPrice * 0.85m;
        }

        return UnitPrice;
    }

    public override string Describe()
    {
        return base.Describe() + String.Format(", {0:N1} labor hours", LaborHours);
    }
}