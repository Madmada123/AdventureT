using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] private string musicName = "MenuTheme";

    void Start()
    {
        // Включаем музыку, если её нет
        if (AudioManager.Instance != null)
        {
            var sound = AudioManager.Instance.sounds.Find(s => s.name == musicName);
            if (sound != null && !sound.source.isPlaying)
            {
                AudioManager.Instance.Play(musicName);
            }

            // Подписываемся на смену сцены, чтобы вовремя вырубить трек
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.LogWarning("AudioManager не найден в сцене!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Если новая сцена не главное меню — выключаем музыку
        if (scene.name != "MainMenu") // ⚠️ поменяй на точное имя твоей сцены
        {
            AudioManager.Instance.Stop(musicName);
            SceneManager.sceneLoaded -= OnSceneLoaded; // отписываемся, чтобы не засорять события
            Destroy(gameObject); // уничтожаем объект MenuMusic, чтобы не болтался
        }
    }
}
