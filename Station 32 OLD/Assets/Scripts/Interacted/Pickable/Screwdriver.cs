
public class Screwdriver : Interacted
{
    protected override void Use()
    {
        Player.Instance.Screwdriver = true;

        Destroy(gameObject);
    }
}

