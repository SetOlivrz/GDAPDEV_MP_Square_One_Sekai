using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTester : MonoBehaviour
{
    private Touch trackedFinger1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0)
        {
            trackedFinger1 = Input.GetTouch(0);
            Debug.Log($"touch: {trackedFinger1.phase}");
        }
    }

    private void OnDrawGizmos()
    {
        if (Input.touchCount>0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                Ray ray = Camera.main.ScreenPointToRay(t.position);

                Gizmos.DrawIcon(ray.GetPoint(2), "ghost");
            }
           

        }
    }
}
