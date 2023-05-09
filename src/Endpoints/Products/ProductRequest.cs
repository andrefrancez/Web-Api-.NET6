namespace WebApiUdemy.Endpoints.Products;

public record ProductRequest(Guid Id, string Name, Guid CategoryId, string Description, bool HasStock, decimal Price, bool Active);
