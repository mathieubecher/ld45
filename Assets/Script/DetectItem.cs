using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectItem : MonoBehaviour
{
    public List<ItemInventory> inventory;
    public Special hover;
    [SerializeField] private List<Special> nearspecials;
    [SerializeField] private GameObject text;
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
        hover = null;
        while (i< nearspecials.Count)
        {
            if (nearspecials[i].GetComponent<BoxCollider2D>().Distance(GetComponent<Collider2D>()).distance > 0)
            {
                nearspecials.Remove(nearspecials[i]);
            }
            else
            {
                if (nearspecials[i].GetComponent<BoxCollider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    if (hover == null || hover.transform.position.z > nearspecials[i].transform.position[i])
                    {
                        hover = nearspecials[i];

                    }
                }
                ++i;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.GetComponent(typeof(Item)) != null)
        {
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
        else if(!collision.isTrigger && collision.GetComponent(typeof(Special)) != null)
        {
            Special item = collision.GetComponent(typeof(Special)) as Special;
            nearspecials.Add(item);
        }
            

    }
}
