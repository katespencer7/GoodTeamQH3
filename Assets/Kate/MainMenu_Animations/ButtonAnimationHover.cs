using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimatorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    void Start()
    {
            animator = GetComponent<Animator>();
            animator.SetBool("Hovered", false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Hovered", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Hovered", false);
    }
}