// FloatingOrigin.cs
// Written by Peter Stirling
// 11 November 2010
// Uploaded to Unify Community Wiki on 11 November 2010
// Updated to Unity 5.x particle system by Tony Lovell 14 January, 2016
// fix to ensure ALL particles get moved by Tony Lovell 8 September, 2016
// URL: http://wiki.unity3d.com/index.php/Floating_Origin

//Modified by Mohammad Aarij
//7 July 2020
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class FloatingOrigin : MonoBehaviour
{
    public float threshold = 100.0f;
    public float physicsThreshold = 0; // Set to zero to disable
    public TileManager tileManager;
#if OLD_PHYSICS
    public float defaultSleepVelocity = 0.14f;
    public float defaultAngularVelocity = 0.14f;
#else
    public float defaultSleepThreshold = 0.14f;
#endif

    ParticleSystem.Particle[] parts = null;

    void LateUpdate()
    {
        Vector3 cameraPosition = gameObject.transform.position;
        cameraPosition.y = 0f;
        if (cameraPosition.magnitude > threshold)
        {

            for (int z = 0; z < SceneManager.sceneCount; z++)
            {
                foreach (GameObject obj in SceneManager.GetSceneAt(z).GetRootGameObjects())
                {
                    obj.transform.position -= cameraPosition;
                }
            }
            Vector3 originDelta = Vector3.zero - cameraPosition;
            Debug.Log("recentering origin at : " + originDelta);
        }
    }
}