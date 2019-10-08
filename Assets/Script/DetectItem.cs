using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectItem : MonoBehaviour
{

    public List<ItemInventory> inventory;
    public Controller parent;
    [SerializeField] public List<Special> nearspecials;
    [SerializeField] private GameObject text;
    public GameObject holdInfo;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<ItemInventory>();
        nearspecials = new List<Special>();

    }


    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i< nearspecials.Count)
        {
            if (nearspecials[i] == null || nearspecials[i].GetComponent<BoxCollider2D>().Distance(GetComponent<Collider2D>()).distance > 0)
            {
                if (holdInfo != null) Destroy(holdInfo);
                nearspecials.Remove(nearspecials[i]);
                if (nearspecials.Count > 0) holdInfo = Instantiate((GameObject)Resources.Load("Text/Hold"), nearspecials[0].transform);
                
            }
            else
            {
                ++i;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.GetComponent(typeof(Item)) != null)
        {
            parent.audio.PlayOneShot((Random.value * 2 > 1) ? ((AudioClip)Resources.Load("Sound/Take1")) : ((AudioClip)Resources.Load("Sound/Take2")));
            Item item = collision.GetComponent(typeof(Item)) as Item;
            if (inventory.Exists(x => x.item.name == item.name))
            {
                Destroy(item.gameObject);
                ++inventory.Find(x => x.item.name == item.name).number;
            }
            else inventory.Add(new ItemInventory(item));
            collision.gameObject.transform.SetParent(this.transform);
            collision.gameObject.SetActive(false);
            Instantiate(text,transform);
        }
        else if(collision.GetComponent(typeof(Special)) != null)
        {
            Special item = collision.GetComponent(typeof(Special)) as Special;
            nearspecials.Add(item);
            if (holdInfo != null) Destroy(holdInfo);
            holdInfo = Instantiate((GameObject)Resources.Load("Text/Hold"), nearspecials[0].transform);
        }
            

    }
}
