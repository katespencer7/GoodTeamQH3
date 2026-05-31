using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectButton : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    ISelectHandler,       IDeselectHandler
{
    [SerializeField] private GameObject portrait;
    [SerializeField] private float fadeDuration = 0.2f;

    private CanvasGroup portraitGroup;
    private Coroutine currentFade;

    private void Awake()
    {
        portraitGroup = portrait.GetComponent<CanvasGroup>();
        portraitGroup.alpha = 0f;
    }

    // Mouse
    public void OnPointerEnter(PointerEventData _) => SetFade(1f);
    public void OnPointerExit(PointerEventData _)  => SetFade(0f);

    // Controller
    public void OnSelect(BaseEventData _)   => SetFade(1f);
    public void OnDeselect(BaseEventData _) => SetFade(0f);

    private void SetFade(float target)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(Fade(target));
    }

    private IEnumerator Fade(float target)
    {
        float start   = portraitGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            portraitGroup.alpha = Mathf.Lerp(start, target, elapsed / fadeDuration);
            yield return null;
        }

        portraitGroup.alpha = target;
    }
}