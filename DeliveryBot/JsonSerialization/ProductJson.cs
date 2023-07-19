using Delivery.Data.Entities;
using JFA.DependencyInjection;
using Newtonsoft.Json;

namespace DeliveryBot.JsonSerialization;

[Scoped]    
public class ProductJson
{
    public List<Product> Products;

    public ProductJson()
    {
        Products = new List<Product>();
        Read();
    }

    void Read()
    {
        var file = File.ReadAllText("C://Users//Muhammadjon//MarketPlace//DeliveryBot//DeliveryBot//wwwroot//Rasmlar//passbook.json");
        Products = JsonConvert.DeserializeObject<List<Product>>(file)!;
    }
}