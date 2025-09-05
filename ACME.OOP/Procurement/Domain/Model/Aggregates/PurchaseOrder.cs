using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents a purchase order made to a supplier, including order details and associated items.   
/// </summary>
/// <param name="orderNumber">The <see cref="OrderNumber"/> of the Order being made.</param>
/// <param name="supplierId">The <see cref="SupplierId"/> of the supplier.</param>
/// <param name="orderDate">The date in which the order is being made.</param>
/// <param name="currency">The currency code(ISO 4217 format).</param>

/// <exception cref="ArgumentNullException">Thrown when <paramref name="orderNumber"/>, <paramref name="supplierId"/>, or <paramref name="currency"/> is null or empty.</exception>
/// <exception cref="ArgumentException">Thrown when <paramref name="currency"/> is not a valid 3-letter ISO code.</exception>

public class PurchaseOrder(string orderNumber, SupplierId supplierId, DateTime orderDate, string currency)
{
    // The list is initialized to an empty list to avoid null reference issues.
    // Items can be added through methods that enforce business rules.
    private List<PurchaseOrderItem> _items = new();
    
    public string OrderNumber { get; } = orderNumber ?? throw new ArgumentNullException(nameof(orderNumber));
    public SupplierId SupplierId { get; } = supplierId ?? throw new ArgumentNullException(nameof(supplierId));
    public DateTime OrderDate { get; } = orderDate;
    public string Currency { get; } = string.IsNullOrWhiteSpace(currency) || currency.Length != 3
        ? throw new ArgumentNullException(nameof(currency))
        : currency;
    
    /// <summary>
    /// Gets a read-only list of <see cref="PurchaseOrderItem"/> associated with this purchase order.
    /// </summary>
    public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Adds a <see cref="PurchaseOrderItem"/> to the purchase order.
    /// </summary>
    /// <param name="productId">The <see cref="ProductId"/> of the product being ordered.</param>
    /// <param name="quantity">The quantity of the product being ordered.</param>
    /// <param name="unitPriceAmount">The unit price of the product in the order's currency.</param>
    
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="productId"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="quantity"/> or <paramref name="unitPriceAmount"/> is less than or equal to zero.</exception>
    public void AddItem(ProductId productId, int quantity, decimal unitPriceAmount)
    {
        ArgumentNullException.ThrowIfNull(productId);
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        if (unitPriceAmount <= 0) throw new ArgumentOutOfRangeException(nameof(unitPriceAmount));
        
        var unitPrice = new Money(unitPriceAmount, Currency);
        var item = new PurchaseOrderItem(productId, quantity, unitPrice);
        _items.Add(item);
    }
    
    /// <summary>
    /// Calculates the total amount of the purchase order by summing the total of each item.
    /// </summary>
    /// <returns>A <see cref="Money"/> object representing the total amount of the order in the order's currency.</returns>
    public Money CalculateOrderTotal()
    {
        var totalAmount = _items.Sum(item => item.CalculateItemTotal().Amount);
        return new Money(totalAmount, Currency);
    }
}