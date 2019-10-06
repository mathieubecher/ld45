using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CaseType
{
    SAND,STONE,GROUND,GRASS
}
public class Case : MonoBehaviour
{
    private bool appear = false;
    public Vector2 position;
    public GameObject neighbour;
    [SerializeField] public float height = 0;
    public CaseType type;
    public AnimationCurve arrival;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,height);
        for (int i = 0; i < transform.childCount; ++i)
        {
            if ((transform.GetChild(i).GetComponent(typeof(Object)) as Object) != null)
                (transform.GetChild(i).GetComponent(typeof(Object)) as Object).Show(height);
            else transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, height);
        
            if (transform.GetChild(i).GetComponent<Collider2D>() != null) transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!appear)
        {
            if (height < 1) height += Time.deltaTime;
            else
            {
                height = 1;
                Destroy(neighbour);
                for (int i = 0; i < transform.childCount; ++i)
                {
                    if (transform.GetChild(i).GetComponent<Collider2D>() != null)
                        transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                }
                appear = true;
            }
            
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, height);
            for (int i = 0; i < transform.childCount; ++i) {
                if((transform.GetChild(i).GetComponent(typeof(Object)) as Object) != null)
                    (transform.GetChild(i).GetComponent(typeof(Object)) as Object).Show(height);
                else transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,height);
            }

            transform.position = new Vector3(position.x, position.y + (arrival.Evaluate(height) - 1), position.y / 10f) ; 
            // new Vector3(position.x, position.y + height, position.y / 10f);
        }
        
    }
}
