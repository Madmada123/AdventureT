using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PrinceCommentUI : MonoBehaviour
{
    [Header("UI элементы")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text textMesh;

    [Header("Настройки текста")]
    [SerializeField, TextArea(2, 5)] private string[] phrases; // фразы прямо в инспекторе
    [SerializeField] private float typeSpeed = 0.04f; // скорость печати

    [Header("Настройки анимации")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float visibleDuration = 3f;
    [SerializeField] private float delayBetweenPhrases = 5f;

    private CanvasGroup canvasGroup;
    private int currentPhrase = 0;

    private void Awake()
    {
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private void Start()
    {
        if (phrases.Length > 0)
            StartCoroutine(ShowPhrasesLoop());
    }

    private IEnumerator ShowPhrasesLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenPhrases);

            string phrase = phrases[currentPhrase];
            yield return StartCoroutine(ShowCommentRoutine(phrase));

            currentPhrase = (currentPhrase + 1) % phrases.Length;
        }
    }

    private IEnumerator ShowCommentRoutine(string message)
    {
        textMesh.text = "";
        yield return StartCoroutine(FadeCanvas(1f)); // появление

        foreach (char c in message)
        {
            textMesh.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        yield return new WaitForSeconds(visibleDuration);
        yield return StartCoroutine(FadeCanvas(0f)); // исчезновение
    }

    private IEnumerator FadeCanvas(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
