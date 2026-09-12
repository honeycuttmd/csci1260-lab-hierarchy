namespace HierarchyLab;

// manages the collection of stock items and produces inventory reports
public class Shop : IReportable
{
    // private fields
    private string _name;
    private List<StockItem> _items;

    // public read-only properties
    public string Name
    {
        get { return _name; }
    }

    public int Count
    {
        get { return _items.Count; }
    }

    // constructor
    public Shop(string name)
    {
        _name = name;
        _items = new List<StockItem>();
    }

    // methods
    public StockItem Find(string sku)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Sku == sku)
            {
                return _items[i];
            }
        }

        return null;
    }

    public bool Add(StockItem item)
    {
        if (item == null || Find(item.Sku) != null)
        {
            return false;
        }

        _items.Add(item);
        return true;
    }

    public decimal TotalValue()
    {
        decimal total = 0m;

        for (int i = 0; i < _items.Count; i++)
        {
            total += _items[i].ExtendedValue();
        }

        return total;
    }

    public decimal SaleValue()
    {
        decimal total = 0m;

        for (int i = 0; i < _items.Count; i++)
        {
            StockItem item = _items[i];

            if (item is IDiscountable d && d.IsOnSale)
            {
                total += (d.SalePrice() + item.HandlingFee()) * item.QuantityOnHand;
            }
            else
            {
                total += item.ExtendedValue();
            }
        }

        return total;
    }

    public int SignedCount()
    {
        int count = 0;

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] is IDiscountable)
            {
                count++;
            }
        }

        return count;
    }

    public int OnSaleCount()
    {
        int count = 0;

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] is IDiscountable d && d.IsOnSale)
            {
                count++;
            }
        }

        return count;
    }

    public void SortByValue()
    {
        for (int i = 0; i < _items.Count - 1; i++)
        {
            int best = i;

            for (int j = i + 1; j < _items.Count; j++)
            {
                if (Beats(_items[j], _items[best]))
                {
                    best = j;
                }
            }

            if (best != i)
            {
                StockItem hold = _items[i];
                _items[i] = _items[best];
                _items[best] = hold;
            }
        }
    }

    private static bool Beats(StockItem a, StockItem b)
    {
        if (a.ExtendedValue() != b.ExtendedValue())
        {
            return a.ExtendedValue() > b.ExtendedValue();
        }
        
        return string.Compare(a.Name, b.Name, StringComparison.Ordinal) < 0;
    }

    public string ReportLine()
    {
        return String.Format(
            "{0}: {1} items, ${2:N2} on hand",
            Name,
            Count,
            TotalValue()
        );
    }

    public void PrintReport()
    {
        Console.WriteLine(new string('=', 60));
        Console.WriteLine($"  {Name.ToUpper()} : INVENTORY REPORT");
        Console.WriteLine(new string('=', 60));

        Console.WriteLine(String.Format(
            " {0,-7} {1,-21} {2,-10} {3,4} {4,11}",
            "SKU",
            "ITEM",
            "CATEGORY",
            "QTY",
            "VALUE"
        ));

        Console.WriteLine(new string('-', 60));

        for (int i = 0; i < _items.Count; i++)
        {
            Console.WriteLine(_items[i].ReportLine());
        }

        Console.WriteLine(new string('-', 60));

        Console.WriteLine(String.Format(
            " {0,-46} {1,11}",
            "Records on file:",
            Count
        ));

        Console.WriteLine(String.Format(
            " {0,-46}${1,11:N2}",
            "Total value on hand:",
            TotalValue()
        ));

        Console.WriteLine(String.Format(
            " {0,-46}${1,11:N2}",
            "Value if every sale price were taken:",
            SaleValue()
        ));

        Console.WriteLine(new string('=', 60));
    }
}