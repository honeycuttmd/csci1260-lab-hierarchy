namespace HierarchyLab;

// represents a physical stock item that does not expire
public class DurableGood : PhysicalGood
{
    // private field
    private int _warrantyMonths;

    // public read-only property
    public int WarrantyMonths
    {
        get { return _warrantyMonths; }
    }

    // constructor
    public DurableGood(
        string sku,
        string name,
        decimal unitPrice,
        int quantityOnHand,
        double weightPounds,
        int warrantyMonths)
        : base(sku, name, unitPrice, quantityOnHand, weightPounds)
    {
        _warrantyMonths = warrantyMonths;
    }

    // methods
    public override string Category()
    {
        return "Durable";
    }

    public override decimal HandlingFee()
    {
        return ShippingCost();
    }

    public override string Describe()
    {
        return base.Describe() + String.Format(", {0} month warranty", WarrantyMonths);
    }
}