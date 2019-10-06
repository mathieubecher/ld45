using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDay : MonoBehaviour
{
    public float cycle = 0;
    public float timeday = 10000;
    public Color night = new Color(88/255, 87/255, 108/255);
    public Color stateNight = new Color(1, 1, 1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cycle += Time.deltaTime;
        cycle = cycle % timeday;
        stateNight = Color.Lerp(Color.white, night,0);

    }
}
