using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consummer : Special
{
    public float food = 0;
    public float energy = 0;
    public float life = 0;

    public void Start()
    {
        placable = false;
    }
    public override void ActionRelease(Controller parent)
    {
        base.ActionRelease(parent);
        parent.Food += food;
        parent.energy += energy;
        parent.life += life;
    }


}
