using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

/// <summary>
/// Manages all audio playback in the game, including sound initialization and control.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; // Array of sound objects for audio management.

    // Singleton instance for global access.
    public static AudioManager instance;

    /// <summary>
    /// Sets up the AudioManager instance and initializes sound sources.
    /// </summary>
    private void Awake()
    {
        // Ensure a single instance of AudioManager exists.
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Prevent destruction on scene load.

        // Initialize audio sources for each sound.
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    /// <summary>
    /// Plays the background music at the start of the game.
    /// </summary>
    public void Start()
    {
        Play("Background"); // Default background audio.
    }

    /// <summary>
    /// Plays a sound by its name.
    /// </summary>
    /// <param name="name">The name of the sound to play.</param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // Find the sound by name.
        if (s == null) return; // Exit if the sound is not found.
        s.source.Play(); // Play the sound.
    }

    /// <summary>
    /// Stops playing a sound by its name.
    /// </summary>
    /// <param name="name">The name of the sound to stop.</param>
    public void StopPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // Find the sound by name.
        if (s == null) return; // Exit if the sound is not found.
        s.source.Stop(); // Stop the sound.
    }
}
