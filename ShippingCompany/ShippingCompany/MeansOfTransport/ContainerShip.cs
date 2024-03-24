namespace ShippingCompany.Containers;

public class ContainerShip
{
    public List<BaseContainer> Containers { get; private set; }
    public double MaxVelocity { get; }
    public int MaxContainers { get; }
    public double MaxLoadWeight { get; }
    
    public ContainerShip(double maxVelocity, int maxContainers, double maxLoadWeight)
    {
        MaxVelocity = maxVelocity;
        MaxContainers = maxContainers;
        MaxLoadWeight = maxLoadWeight;
        Containers = new List<BaseContainer>();
    }
    
    public void LoadContainer(BaseContainer container)
    {
        if (Containers.Count >= MaxContainers) throw new OverfillException($"{container.SerialNumber} => " +
                                                                           $"Nieudany załadunek. Statek jest pełny!");
        
        if (container.LoadWeight + Containers.Sum(c => c.LoadWeight) > MaxLoadWeight) 
            throw new OverfillException($"{container.SerialNumber} => " +
                                        $"Nieudany załadunek. Przekroczono ładowność statku!");
        
        Containers.Add(container);
    }
    public void LoadContainer(List<BaseContainer> containers)
    {
        foreach (var container in containers)
            try
            {
                LoadContainer(container);
            }
            catch (OverfillException e)
            {
                Console.WriteLine(e);
            }
        
    }
    public void UnloadContainer(BaseContainer container)
    {
        Containers.Remove(container);
    }
    public void ReplaceContainer(BaseContainer oldContainer, BaseContainer newContainer)
    {
        UnloadContainer(oldContainer);
        LoadContainer(newContainer);
    }
    public void MoveContainerToShip(BaseContainer container, ContainerShip ship)
    {
        try
        {
            ship.LoadContainer(container);
        }
        catch (OverfillException e)
        {
            Console.WriteLine(e);
            return;
        }
        UnloadContainer(container);
    }
    
    public override string ToString()
    {
        return $"Statek kontenerowy\n" +
               $"Prędkość maksymalna: {MaxVelocity} kt\n" +
               $"Maksymalna ilość kontenerów: {MaxContainers}\n" +
               $"Maksymalna ładowność: {MaxLoadWeight} ton\n" +
               $"Aktualna ładowność: {Containers.Count} kontenerów\n" +
               $"Aktualna waga ładunku: {Containers.Sum(c => c.LoadWeight)} ton";
    }
}