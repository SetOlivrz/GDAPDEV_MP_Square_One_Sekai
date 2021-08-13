using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource playerAudioSource;
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] AudioClip flashCamSound;
    [SerializeField] AudioClip sonicCamSound;
    [SerializeField] AudioClip pumpCamSound;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playFlashCamSound()
    {
        if (playerAudioSource != null && flashCamSound != null)
        {
            if (!playerAudioSource.isPlaying)
            {
                playerAudioSource.clip = flashCamSound;
                playerAudioSource.Play();
            }
        }

        else Debug.Log("Error playing flashCamSound");
    }

    public void playSonicCamSound()
    {
        if(playerAudioSource != null && sonicCamSound != null)
        {
            if(!playerAudioSource.isPlaying)
            {
                playerAudioSource.clip = sonicCamSound;
                playerAudioSource.Play();
            }
        }

        else Debug.Log("Error playing sonicCamSound");
    }

    public void playPumpCamSound()
    {
        if (playerAudioSource != null && pumpCamSound != null)
        {
            if (!playerAudioSource.isPlaying)
            {
                playerAudioSource.clip = pumpCamSound;
                playerAudioSource.Play();
            }
        }

        else Debug.Log("Error playing pumpCamSound");
    }
}
