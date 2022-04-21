using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public GameObject player;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.current;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
