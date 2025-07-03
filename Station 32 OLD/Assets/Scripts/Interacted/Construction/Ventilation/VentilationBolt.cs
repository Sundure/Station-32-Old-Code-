using UnityEngine;

public class VentilationBolt : Interacted
{
    [SerializeField] private Ventilation _ventilation;

    private void Awake()
    {
        _ventilation.BoltsCount++;
    }

    protected override void Use()
    {
        _ventilation.UnscrewBolt();

        Destroy(gameObject);
    }

    public override bool InteractCondition()
    {
        if (Player.Instance.Screwdriver == true)
            return true;

        return false;
    }
}
