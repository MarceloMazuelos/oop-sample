namespace ACME.OOP.Procurement.Domain.Model.ValueObjects;

/// <summary>
/// Represents a unique identifier for a product in the Procurement bounded context.
/// </summary>

public record ProductId
{
    public Guid Id { get; init; }
    
    /// <summary>
    /// Creates a new instance of <see cref ="ProductId"/>.
    /// </summary>
    /// <param name="id"> The unique identifier for ProductId </param>
    /// <exception cref="ArgumentException">Thrown when the identifier is null or empty.</exception>
    
    //Constructor
    public ProductId(Guid id)
    {
        // Basic validation for id
        if (id == Guid.Empty)
            throw new ArgumentException("ProductId cannot be empty.", nameof(id));
        
        Id = id;
    }
    
    /// <summary>
    /// Generates a new unique ProductId.
    /// </summary>
    /// <returns>A new instance of ProductId</returns>
    public static ProductId NewId() => new (Guid.NewGuid());
    
    // Override ToString for better readability in logs and debugging
    public override string ToString() => Id.ToString();
};