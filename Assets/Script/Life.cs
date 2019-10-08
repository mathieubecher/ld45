using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    private float minlife = 0.16f;
    private float maxlife = 0.6f;
    [SerializeField] private Controller parent;
    [SerializeField] private Image life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = minlife + (parent.Food/5) *( maxlife - minlife);
        life.fillAmount = (parent.Life / 5);
    }
}
