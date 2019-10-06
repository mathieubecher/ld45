using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private float timeshake;
    private Vector3 vectorshake;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeshake > 0) timeshake -= Time.deltaTime;
        else vectorshake = Vector3.back * 10;
        transform.localPosition = vectorshake;
    }
    public void ScreenShake(float force)
    {
        timeshake = 0.05f;
        vectorshake = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1).normalized * force/10;
        vectorshake.z = -10;
    }
}
