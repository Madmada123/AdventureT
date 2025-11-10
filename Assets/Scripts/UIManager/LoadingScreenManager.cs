using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject[] loadingScreens;
    [SerializeField] private float minLoadingTime = 2f; // Минимальная длительность загрузки

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        foreach (var screen in loadingScreens)
            screen.SetActive(false);

        int randomIndex = Random.Range(0, loadingScreens.Length);
        loadingScreens[randomIndex].SetActive(true);
        Debug.Log($"🎨 Выбран экран: {randomIndex + 1} из {loadingScreens.Length}");

        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;

        while (!operation.isDone)
        {
            timer += Time.deltaTime;

            // Если прошло достаточно времени и сцена уже почти загружена — активируем
            if (operation.progress >= 0.9f && timer >= minLoadingTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
