using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        Vector3 vec = player.transform.position + new Vector3(-8f, 16f, -8f);

        transform.position = vec;
    }
}
