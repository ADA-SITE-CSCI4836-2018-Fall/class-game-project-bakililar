using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootAtCamera : MonoBehaviour
{

    void Start()
    {
        //transform.LookAt(Camera.current.transform);
    }

    void Update()
    {
        transform.LookAt(Camera.current.transform);
    }

}
