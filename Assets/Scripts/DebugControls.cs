using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
	[SerializeField] FlashImage flashReference;
	Vector2 rotation = Vector2.zero;
	public float speed = 3;

    private void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
	{
		rotation.y += Input.GetAxis("Mouse X");
		rotation.x += -Input.GetAxis("Mouse Y");
		transform.eulerAngles = (Vector2)rotation * speed;

		if(Input.GetMouseButtonUp(0))
        {
			flashReference.StartFlash(0.25f, 0.8f, Color.white);
        }
	}
}
