namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    
    /// <summary>
    /// Creates a new instance of <see cref ="Money"/>.
    /// </summary>
    /// <param name="amount">The monetary Amount.</param>
    /// <param name="currency">The currency code(ISO 4217 format).</param>
    /// <exception cref="ArgumentException">Thrown when the currency code is invalid.</exception>
    
    // Constructor
    public Money(decimal amount, string currency)
    {
        // Basic validation for currency code
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
        {
            throw new ArgumentException("Currency must be a 3-letter ISO code.", nameof(currency));
        }
        Amount = amount;
        Currency = currency;
    }
    
    /// <summary>
    /// Returns a string representation of the Money object.
    /// </summary>
    /// <returns> A string in the format "Amount Currency"</returns>
    
    // Override ToString for better readability with $"{}"
    public override string ToString() => $"{Amount} {Currency}";
    
    
};