using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectButton : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    ISelectHandler,       IDeselectHandler
{
    [SerializeField] private string characterName;

    private MainMenuManager menuManager;

    private void Awake()
    {
        menuManager = FindAnyObjectByType<MainMenuManager>();
    }

    // Mouse
    public void OnPointerEnter(PointerEventData _) => Show();
    public void OnPointerExit(PointerEventData _)  => Hide();

    // Controller
    public void OnSelect(BaseEventData _)   => Show();
    public void OnDeselect(BaseEventData _) => Hide();

    private void Show() => menuManager?.SetCharacterPortraitActive(characterName);
    private void Hide() => menuManager?.ClearCharacterPortraits();
}