using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseState : State
{
    public override State runCurrentState()
    {
        return this;
    }
}
