using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Represents a sound effect with properties for playback, volume, pitch, and looping.
/// </summary>
[System.Serializable]
public class Sound
{
    public AudioClip clip; // The audio clip associated with this sound.
    public string name; // The name of the sound for identification.

    [Range(0f, 1f)]
    public float volume; // The volume of the sound (0.0 to 1.0).
    [Range(0.1f, 3f)]
    public float pitch; // The pitch of the sound (0.1 to 3.0).

    public bool loop; // Indicates whether the sound should loop.

    [HideInInspector]
    public AudioSource source; // The audio source used to play this sound (hidden in the inspector).
}
