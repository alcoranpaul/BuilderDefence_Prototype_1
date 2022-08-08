using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }
    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;
    private float volume = 0.5f;

    public enum Sound {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver
    }
    private void Awake() {
        Instance = this;
        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        audioSource = GetComponent<AudioSource>();

        foreach (Sound sound in System.Enum.GetValues(typeof(Sound))) {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }


    }

    public void PlaySound(Sound sound) {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound], volume);
    }

    public void IncreaseVolume() {
        volume += 0.1f;
        volume = Mathf.Clamp01(volume);
    }
    public void DecreaseVolume() {
        volume -= 0.1f;
        volume = Mathf.Clamp01(volume);
    }

    public float GetVolume() {
        return volume;
    }
}
