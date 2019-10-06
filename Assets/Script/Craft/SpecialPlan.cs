using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlan : Special
{
    public List<Plan> plans;

    public override void ActionRelease(Controller parent)
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
