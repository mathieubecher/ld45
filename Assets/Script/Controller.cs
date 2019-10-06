using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    [SerializeField] public DetectObject detect;
    [SerializeField] public DetectItem detectItem;

    public Vector2 move;
    public State state;
    public float speed = 2;
    public float originforce = 0.5f;
    public float food = 5;
    public float energy = 5;
    public float life = 5;
    // Start is called before the first frame update
    void Start()
    {
        state = new State(this);
    }

    // Update is called once per frame
    void Update()
    {
        GetMove();
        state.Move();
        if (Input.GetMouseButtonDown(0)) state.Click();



    }
    protected void GetMove()
    {
        
        move = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")).normalized * speed;
    }

   

}
