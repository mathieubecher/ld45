using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public Controller parent;
    public float feet = 1;

    public State(Controller parent)
    {
        this.parent = parent;
    }
    public virtual void Update()
    {

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
        feet -= parent.move.magnitude * Time.deltaTime * 1.4f;
        if (feet <= 0)
        {
            feet =1;
            parent.audio.PlayOneShot((AudioClip)Resources.Load("Sound/" + ((Random.value * 2 > 1) ? "Feet1" : "Feet2")));
        }
    }
    public virtual void Click()
    {
        if (parent.detect.hover)
        {
            float hitvalue = parent.originforce;
            if (parent.detect.hover.type == ObjectType.STONE && parent.pickaxe) hitvalue = 2;
            else if (parent.detect.hover.type == ObjectType.WOOD && parent.axe) hitvalue = 1;
            else if (parent.detect.hover.type == ObjectType.PLANT && parent.sickle) hitvalue = 1;
            parent.detect.hover.Resistance -= hitvalue;
            if (parent.detect.hover.Resistance - hitvalue >= 0) parent.audio.PlayOneShot((Random.value * 2> 1)? ((AudioClip)Resources.Load("Sound/Hit1")) : ((AudioClip)Resources.Load("Sound/Hit2")));
            else
            {
                if (parent.detect.hover.type == ObjectType.STONE) parent.audio.PlayOneShot((AudioClip)Resources.Load("Sound/BrokeStone"));
                if (parent.detect.hover.type == ObjectType.WOOD) parent.audio.PlayOneShot((AudioClip)Resources.Load("Sound/BrokeWood"));
            }
        }
    }
    public virtual void E() { 
        if (parent.detectItem.nearspecials.Count > 0)
        {
            
            Hold(parent.detectItem.nearspecials[0]);
           
        }
    }
    public virtual void RightClic() { }
    public virtual void Hold(Special hold) { parent.state = new Hold(parent, hold);}

    public virtual string GetName() { return "Idde"; }
}
