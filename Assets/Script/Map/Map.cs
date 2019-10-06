using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject initialCase;
    public GameObject EmptyCase;
    public AnimationCurve curve;
    public List<GameObject> prefabCases;
    public float minTime = 3;
    public float maxTime = 7;

    public List<Case> cases;
    public List<Neighbour> neighbours;
    private float wait = 1;


    // Start is called before the first frame update
    void Start()
    {
        AddCase(initialCase, new Vector2(0,0), 1);
        wait = minTime + Random.value * (maxTime - minTime);
    }

    // Update is called once per frame
    void Update()
    {
        wait -= Time.deltaTime;
        if(wait < 0)
        {
            wait = minTime + Random.value * (maxTime - minTime);
            AddCase(prefabCases[(int)Mathf.Floor(curve.Evaluate(Random.value) * prefabCases.Count)], neighbours[(int)Mathf.Floor(Random.value * neighbours.Count)]);
        }
    }
    public Case AddCase(GameObject prefabCase, Vector2 pos, float height = -1)
    {
       

        GameObject newcase = Instantiate(prefabCase, this.transform);
        (newcase.GetComponent(typeof(Case)) as Case).position = pos;
        (newcase.GetComponent(typeof(Case)) as Case).height = height;

        cases.Add((newcase.GetComponent(typeof(Case)) as Case));

        AddNeighbour(new Vector2(1, 0) + pos);
        AddNeighbour(new Vector2(-1, 0) + pos);
        AddNeighbour(new Vector2(0, -1) + pos);
        AddNeighbour(new Vector2(0, 1) + pos);
        return (newcase.GetComponent(typeof(Case)) as Case);
    }
    public void AddCase(GameObject prefabCase, Neighbour pos, float height = -1)
    {
        Case newcase = AddCase(prefabCase, pos.position, height);
        newcase.neighbour = pos.gameObject;
        neighbours.Remove(pos);
        
        
    }
    public void AddNeighbour(Vector2 pos)
    {
        if (!neighbours.Find(x => x.position == pos) && !cases.Find(x => x.position == pos))
        {
            GameObject emptyCase = Instantiate(EmptyCase,this.transform);
            (emptyCase.GetComponent(typeof(Neighbour)) as Neighbour).position = pos;
            neighbours.Add(emptyCase.GetComponent(typeof(Neighbour)) as Neighbour);
        }
    }
}
