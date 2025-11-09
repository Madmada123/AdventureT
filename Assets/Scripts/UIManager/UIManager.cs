using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Синглтон для удобного доступа
    public static UIManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Метод для перехода на другую сцену
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Метод открытия UI окна (панели)
    public void OpenWindow(GameObject window)
    {
        window.SetActive(true);
    }

    // Метод закрытия UI окна (панели)
    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }

    // Универсальный метод переключения окон (закрыть все, открыть выбранное)
    public void SwitchWindow(GameObject windowToOpen, GameObject[] allWindows)
    {
        foreach (var window in allWindows)
            window.SetActive(false);

        windowToOpen.SetActive(true);
    }
}
