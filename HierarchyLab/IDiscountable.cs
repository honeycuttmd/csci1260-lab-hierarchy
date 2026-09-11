namespace HierarchyLab;

// defines the discount contract for classes that can go on sale
public interface IDiscountable
{
    public bool IsOnSale { get; }

    public decimal SalePrice();
}