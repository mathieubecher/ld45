using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    [SerializeField] public DetectObject detect;
    [SerializeField] public DetectItem detectItem;
    [SerializeField] public DetectPlan plans;

    public AudioClip hit1;
    public AudioClip hit2;
    public Vector2 move;
    public State state;
    public float speed = 2;
    public float originforce = 0.5f;
    private float food = 5;
    public float energy = 5;
    private float life = 5;
    public AudioSource audio;
    public bool pickaxe = false;
    public bool axe = false;
    public bool sickle = false;

    public float Food { get => food; set
        {
            food = value;
            if (food < 0) food = 0;
            else if (food > 5) food = 5;
        }
    }
    public float Life
    {
        get => life; set
        {
            life = value;
            if (life < 0) life = 0;
            else if (life > 5) life = 5;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        detectItem.parent = this;
        state = new State(this);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
        GetMove();
        state.Move();
        if (Input.GetMouseButtonDown(0)) state.Click();
        else if (Input.GetKeyDown(KeyCode.E)) state.E();
        if (Input.GetMouseButtonDown(1)) state.RightClic();

        this.Food -= Time.deltaTime / 20;
        if (this.Food <= 0) Life -= Time.deltaTime / 10;


    }
    protected void GetMove()
    {
        
        move = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")).normalized * speed;
        if (move.magnitude == 0) GetComponent<Animator>().SetBool("Move", false);
        else GetComponent<Animator>().SetBool("Move", true);
    }
    public void AddTool(string tool)
    {
        Debug.Log(tool);
        if (tool == "Pickaxe") pickaxe = true;
        else if (tool == "Axe") axe = true;
        else if (tool == "Sickle") sickle = true;
    }



}
