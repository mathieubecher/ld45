using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public Controller parent;

    public State(Controller parent)
    {
        this.parent = parent;
    }

    public virtual void Move()
    {
        parent.GetComponent<Rigidbody2D>().velocity = parent.move;
        if (parent.move.x < 0)
        {
            parent.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (parent.move.x > 0)
        {
            parent.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    public virtual void Click()
    {
        if (Input.GetMouseButtonDown(0) && parent.detect.hover)
        {
            parent.detect.hover.Resistance -= parent.originforce;
        }
        else if (Input.GetMouseButtonDown(0) && parent.detectItem.hover)
        {
            Debug.Log("j'attrape");
            Hold(parent.detectItem.hover);
        }
    }
    public virtual void Hold(Special hold) { parent.state = new Hold(parent, hold);}

    public virtual string GetName() { return "Idde"; }
}
