
public class Flare : Interacted
{
    protected override void Use()
    {
        Player.Instance.FlareCount++;

        ItemsUI.Instance.ChangeFlareCount();

        Destroy(gameObject);
    }
}
