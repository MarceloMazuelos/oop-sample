// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;
using ACME.OOP.Procurement.Domain.Model.Aggregates;
using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.Aggregates;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

var supplierAddress = new Address(street: "123 Main St", number: "Apt 4B", city: "Anytown", stateOrRegion: "CA", postalCode: "12345", country: "USA");

var supplier = new Supplier("SUP123", "Best Supplies Inc.", supplierAddress);

var purchaseOrder = new PurchaseOrder("PO12345", new SupplierId(supplier.Identifier), DateTime.Now, "USD");

purchaseOrder.AddItem(ProductId.NewId(), 10, 15.00m);
purchaseOrder.AddItem(ProductId.NewId(), 5, 25.00m);
Console.WriteLine($"order total: {purchaseOrder.CalculateOrderTotal()}");