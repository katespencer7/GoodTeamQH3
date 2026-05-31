using UnityEngine;
using UnityEngine.EventSystems;

public class PanelFocusOnShow : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelected;

    public void Focus()
    {
        if (defaultSelected == null) return;

        // Clear first to force a fresh selection event even if
        // the same button was selected before.
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultSelected);
    }
}