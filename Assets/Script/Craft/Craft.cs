using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Craft : MonoBehaviour
{
    public CraftTable parent;
    public GameObject craftitem;
    public TMPro.TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private GameObject hoverInfo;
    private bool hover = false;
    public Plan plan;
   
    public void Action()
    {
        if (parent.player.state.GetName() != "Hold"){
           
            if (EnoughtItem())
            {
                for (int i = 0; i < plan.plan.Count; ++i)
                {
                    parent.player.detectItem.inventory.Find(x => x.item.name == plan.plan[i].item.name).number -= plan.plan[i].number;

                    ItemInventory item = parent.player.detectItem.inventory.Find(x => x.item.name == plan.plan[i].item.name);
                    if (item.number <= 0)
                    {
                        Destroy(item.item.gameObject);
                        parent.player.detectItem.inventory.Remove(item);
                    }
                }
                if (plan.type == PlanType.TOOL) {
                    parent.player.AddTool((plan.craftItem.GetComponent(typeof(Item)) as Item).name);
                }
                else { 
                    
                    GameObject g = Instantiate(plan.craftItem, Vector3.zero, Quaternion.identity);
                    Special special = g.GetComponent(typeof(Special)) as Special;
                    parent.player.state.Hold(special);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        image.sprite = plan.craftItem.GetComponent<SpriteRenderer>().sprite;
        text.text = plan.craftItem.name;
        
        for (int i = 0; i < plan.plan.Count; ++i)
        {
            GameObject g = Instantiate(craftitem, hoverInfo.transform);
            g.GetComponent<Image>().sprite = plan.plan[i].item.GetComponent<SpriteRenderer>().sprite;
            g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + plan.plan[i].number;
            g.GetComponent<RectTransform>().anchoredPosition = new Vector2(g.GetComponent<RectTransform>().anchoredPosition.x + 50 * i, g.GetComponent<RectTransform>().anchoredPosition.y);


        }

        float size = text.GetTextInfo(text.text).textComponent.GetPreferredValues().x+20;
        hoverInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(( 50 * (plan.plan.Count)> size) ? (50 * (plan.plan.Count)) : size +20, hoverInfo.GetComponent<RectTransform>().sizeDelta.y);
        hoverInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnoughtItem())
        {
            image.material = Object.defaultMaterial;
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            image.material = (Material)Resources.Load("BlackWhite");
            image.color = new Color(1, 1, 1, 0.5f);
        }
        if (!hover && IsMouseOverUI())
        {
            hover = true;
            hoverInfo.SetActive(true);
            
        }
        else if (hover && !IsMouseOverUI())
        {
            hoverInfo.SetActive(false);
            hover = false;
        }
    }
    public bool EnoughtItem()
    {
        bool enought = true;
        foreach(ItemInventory item in plan.plan)
        {
            if(!(parent.player.detectItem.inventory.Exists(x=> x.item.name == item.item.name) && parent.player.detectItem.inventory.Find(x => x.item.name == item.item.name).number >= item.number))
            {
                enought = false;
            }
        }
        return enought;
    }
    private bool IsMouseOverUI()
    {
        
        Rect rectangle = new Rect((Vector2)transform.position - GetComponent<RectTransform>().pivot * GetComponent<RectTransform>().rect.size, GetComponent<RectTransform>().rect.size);
        return rectangle.Contains(Input.mousePosition);
    }
}
