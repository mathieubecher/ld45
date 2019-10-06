using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tool : MonoBehaviour
{
    public Controller player;
    public string tool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tool == "Axe" && player.axe) GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (tool == "PickAxe" && player.pickaxe) GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (tool == "Sickle" && player.sickle) GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
}
