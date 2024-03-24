namespace ShippingCompany.Containers;

public class GasContainer : BaseContainer, IHazardNotifier
{
    private static int _number;
    private const string Type = "G";
    private readonly int _psi;
    public bool Hazardous { get; private set; }
    
    public GasContainer(double maxCapacity, double deadWeight, double height, double depth, int psi, bool hazardous) : 
            base(Type, ++_number, maxCapacity, deadWeight, height, depth)
    {
        Hazardous = hazardous;
        _psi = psi;
    }
    
    public void NotifyHazard()
    {
        Console.Error.WriteLine($"Kontener {SerialNumber} - błąd załadunku! " +
                                "Przekroczenie normy dla substancji gazowej.\n");
    }

    public override void LoadACargo(double cargoWeight)
    {
        try
        {
            LoadWeight += cargoWeight;
        }
        catch (OverfillException e)
        {
            Console.Error.WriteLine(e.Message);
            NotifyHazard();
        }
    }
    
    public override void UnloadACargo()
    {
        LoadWeight *= 0.05;
    }

    public override string ToString()
    {
        return base.ToString() + $"Ciśnienie: {_psi} PSI, Substancja: {(Hazardous ? "niebezpieczna" : "nieszkodliwa")}";
    }
}