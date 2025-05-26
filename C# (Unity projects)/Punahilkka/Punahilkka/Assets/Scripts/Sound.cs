using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Represents a sound effect or music track, storing its properties
/// and associated AudioSource.
/// </summary>
[System.Serializable]
public class Sound
{
    public AudioClip clip; // The audio clip to play
    public string name; // Unique name for the sound

    [Range(0f, 1f)]
    public float volume; // Volume of the sound
    [Range(0.1f, 3f)]
    public float pitch; // Pitch of the sound

    public bool loop; // Whether the sound should loop

    [HideInInspector]
    public AudioSource source; // AudioSource component for playback
}
