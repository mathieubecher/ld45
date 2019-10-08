using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CraftItem : MonoBehaviour
{
    public DetectItem items;
    public int number;
    public string itemName;
    [SerializeField] private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ItemInventory nbitem = items.inventory.Find(x => x.item.name == itemName);
        if (nbitem == null || nbitem.number < number)
        {
            GetComponent<Image>().material = (Material)Resources.Load("BlackWhite");
            GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            text.color = new Color(1, 0, 0, 1);
        }
        else {  
            GetComponent<Image>().material = Object.defaultMaterial;
            GetComponent<Image>().color = new Color(1,1,1, 1);
            text.color = new Color(0,0,0, 1);
        }
        text.text = number+"/"+((nbitem == null)?0:nbitem.number);
    }
}
