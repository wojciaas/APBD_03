namespace ShippingCompany.Containers;

public class LiquidContainer : BaseContainer, IHazardNotifier
{
    private static int _number;
    private const string Type = "L";
    public bool Hazardous { get; private set; }

    public LiquidContainer(double maxCapacity, double deadWeight, double height, double depth, bool hazardous) : 
                        base(Type, ++_number, maxCapacity, deadWeight, height, depth)
    {
        Hazardous = hazardous;
    }

    public override void UnloadACargo()
    {
        Hazardous = false;
        base.UnloadACargo();
    }
    
    public void NotifyHazard()
    {
        Console.Error.WriteLine($"Kontener {SerialNumber} - błąd załadunku! " +
                          "Przekroczenie normy dla substancji " + (Hazardous ? "niebezpiecznej" : "nieszkodliwej.\n") +
                          "Normy dla substancji niebezpiecznej: 50% pojemności\n" +
                          "Normy dla substancji nieszkodliwej: 90% pojemności");
    }
    
    public override void LoadACargo(double cargoWeight)
    {
        if (
                (LoadWeight + cargoWeight > MaxCapacity * 0.5 && Hazardous) || 
                (LoadWeight + cargoWeight > MaxCapacity * 0.9 && Hazardous)
            )
            NotifyHazard();
        base.LoadACargo(cargoWeight);
    }
    
    public override string ToString()
    {
        return base.ToString() + $"Substancja: {(Hazardous ? "niebezpieczna" : "nieszkodliwa")}";
    }
}