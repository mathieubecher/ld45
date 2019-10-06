using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : MonoBehaviour
{
    [SerializeField] public GameObject prefabPlan;
    public Controller player;
    private List<Craft> table;
    public List<Plan> plans;
    public PlanType onglet = PlanType.FURNITURE;
    // Start is called before the first frame update
    void Start()
    {
        table = new List<Craft>();
        ChangeOnglet();
           
    }

    // Update is called once per frame
    void Update()
    {
        if (player.plans.changed) ChangeOnglet();
    }
    public void ChangeOnglet()
    {
        player.plans.changed = false;
        int i = 0;
        while (i < table.Count)
        {
            Destroy(table[i].gameObject);
            ++i;
        }
        table = new List<Craft>();
        foreach (Plan plan in plans)
        {
            if (plan.type == onglet)
            {
                GameObject g = Instantiate(prefabPlan, transform);
                Craft craft = g.GetComponent(typeof(Craft)) as Craft;
                craft.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -25 - table.Count * 45, 0);
                craft.plan = plan;
                craft.parent = this;
                table.Add(craft);
            }
        }
        foreach(SpecialPlan specialPlan in player.plans.plans)
        {
            foreach(Plan plan in specialPlan.plans)
            {
                if (plan.type == onglet)
                {
                    GameObject g = Instantiate(prefabPlan, transform);
                    Craft craft = g.GetComponent(typeof(Craft)) as Craft;
                    craft.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -25 - table.Count * 45, 0);
                    craft.plan = plan;
                    craft.parent = this;
                    table.Add(craft);
                }
            }
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, 50 + (table.Count-1) * 45);
    }
    public void ChangeOnglet(PlanType type)
    {
        this.onglet = type;
        ChangeOnglet();
    }
    public void ChangeOnglet(string type)
    {
        switch (type)
        {
            case "FOOD":
                this.onglet = PlanType.FOOD;
                break;
            case "FURNITURE":
                this.onglet = PlanType.FURNITURE;
                break;
            case "TOOL":
                this.onglet = PlanType.TOOL;
                break;
        }
        ChangeOnglet();
    }
}
