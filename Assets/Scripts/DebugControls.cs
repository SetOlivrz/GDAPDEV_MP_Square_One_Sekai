using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
	[SerializeField] FlashImage flashReference;
	[SerializeField] PlayerController player;
	[SerializeField] Camera cam;
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
            Shoot();
        }
	}

    public void Shoot()
    {
        Debug.Log("pew");

        RaycastHit hit;
        if (Physics.SphereCast(cam.transform.position, 5, cam.transform.forward, out hit, 100))

        {
            Debug.Log("wahhhhhhhhhhhhhhhhhhhhhh");

            Debug.Log(hit.transform.name);

            hit.transform.GetComponent<EnemyBehavior>().TakeDamage(1);
            hit.transform.GetComponent<EnemyBehavior>().DisplayStats();
        }
    }
}
