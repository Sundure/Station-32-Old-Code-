using UnityEngine;

public abstract class Interacted : MonoBehaviour
{
    [SerializeField] private bool _holdButtonToUse;
    public bool HoldButtonToUse { get { return _holdButtonToUse; } }

    public void Interact()
    {
        if (InteractCondition())
            Use();
    }
    
    public virtual bool InteractCondition()
    {
        return true;
    }

    protected abstract void Use();
}
