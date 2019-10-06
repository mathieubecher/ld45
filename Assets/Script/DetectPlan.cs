using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlan : MonoBehaviour
{
    [SerializeField] public List<SpecialPlan> plans;
    public bool changed = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < plans.Count)
        {
            if (plans[i] == null || plans[i].GetComponent<BoxCollider2D>().isTrigger || plans[i].GetComponent<BoxCollider2D>().Distance(GetComponent<Collider2D>()).distance > 0)
            {
                plans.Remove(plans[i]);
                changed = true;
            }
            else
            {
                ++i;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent(typeof(SpecialPlan))) { 
            if(!plans.Exists(x=>x.name == (collision.gameObject.GetComponent(typeof(SpecialPlan)) as SpecialPlan).name)){ 
                plans.Add(collision.gameObject.GetComponent(typeof(SpecialPlan)) as SpecialPlan);
                changed = true;
            }
        }
    }
}
