using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Special
{
    [SerializeField] private Sprite plant;
    [SerializeField] private float timeGrow = 120;
    private bool grow = false;
    [SerializeField] private GameObject finalState;

    public void Update()
    {
        if(grow) timeGrow -= Time.deltaTime;
        if(timeGrow < 0)
        {
            Instantiate(finalState, transform.parent);
            Destroy(this.gameObject);
        }
    }
    public override void ActionRelease(Controller parent)
    {
        base.ActionRelease(parent);
        
        Destroy( this.GetComponent<Rigidbody2D>());
        this.GetComponent<SpriteRenderer>().sprite = plant;
        grow = true;
        this.gameObject.layer = LayerMask.GetMask("Default");
    }
}
