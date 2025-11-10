using UnityEngine;
using TMPro;
using System.Collections; // <--- Добавлено для работы с корутинами

public class BossTextDisplay : MonoBehaviour
{
    [Header("Текст и позиции")]
    [SerializeField] private GameObject textPrefab; // Привяжи prefab с TextMeshPro
    [SerializeField] private Transform spawnPoint; // Точка появления текста
    [SerializeField] private string[] phrases; // Массив фраз

    [Header("Настройки поведения")]
    [SerializeField] private float lifeTime = 2f; // Время жизни текста
    [SerializeField] private float floatSpeed = 0.5f; // Скорость поднятия
    [SerializeField] private float displayInterval = 5f; // Интервал между появлениями текста (в секундах)

    private void Start()
    {
        Debug.Log("BossTextDisplay: Start() - Запуск цикла отображения текста.");
        // Запускаем корутину, которая будет циклически вызывать ShowRandomText
        StartCoroutine(TextLoopRoutine());
    }

    /// <summary>
    /// Корутина, обеспечивающая циклическое отображение текста с заданным интервалом.
    /// </summary>
    private IEnumerator TextLoopRoutine()
    {
        // Выполняем бесконечный цикл
        while (true)
        {
            // 1. Ждем заданный интервал, прежде чем показать следующий текст
            yield return new WaitForSeconds(displayInterval);

            // 2. Показываем текст
            ShowRandomText();
        }
    }

    public void ShowRandomText()
    {
        if (textPrefab == null || spawnPoint == null || phrases.Length == 0)
        {
            Debug.LogWarning("BossTextDisplay: Нет prefab, точки спавна или фраз. Проверьте настройки в Инспекторе.");
            return;
        }

        // Выбираем случайную фразу
        string phrase = phrases[Random.Range(0, phrases.Length)];
        GameObject textObj = Instantiate(textPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log($"BossTextDisplay: Создан текст '{phrase}' на {spawnPoint.position}");

        // Настраиваем TextMeshPro
        TextMeshPro tmp = textObj.GetComponent<TextMeshPro>();
        if (tmp != null)
            tmp.text = phrase;

        // Добавляем компонент для поднятия и уничтожения
        FloatingTextBehavior ftb = textObj.AddComponent<FloatingTextBehavior>();
        ftb.lifeTime = lifeTime;
        ftb.floatSpeed = floatSpeed;
    }
}

// Компонент для текста (остался без изменений, он корректен)
public class FloatingTextBehavior : MonoBehaviour
{
    // [HideInInspector] позволяет не отображать поля в Инспекторе, 
    // так как они настраиваются программно при создании.
    [HideInInspector] public float lifeTime = 2f;
    [HideInInspector] public float floatSpeed = 0.5f;

    private void Start()
    {
        Debug.Log($"FloatingText: '{name}' стартовал");
        // Запускаем автоматическое уничтожение объекта через 'lifeTime' секунд
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Двигаем объект вверх с учетом времени кадра (Time.deltaTime)
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }
}