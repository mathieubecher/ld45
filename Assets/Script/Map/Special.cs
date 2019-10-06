using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    public string name;
    public bool physic = true;
    public bool placable = true;
    public List<CaseType> typesAccept;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void ActionRelease(Controller parent)
    {

    }
}
