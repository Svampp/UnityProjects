using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;  // Array of sound objects to hold all sounds

    public static AudioManager instance;  // Singleton instance of AudioManager

    private void Awake()
    {
        // Ensures only one instance of AudioManager exists
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
            return;
        }

        DontDestroyOnLoad(gameObject);  // Keep this object across scenes

        // Initialize each sound in the sounds array
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();  // Add AudioSource to game object
            s.source.clip = s.clip;  // Assign the audio clip
            s.source.volume = s.volume;  // Set the volume
            s.source.pitch = s.pitch;  // Set the pitch
            s.source.loop = s.loop;  // Set looping behavior
        }
    }

    public void Start()
    {
        Play("Background");  // Start playing background music on start
    }

    // Method to play a sound by its name
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);  // Find the sound by name
        if (s == null)
            return;  // If no sound is found, do nothing

        s.source.Play();  // Play the sound
    }

    // Method to stop a sound by its name
    public void StopPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);  // Find the sound by name
        if (s == null)
            return;  // If no sound is found, do nothing

        s.source.Stop();  // Stop the sound
    }
}
