using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents an item within a purchase order, including product details, quantity, and pricing information.
/// </summary>
/// <param name="productId">The <see cref="ProductId"/> of the product being ordered. </param>
/// <param name="quantity">The quantity of the product being ordered. Must be greater than zero. </param>
/// <param name="unitPrice">The <see cref="Money"/> representing the unit price of the product. Cannot be null.</param>

/// <exception cref="ArgumentNullException">Thrown when <paramref name="productId"/> or <paramref name="unitPrice"/> is null.</exception>
/// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="quantity"/> is less than zero.</exception>
public class PurchaseOrderItem(ProductId productId, int quantity, Money unitPrice)
{
    public ProductId ProductId { get; } = productId ?? throw new ArgumentNullException(nameof(productId));

    public int Quantity { get; } = quantity > 0
        ? quantity
        : throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

    public Money UnitPrice { get; } = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));

    public Money CalculateItemTotal() => new(UnitPrice.Amount * Quantity, UnitPrice.Currency);

}