using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLightChanger : MonoBehaviour
{
    Light _L;

    // Start is called before the first frame update
    void Start()
    {
        _L = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        _L.intensity = Mathf.PingPong(Time.time, 2);
    }
}
