namespace HierarchyLab;

// runs the River City Supply inventory program
public class Program
{
    static void Show(IReportable r)
    {
        Console.WriteLine(r.ReportLine());
    }

    public static void Main()
    {
        Shop shop = new Shop("River City Supply");

        Console.Write("Opening catalog: ");
        Show(shop);

        Console.WriteLine();
        Console.WriteLine("Loading five records...");

        PerishableGood honey = new PerishableGood("HON01", "Wildflower honey", 8.00m, 12, 1.5, 2);

        DurableGood kettle = new DurableGood("KTL11", "Cast iron kettle", 24.00m, 5, 4.0, 24);

        PerishableGood cheddar = new PerishableGood("CHZ07", "Farm cheddar wedge", 3.50m, 40, 0.5, 9);

        ServiceItem sharpening = new ServiceItem("SRV20", "Knife sharpening", 60.00m, 2, 2.5);

        ServiceItem wrapping = new ServiceItem("SRV21", "Gift wrapping", 15.00m, 3, 1.0);

        shop.Add(honey);
        shop.Add(kettle);
        shop.Add(cheddar);
        shop.Add(sharpening);
        shop.Add(wrapping);

        PerishableGood duplicateHoney = new PerishableGood("HON01", "Duplicate honey", 5.00m, 1, 1.0, 1);

        if (!shop.Add(duplicateHoney))
        {
            Console.WriteLine(" REJECTED: duplicate SKU HON01");
        }

        Console.WriteLine();
        Console.WriteLine("Recording four movements...");

        int movementsAccepted = 0;

        StockItem honeyItem = shop.Find("HON01");
        if (honeyItem != null && honeyItem.Receive(6))
        {
            movementsAccepted++;
        }

        StockItem kettleItem = shop.Find("KTL11");
        if (kettleItem != null && kettleItem.Release(2))
        {
            movementsAccepted++;
        }

        kettleItem = shop.Find("KTL11");
        if (kettleItem != null && !kettleItem.Release(99))
        {
            Console.WriteLine(" REJECTED: release of 99 from KTL11");
        }

        StockItem cheddarItem = shop.Find("CHZ07");
        if (cheddarItem != null && !cheddarItem.Receive(-5))
        {
            Console.WriteLine(" REJECTED: receive of -5 into CHZ07");
        }

        Console.WriteLine();
        Console.WriteLine($"Records accepted: {shop.Count}");
        Console.WriteLine($"Movements accepted: {movementsAccepted}");
        Console.WriteLine();

        Console.WriteLine($"Top record: {cheddar}");
        Console.WriteLine();

        shop.SortByValue();
        shop.PrintReport();

        Console.WriteLine();
        Console.WriteLine("Contract check");

        Console.WriteLine(String.Format(
            " {0,-46} {1,11}",
            "Records signing IDiscountable:",
            shop.SignedCount()
        ));

        Console.WriteLine(String.Format(
            " {0,-46} {1,11}",
            "Records on sale right now:",
            shop.OnSaleCount()
        ));

        Console.WriteLine(String.Format(
            " {0,-46}${1,11:N2}",
            "Difference between the two totals:",
            shop.TotalValue() - shop.SaleValue()
        ));

        Console.WriteLine();
        Console.WriteLine("Composition check");

        Console.WriteLine(String.Format(
            " {0,-46} {1,11}",
            "Movements recorded by HON01:",
            honey.MoveCount
        ));

        if (honey.MoveCount > 0)
        {
            Console.WriteLine(honey.MovementLines());
        }

        Console.WriteLine(String.Format(
            " {0,-46} {1,11}",
            "Movements recorded by KTL11:",
            kettle.MoveCount
        ));

        if (kettle.MoveCount > 0)
        {
            Console.WriteLine(kettle.MovementLines());
        }

        Console.WriteLine(String.Format(
            " {0,-46} {1,11}",
            "Movements recorded by CHZ07:",
            cheddar.MoveCount
        ));

        if (cheddar.MoveCount > 0)
        {
            Console.WriteLine(cheddar.MovementLines());
        }

        // StockItem bad = new StockItem("X", "Nope", 1m, 1);
        // CS0144: Cannot create an instance of the abstract type or interface 'StockItem'
    }
}