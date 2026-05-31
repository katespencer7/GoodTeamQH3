using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerMenuInput : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;
    private int currentIndex = 0;
    private Animator currentAnimator;

    void Start()
    {
        // Delay so PanelFocusOnShow has time to run first
        StartCoroutine(InitNextFrame());
    }

    private System.Collections.IEnumerator InitNextFrame()
    {
        yield return null;
        SelectButton(0);
    }

void Update()
{
    var gamepad = Gamepad.current;
    if (gamepad == null) return;

    if (gamepad.dpad.down.wasPressedThisFrame || 
        gamepad.leftStick.down.wasPressedThisFrame)
    {
        currentIndex = (currentIndex + 1) % buttons.Length;
        SelectButton(currentIndex);
    }

    if (gamepad.dpad.up.wasPressedThisFrame || 
        gamepad.leftStick.up.wasPressedThisFrame)
    {
        currentIndex = (currentIndex - 1 + buttons.Length) % buttons.Length;
        SelectButton(currentIndex);
    }

    if (gamepad.buttonSouth.wasPressedThisFrame)
    {
        Debug.Log("South pressed — clicking: " + buttons[currentIndex].name);
        ClickButton(currentIndex);
    }
}

    public void SetButtons(GameObject[] newButtons)
    {
        buttons = newButtons;
        currentIndex = 0;
        SelectButton(0);
    }

    void SelectButton(int index)
    {
        if (currentAnimator != null)
            currentAnimator.SetBool("Hovered", false);

        currentIndex = index;
        currentAnimator = buttons[currentIndex].GetComponent<Animator>();

        if (currentAnimator != null)
            currentAnimator.SetBool("Hovered", true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[currentIndex]);
    }

    void ClickButton(int index)
    {
        buttons[index].GetComponent<Button>()?.onClick.Invoke();
    }
}