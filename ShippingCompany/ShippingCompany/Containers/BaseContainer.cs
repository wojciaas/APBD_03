namespace ShippingCompany.Containers;

public abstract class BaseContainer
{
    protected double MaxCapacity { get; }
    private double _loadWeight;
    public double LoadWeight
    {
        get => _loadWeight;
        protected set
        {
            if (value > MaxCapacity)
            {
                throw new OverfillException("Przekroczono maksymalną pojemność kontenera!");
            }

            _loadWeight = value;
        }
    }
    protected double DeadWeight { get; }
    protected double Height { get; }
    protected double Depth { get; }
    public string SerialNumber { get; }
    
    private string? _cargoType;
    protected string? CargoType
    {
        get => _cargoType;
        set => _cargoType = value?.ToUpper();
    }
    
    protected BaseContainer(string type, int number, double maxCapacity, double deadWeight, double height, double depth)
    {
        MaxCapacity = maxCapacity;
        DeadWeight = deadWeight;
        Height = height;
        Depth = depth;
        SerialNumber = $"KON-{type}-{number}";
    }

    public virtual void UnloadACargo()
    {
        CargoType = null;
        LoadWeight = 0;
    }
    
    protected bool IsCargoTypeValid(string cargoType)
    {
        if (CargoType != null && CargoType.Equals(cargoType.ToUpper()))
        {
            Console.Error.WriteLine($"Konflikt ładunków! Kontener jest już załadowany ładunkiem {CargoType}");
            return false;
        }
        CargoType ??= cargoType;
        return true;
    }
    
    public virtual void LoadACargo(double cargoWeight)
    {
        try 
        {
            LoadWeight += cargoWeight;
        }
        catch (OverfillException e)
        {
            Console.Error.WriteLine(e.Message);
        }
    }
    
    public virtual void LoadACargo(string cargoType, double cargoWeight)
    {
        try 
        {
            LoadWeight += cargoWeight;
        }
        catch (OverfillException e)
        {
            Console.Error.WriteLine(e.Message);
        }
    }
    
    public override string ToString()
    {
        return $"Kontener {SerialNumber} - ładunek: {CargoType ?? "pusty"}, ładowność: {LoadWeight}kg/{MaxCapacity}kg, " +
               $"masa własna: {DeadWeight}kg, wysokość: {Height}cm, głębokość: {Depth}cm";
    }
}