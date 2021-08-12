using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    [SerializeField]private GameObject cameraContainer;
    private Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        //cameraContainer = new GameObject("Camera Container");
        //cameraContainer.transform.position = transform.position;
        //transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();
    }

    // Update is called once per frame
    void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        else
        {
            return false;
        }
    }
}
