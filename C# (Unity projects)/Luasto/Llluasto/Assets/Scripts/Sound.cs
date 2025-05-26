using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// A Serializable class to define properties for individual sounds in the game
[System.Serializable]
public class Sound
{
    // The audio clip that will be played
    public AudioClip clip;

    // A custom name for the sound (used for identification purposes)
    public string name;

    // Volume of the sound, ranging from 0 (mute) to 1 (full volume)
    [Range(0f, 1f)]
    public float volume;

    // Pitch of the sound, ranging from 0.1 (very slow) to 3 (very fast)
    [Range(0.1f, 3f)]
    public float pitch;

    // A flag to determine if the sound should loop indefinitely
    public bool loop;

    // A reference to the AudioSource that will play the sound (hidden in the inspector)
    [HideInInspector]
    public AudioSource source;
}
