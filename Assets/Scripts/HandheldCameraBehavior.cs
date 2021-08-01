using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandheldCameraBehavior : MonoBehaviour
{
    List<GameObject> enemyList;
    Camera cam;
    private bool canSeeTarget;
    // Start is called before the first frame update

    public static class RendererExtensions
    {
        
    }
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        canSeeTarget = false;
        if(canSeeTarget)
        {
            Debug.Log("can see target");
        }
    }

    // returns true if renderer is detected in camera
    private bool checkIfVisible(Renderer renderer, Camera camera)
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    //returns visible target
    public GameObject getVisibleTarget()
    {
        enemyList = GameManager.Instance.enemyList;

        for(int i = 0; i < enemyList.Count; i++)
        {
            // checks if each target if visible in cam
            canSeeTarget = checkIfVisible(enemyList[i].GetComponent<Renderer>(), cam);
            if (canSeeTarget)
            {
                return enemyList[i];
            }
        }
        return null;
    }
}
