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

    /// <summary>
    /// If Return True - Player Can Interact With This Object
    /// </summary>
    /// <returns></returns>
    public virtual bool InteractCondition()
    {
        return true;
    }

    /// <summary>
    /// Called When Interact Condition Is True
    /// </summary>
    protected abstract void Use();
}
