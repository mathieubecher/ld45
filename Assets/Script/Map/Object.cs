using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    STONE,WOOD
}
public class Object : MonoBehaviour
{
    public static Material defaultMaterial;
    private PolygonCollider2D detectMouse;

    [SerializeField] private int mindrop = 0;
    [SerializeField] private int maxdrop = 1;

    [SerializeField] private List<ItemDrop> drops;

    public ObjectType type = ObjectType.STONE;

    private float timeShake;
    private Vector3 originalPos;
    private Vector3 shake;

    [SerializeField] private float resistance = 3;
    public PolygonCollider2D DetectMouse { get => detectMouse;}
    public float Resistance { get => resistance; set {
            
            shake = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1).normalized * (resistance-value) / 10;
            timeShake = 0.02f;
            if (value <= 0)
            {
                (Camera.main.GetComponent(typeof(Effect)) as Effect).ScreenShake((resistance-value)*2);
                for(int i = 0; i < (int)Mathf.Floor(Random.value * (maxdrop - mindrop) + mindrop); ++i)
                {

                    int item = 0;
                    float allproba = Random.value * GetAllProba();
                    while(item < drops.Count && allproba > 0)
                    {
                        allproba -= drops[item].dropchance;
                        if (allproba > 0) ++item;
                    }
                    GameObject g = Instantiate(drops[item].item, this.transform.position, Quaternion.identity);
                    g.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.value * 2 - 1, Random.value * 2 - 1).normalized * (resistance - value) *20,ForceMode2D.Impulse);
                }
                Destroy(this.gameObject);
            }
            resistance = value;
        }
    }

    private float GetAllProba()
    {
        float proba = 0;
        foreach (ItemDrop drop in drops)
        {
            proba += drop.dropchance;
        }
        return proba;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(defaultMaterial == null ) defaultMaterial = new Material(Shader.Find("Sprites/Default"));
        detectMouse = GetComponent<PolygonCollider2D>();
        originalPos = transform.localPosition;
        shake = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeShake > 0) timeShake -= Time.deltaTime;
        else shake = Vector3.zero;
        transform.localPosition = originalPos + shake;
    }
    public void Show(float height)
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (height));
        // gestion shadow
        for (int i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, height);
    }
}
