using EventSourcingDemo.Aggregates.Product;
using EventSourcingDemo.Events.Product;
using EventSourcingDemo.Repositories;
using EventSourcingDemo.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventSourcingDemo;

class Program
{
    static async Task Main(string[] args)
    {
        //TestBankAccount();
        //TestProduct();
        await TestProductCommands();
    }

    private static async Task TestProductCommands()
    {
        var services = new ServiceCollection();

        // Register MediatR (scan current assembly for handlers)
        services.AddMediatR(Assembly.GetExecutingAssembly());

        // Register your domain services, event store, etc.
        services.AddSingleton<IProductService, ProductService>();
        services.AddSingleton<IProductEventStore, ProductEventStore>();
        services.AddSingleton<IProductRepository, ProductJsonRepository>();

        var serviceProvider = services.BuildServiceProvider();

        var productService = serviceProvider.GetRequiredService<IProductService>();

        // var productId = Guid.NewGuid();

        // await productService.CreateAsync(productId, "Dave Murray Stratocaster", "Fender");

        // await productService.ChangeModelAsync(productId, "Dave Murray Stratocaster");

        // var repo = new ProductJsonRepository("data/products.json");

        // Read
        var allProducts = await productService.GetAllAsync();
        foreach (var (p, index) in allProducts.Select((value, i) => (value, i)))
        {
            Console.WriteLine($"{index + 1}: {p.Brand} - {p.Model} (${p.Price})");
        }

        // Save
        var productId = Guid.NewGuid();
        await productService.CreateAsync(productId, "Dave Murray Stratocaster", "Fender", 1200);
        
        Product product = await productService.GetAsync(productId);
        Console.WriteLine($"{product.Brand} - {product.Model} (${product.Price})");

        await productService.ChangePriceAsync(productId, 1000);

        product = await productService.GetAsync(productId);

        Console.WriteLine($"{product.Brand} - {product.Model} (${product.Price})");
    }

    // private static void TestProduct()
    // {
    //     var store = new ProductEventStore();

    //     var productId = Guid.NewGuid();
    //     var product = new Product();
    //     product.Create(productId, "RG 1527", "Ibanez");
    //     product.ChangePrice(1000);
    //     store.Save(productId, product.GetUncommittedChanges());
    //     product.MarkChangesAsCommitted();
    //     Console.WriteLine($"Details of product: model {product.Model}, brand {product.Brand}, price {product.Price}");
    //     product.ChangeModel("RG1527");
    //     store.Save(productId, product.GetUncommittedChanges());
    //     product.MarkChangesAsCommitted();
    //     Console.WriteLine($"Details of product: model {product.Model}, brand {product.Brand}, price {product.Price}");

    //     var snapshot = new ProductSnapshot
    //     {
    //         Id = product.Id,
    //         Version = product.Version,
    //         Model = product.Model,
    //         Brand = product.Brand,
    //         Price = product.Price
    //     };

    //     store.SaveSnapshot(snapshot);

    //     var restored = ProductFactory.Restore(store, product.Id);

    //     Console.WriteLine($"Details of restored product: model {restored.Model}, brand {restored.Brand}, price {restored.Price}");

    //     product.ChangeModel("RG 1527");
    //     product.ChangeBrand("IBANEZ");

    //     store.Save(productId, product.GetUncommittedChanges());
    //     product.MarkChangesAsCommitted();

    //     restored = ProductFactory.Restore(store, product.Id);

    //     Console.WriteLine($"Details of restored product: model {restored.Model}, brand {restored.Brand}, price {restored.Price}");
    // }

    // private static void TestBankAccount()
    // {
    //     var store = new BankAccountEventStore();

    //     var accountId = Guid.NewGuid();
    //     var account = new BankAccount();
    //     account.Create(accountId, "Matheus");
    //     account.Deposit(100);
    //     account.Withdraw(40);

    //     store.Save(accountId, account.GetUncommittedChanges());
    //     account.MarkChangesAsCommitted();

    //     var snapshot = new BankAccountSnapshot
    //     {
    //         Id = accountId,
    //         Version = account.Version,
    //         Owner = account.Owner,
    //         Balance = account.Balance
    //     };
    //     store.SaveSnapshot(snapshot);

    //     Console.WriteLine($"Original Account Balance: {account.Balance}");

    //     var restored = BankAccountFactory.Restore(store, accountId);
    //     Console.WriteLine($"Restored from snapshot Balance: {restored.Balance}");

    //     account.Deposit(100);
    //     store.Save(accountId, account.GetUncommittedChanges());

    //     Console.WriteLine($"Original Account Balance: {account.Balance}");

    //     restored = BankAccountFactory.Restore(store, accountId);
    //     Console.WriteLine($"Restored from snapshot Balance: {restored.Balance}");
    // }
}
