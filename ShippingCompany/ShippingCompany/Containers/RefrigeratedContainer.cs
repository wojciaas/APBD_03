namespace ShippingCompany.Containers;

public class RefrigeratedContainer : BaseContainer
{
    private static int _number;
    private const string Type = "C";
    public static readonly Dictionary<string, double> TemperatureLimits = new()
    {
        {"BANANA", 13.3},
        {"CHOCOLATE", 18},
        {"FISH", 2},
        {"MEAT", -15},
        {"ICE CREAM", -18},
        {"FROZEN PIZZA", -30},
        {"CHEESE", 7.2},
        {"SAUSAGES", 5},
        {"BUTTER", 20.5},
        {"EGGS", 19}
    };
    private double _temperature;

    public RefrigeratedContainer(double maxCapacity, double deadWeight, double height, double depth, double temperature) : 
            base(Type, ++_number, maxCapacity, deadWeight, height, depth)
    {
        _temperature = temperature;
    }
    
    public override void LoadACargo(string cargoType, double cargoWeight)
    {
        if (!IsCargoTypeValid(cargoType)) return;
        if (TemperatureLimits.ContainsKey(cargoType) && _temperature < TemperatureLimits[cargoType])
        {
            Console.Error.WriteLine($"Kontener {SerialNumber} - błąd załadunku! " +
                                    $"Temperatura za niska dla ładunku {cargoType}.\n" +
                                    $"Wymagana temperatura: {TemperatureLimits[cargoType]}°C\n" +
                                    $"Aktualna temperatura: {_temperature}°C");
            return;
        }
        base.LoadACargo(cargoType, cargoWeight);
    }

    public override string ToString()
    {
        return base.ToString() + $"Temperatura: {_temperature}°C";
    }
}