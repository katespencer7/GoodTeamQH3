using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject characterSelectPanel;
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;

    [Header("Portraits")]
    public GameObject InquisitorPortrait;
    public GameObject CountessPortrait;
    public GameObject AssassinPortrait;

    [SerializeField] private float fadeDuration = 0.3f;

private void Start()
{
    SetPanelAlpha(mainMenuPanel, 1f);
    SetPanelAlpha(characterSelectPanel, 0f);
    SetPanelAlpha(creditsPanel, 0f);

    SetPanelActive(characterSelectPanel, false);
    SetPanelActive(creditsPanel, false);

    // Set initial focus manually since we never transition to the main menu on startup.
    mainMenuPanel.GetComponent<PanelFocusOnShow>()?.Focus();
}

    public void LoadCharacterSelect() => StartCoroutine(TransitionTo(characterSelectPanel));
    public void LoadCredits()         => StartCoroutine(TransitionTo(creditsPanel));
    public void LoadMainMenu()        => StartCoroutine(TransitionTo(mainMenuPanel));

    private IEnumerator TransitionTo(GameObject targetPanel)
    {
        yield return StartCoroutine(FadeAllOut());

        SetPanelActive(targetPanel, true);
        yield return StartCoroutine(FadeIn(targetPanel));

        // Hand focus to the panel's default selected button once fully visible.
        targetPanel.GetComponent<PanelFocusOnShow>()?.Focus();
    }

    private IEnumerator FadeAllOut()
    {
        GameObject[] panels = { mainMenuPanel, characterSelectPanel, creditsPanel };

        float elapsed = 0f;
        float[] startAlphas = System.Array.ConvertAll(panels, p => GetPanelAlpha(p));

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            for (int i = 0; i < panels.Length; i++)
                SetPanelAlpha(panels[i], Mathf.Lerp(startAlphas[i], 0f, t));

            yield return null;
        }

        foreach (GameObject panel in panels)
        {
            SetPanelAlpha(panel, 0f);
            SetPanelActive(panel, false);
        }
    }

    private IEnumerator FadeIn(GameObject panel)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            SetPanelAlpha(panel, Mathf.Lerp(0f, 1f, elapsed / fadeDuration));
            yield return null;
        }

        SetPanelAlpha(panel, 1f);
    }

    public void SetCharacterPortraitActive(string characterName)
    {
        InquisitorPortrait.SetActive(characterName == "Inquisitor");
        CountessPortrait.SetActive(characterName == "Countess");
        AssassinPortrait.SetActive(characterName == "Assassin");
    }

    public void ClearCharacterPortraits()
    {
        InquisitorPortrait.SetActive(false);
        CountessPortrait.SetActive(false);
        AssassinPortrait.SetActive(false);
    }

    // CanvasGroup helpers
    private CanvasGroup GetCanvasGroup(GameObject panel)
        => panel.GetComponent<CanvasGroup>();

    private float GetPanelAlpha(GameObject panel)
        => GetCanvasGroup(panel)?.alpha ?? 1f;

    private void SetPanelAlpha(GameObject panel, float alpha)
    {
        CanvasGroup cg = GetCanvasGroup(panel);
        if (cg != null) cg.alpha = alpha;
    }

    private void SetPanelActive(GameObject panel, bool active)
    {
        CanvasGroup cg = GetCanvasGroup(panel);
        if (cg == null) return;

        cg.blocksRaycasts = active;
        cg.interactable   = active;
        panel.SetActive(active);
    }
}