# CSCI 1260 Lab - Hierarchy

## Student
Madison Honeycutt
Section 002

## Track
Track A: The Shop

## Unfinished
Nothing unfinished.

## How to Run
Open the project in Visual Studio and run the program. The console will display the River City Supply inventory results, including the accepted records and movements, sorted inventory report, contract check, and composition check.

## Design Questions

### 1. Why is PhysicalGood abstract even though it has real code?
PhysicalGood is abstract because it contains behavior that all physical items share, such as weight and shipping cost, but it still does not represent a complete type of item by itself. It does not provide its own Category() or HandlingFee(), so those details are left for the child classes to define. If PhysicalGood were made concrete, those abstract methods would also have to be given implementations just so someone could create a PhysicalGood object. That would force the class to invent a generic category and handling fee that may not make sense, so keeping it abstract prevents incomplete physical items from being created.

### 2. Explain the two diamonds in the program.
The relationship between Shop and StockItem is aggregation, shown with a hollow diamond. The StockItem objects are created in Program and then handed to Shop through Add(), so the items can exist independently of the Shop. The relationship between StockItem and StockMovement is composition, shown with a filled diamond. StockMovement objects are created only inside StockItem when Receive() or Release() succeeds, and their history belongs to that StockItem. If Add() were changed so Shop received the information and created the StockItem itself, the hollow diamond between Shop and StockItem would change to a filled diamond because Shop would now be responsible for creating the records. I think that would be a mistake because it would give Shop a responsibility it does not currently need and would make adding new record types harder.

### 3. What would change if a rental kind were added?
I would create a new RentalGood.cs file. RentalGood would inherit from PhysicalGood because it has bulk, implement IDiscountable because it can go on sale, and add the property and behavior needed for its deposit. The existing file I would edit is Program.cs so a RentalGood object could be created and added to the Shop. I would not need to edit Shop.cs because Shop already works with StockItem objects and does not need to know the exact child type. DurableGood does not implement IDiscountable, while RentalGood would. If IsOnSale had been declared on StockItem instead of in the IDiscountable contract, then every StockItem child would be forced to have sale behavior even when that behavior does not make sense for that class.