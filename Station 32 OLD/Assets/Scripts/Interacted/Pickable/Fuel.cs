
public class Fuel : Interacted
{
    protected override void Use()
    {
        Player.Instance.FuelCount++;
        ItemsUI.Instance.ChangeFuelCount();

        Destroy(gameObject);
    }
}
