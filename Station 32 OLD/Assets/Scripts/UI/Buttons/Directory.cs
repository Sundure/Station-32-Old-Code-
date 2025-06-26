using UnityEngine;
using UnityEngine.EventSystems;

public class Directory : MonoBehaviour, IPointerClickHandler
{
    public Directory PreviousDirectory;
    public GameObject DirectoryContent;

    [SerializeField] private bool _clickable = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_clickable)
        {
            if (PausePanel.Instance.DirectoryManager.CurrentDirectory == this)
                return;

            PausePanel.Instance.DirectoryManager.ChangeDirectory(this);
        }
    }
}
