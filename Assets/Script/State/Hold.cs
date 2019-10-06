using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : State
{
    public Special hold;
    public Hold(Controller parent, Special hold):base(parent)
    {
        this.hold = hold;
        hold.transform.SetParent(parent.transform);
        hold.transform.localPosition = new Vector3(0,0.5f);
        hold.GetComponent<Rigidbody2D>().isKinematic = true;
        hold.GetComponent<SpriteRenderer>().sortingOrder = 4;
    }
    public override void Move()
    {
        parent.move = parent.move * 0.5f;
        base.Move();
    }
    public override void Click()
    {
        parent.state = new State(parent);
        hold.transform.localPosition = Vector3.zero;
        hold.transform.SetParent(null);
        Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position;
        vector.z = 0;
        if (vector.magnitude > 2) hold.transform.position = vector.normalized * 2 + parent.transform.position;
        else hold.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hold.GetComponent<Rigidbody2D>().isKinematic = false;
        hold.GetComponent<SpriteRenderer>().sortingOrder = 1;
        
    }
}
