using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : State
{
    public Special hold;
    public GameObject nextpos;
    public GameObject hover;

    private int lastSorting;
    public Hold(Controller parent, Special hold):base(parent)
    {
        parent.audio.PlayOneShot((AudioClip)Resources.Load("Sound/" + ((Random.value * 2 > 1) ? "Hold1" : "Hold2")));
        if (hold.transform.childCount > 0)
            hold.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        this.hold = hold;
        this.hold.GetComponent<Collider2D>().enabled = false;
        hold.transform.SetParent(parent.transform);
        hold.transform.localPosition = new Vector3(0,0.8f);
        hold.GetComponent<Rigidbody2D>().isKinematic = true;
        lastSorting = hold.GetComponent<SpriteRenderer>().sortingOrder;
        hold.GetComponent<SpriteRenderer>().sortingOrder = 4;
        if (hover == null) hover = Object.Instantiate(Resources.Load<GameObject>("Case/hover"), Vector3.zero, Quaternion.identity);
        hover.SetActive(false);
    }
    public override void Move()
    {
        parent.move = parent.move * 0.5f;
        base.Move();
    }
    public override void Update()
    {
        if (nextpos != null)
        {
            nextpos = null;
        }
        if (hold.placable)
        {
            Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position;
            vector.z = 0;
            if (vector.magnitude > 2) vector = vector.normalized * 2 + parent.transform.position;
            else vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D raycast = Physics2D.Raycast(vector, Vector2.zero, 50, LayerMask.GetMask("Case"));
            if (raycast.collider != null)
            {
                if (raycast.collider.GetComponent(typeof(Case)) != null && raycast.collider.transform.childCount == 0)
                {
                    nextpos = raycast.collider.gameObject;
                    
                    hover.transform.position = nextpos.transform.position;
                    hover.SetActive(true);


                }
            }
            if (nextpos == null) hover.SetActive(false);
            if (nextpos != null && !(hold.GetComponent(typeof(Special)) as Special).typesAccept.Contains((nextpos.GetComponent(typeof(Case)) as Case).type))
            {
                nextpos = null;
                hover.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            }
            else hover.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
        else hover.SetActive(false);
    }
    public override void Click()
    {
        
        if(nextpos != null) {
            parent.audio.PlayOneShot((AudioClip)Resources.Load("Sound/Release"));
            hover.SetActive(false);
            hold.transform.SetParent(nextpos.transform);
            hold.transform.localPosition = new Vector2(0.5f, -0.5f);
            hold.GetComponent<Rigidbody2D>().isKinematic = false;
            hold.GetComponent<SpriteRenderer>().sortingOrder = lastSorting;
            parent.state = new State(parent);
            hold.ActionRelease(parent);
            this.hold.GetComponent<Collider2D>().enabled = true;
            parent.detectItem.nearspecials.Remove(hold);
            if (hold.transform.childCount > 0)
                hold.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        else if (!hold.placable)
        {
            hover.SetActive(false);
            hold.ActionRelease(parent);
            Object.Destroy(hold.gameObject);
            parent.state = new State(parent);
            hold.GetComponent<SpriteRenderer>().sortingOrder = lastSorting;
            if (hold.transform.childCount > 0)
                hold.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        
    }
    public override void E()
    {
        Click();
    }
    public override void RightClic()
    {
        if (hold.GetComponent(typeof(SpecialPlan))) Click();
        else { 
            hover.SetActive(false);
            parent.audio.PlayOneShot((AudioClip)Resources.Load("Sound/Release"));
            Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position;
            vector.z = 0;   
            if(vector.magnitude > 4) vector = vector.normalized *4;
            hold.transform.localPosition = vector * 0.5f ;
            hold.transform.SetParent(null);
            hold.GetComponent<Rigidbody2D>().isKinematic = false;
            hold.GetComponent<SpriteRenderer>().sortingOrder = lastSorting;
            this.hold.GetComponent<Collider2D>().enabled = true;
            hold.GetComponent<Rigidbody2D>().AddForce(vector*20, ForceMode2D.Impulse);
            parent.state = new State(parent);
            if (hold.transform.childCount > 0)
                hold.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
    public override string GetName() { return "Hold"; }

}
