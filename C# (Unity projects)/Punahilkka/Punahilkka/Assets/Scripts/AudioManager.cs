using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

/// <summary>
/// This script manages all audio in the game, including playing, stopping,
/// and setting up audio sources for different sounds.
/// </summary>
public class AudioManager : MonoBehaviour
{
    // Array of sounds to manage
    public Sound[] sounds;

    // Singleton instance of AudioManager
    public static AudioManager instance;

    /// <summary>
    /// Initializes the AudioManager as a singleton and sets up audio sources.
    /// </summary>
    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Create AudioSource components for each sound
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
        Play("Background");
    }

    /// <summary>
    /// Plays a sound by name.
    /// </summary>
    /// <param name="name">The name of the sound to play.</param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.Play();
    }

    /// <summary>
    /// Stops a sound by name.
    /// </summary>
    /// <param name="name">The name of the sound to stop.</param>
    public void StopPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.Stop();
    }
}
