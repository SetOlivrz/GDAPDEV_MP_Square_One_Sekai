using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] private FlashImage flashImage = null;
    Color newColor = Color.white;
    int flashOpacity = 1;
    bool isShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isShoot = false;
    }

    public void Capture()
    {
        flashImage.StartFlash(0.25f, flashOpacity, newColor);
        isShoot = true;
    }
}
