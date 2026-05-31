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
        StartCoroutine(InitNextFrame());
    }

    private System.Collections.IEnumerator InitNextFrame()
    {
        // Wait until EventSystem and all buttons are guaranteed ready
        yield return null;
        yield return null;

        if (buttons != null && buttons.Length > 0)
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

        if (buttons != null && buttons.Length > 0)
            SelectButton(0);
    }

    void SelectButton(int index)
    {
        if (buttons == null || index < 0 || index >= buttons.Length) return;

        GameObject target = buttons[index];
        if (target == null || !target.activeInHierarchy) return;

        // Deselect previous
        if (currentAnimator != null)
            currentAnimator.SetBool("Hovered", false);

        currentIndex = index;
        currentAnimator = target.GetComponent<Animator>();

        if (currentAnimator != null)
            currentAnimator.SetBool("Hovered", true);

        // Only touch the EventSystem if it actually exists
        if (EventSystem.current == null) return;

        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(target);
    }

    void ClickButton(int index)
    {
        if (buttons == null || index < 0 || index >= buttons.Length) return;
        buttons[index].GetComponent<Button>()?.onClick.Invoke();
    }
}