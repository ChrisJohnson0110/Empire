using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float x = Camera.main.transform.position.x;
        float y = Camera.main.transform.position.z;

        transform.position = new Vector3(x,transform.position.y,y);
    }
}
