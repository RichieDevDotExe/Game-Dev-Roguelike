using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    //SoundFXManager is a singleton. That manages how sound clips are played in the game

    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFX;
    private float clipLength;

    //Makes this file a Singleton. There should only ever be ONE instance of the soundFXManger in the scene
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Passes in an audio clip and the transform of the object the audio clip is playing from. Spawns an object that plays the sound and then destroys that object
    public void playSoundEffect(AudioClip FXclip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFX, spawnTransform.position, Quaternion.identity);
        audioSource.clip = FXclip;
        audioSource.volume = volume;
        audioSource.Play();
        
        clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject,clipLength);
    }
}
