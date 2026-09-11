namespace HierarchyLab;

// represents a physical stock item that can expire and go on sale
public class PerishableGood : PhysicalGood, IDiscountable
{
    // private field
    private int _shelfLifeDays;

    // public constant
    public const decimal SurchargeFee = 0.40m;

    // public read-only properties
    public int ShelfLifeDays
    {
        get { return _shelfLifeDays; }
    }

    public bool IsOnSale
    {
        get { return ShelfLifeDays <= 3; }
    }

    // constructor
    public PerishableGood(
        string sku,
        string name,
        decimal unitPrice,
        int quantityOnHand,
        double weightPounds,
        int shelfLifeDays)
        : base(sku, name, unitPrice, quantityOnHand, weightPounds)
    {
        _shelfLifeDays = shelfLifeDays;
    }

    // methods
    public override string Category()
    {
        return "Perishable";
    }

    public override decimal HandlingFee()
    {
        return ShippingCost() + SurchargeFee;
    }

    public decimal SalePrice()
    {
        if (IsOnSale)
        {
            return UnitPrice * 0.70m;
        }

        return UnitPrice;
    }

    public override string Describe()
    {
        return base.Describe() + String.Format(", {0} days left", ShelfLifeDays);
    }
}