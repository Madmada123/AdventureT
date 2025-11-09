using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Audio; // пригодится, если позже захочешь использовать AudioMixer

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop;
        [HideInInspector] public AudioSource source;
    }

    public List<Sound> sounds;
    private float masterVolume = 1f; // 🔥 общий уровень громкости

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * masterVolume; // учтём общий уровень
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Звук не найден: " + name);
            return;
        }
        s.source.volume = s.volume * masterVolume; // 🔊 применим текущий masterVolume
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s != null)
            s.source.Stop();
    }

    // 🎚 Управление общей громкостью
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * masterVolume;
        }
    }

    // 📦 Чтобы сохранить громкость между сценами (по желанию)
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }

    private void OnEnable()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        SetMasterVolume(masterVolume);
    }
}
