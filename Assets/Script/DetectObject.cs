using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    private List<Object> rangeobjects;
    public Object hover;
    // Start is called before the first frame update
    void Start()
    {
        rangeobjects = new List<Object>();

    }

    // Update is called once per frame
    void Update()
    {
        if(hover!=null)hover.GetComponent<SpriteRenderer>().material = Object.defaultMaterial;
        hover = null;
        int i = 0;
        rangeobjects.RemoveAll(x => x == null);
        while (i < rangeobjects.Count)
        {
            if (rangeobjects[i].GetComponent<BoxCollider2D>().Distance(GetComponent<Collider2D>()).distance > 0)
            {
                
                rangeobjects.Remove(rangeobjects[i]);
            }
            else
            {
                if (rangeobjects[i].DetectMouse.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    if(hover == null || hover.transform.position.z > rangeobjects[i].transform.position[i]) { 
                        hover = rangeobjects[i];
                        
                    }
                }
                ++i;
            }
        }
        if(hover != null) hover.GetComponent<SpriteRenderer>().material = (Material)Resources.Load("Contour");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is BoxCollider2D && collision.GetComponent(typeof(Object)) != null)
            rangeobjects.Add(collision.GetComponent(typeof(Object)) as Object);
    }
}
