using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource playerAudioSource;
    AssetBundleManager assetManager;
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
    AudioClip flashCamSound;
    AudioClip sonicCamSound;
    AudioClip pumpCamSound;
    AudioClip mobSpawnSound;
    AudioClip purchaseUpgradeSound;
    // Start is called before the first frame update

    void Start()
    {
        assetManager = AssetBundleManager.Instance;
        flashCamSound = assetManager.GetAsset<AudioClip>("sounds", "camera-shutter-click-08");
        sonicCamSound = assetManager.GetAsset<AudioClip>("sounds", "Sonic Cam");
        pumpCamSound = assetManager.GetAsset<AudioClip>("sounds", "Pump Cam");
        mobSpawnSound = assetManager.GetAsset<AudioClip>("sounds", "MobSpawn");
        purchaseUpgradeSound = assetManager.GetAsset<AudioClip>("sounds", "Upgrade Purchase");
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

    public void playPurchaseUpgradeSound()
    {
        if(purchaseUpgradeSound != null)
        {
            AudioSource.PlayClipAtPoint(purchaseUpgradeSound, Camera.main.gameObject.transform.position, 1);
        }
    }

    public void playMobSpawnSound()
    {
        AudioSource.PlayClipAtPoint(mobSpawnSound, playerAudioSource.gameObject.transform.position);
    }
}
