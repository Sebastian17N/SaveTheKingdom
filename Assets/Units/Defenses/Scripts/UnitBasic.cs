using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBasic : UnitBase, IIsDraggedOwner
{
    private void Start()
    {
        IsDragged = true;
        isWalking = false;
        Direction = Vector2.right;

        Team = TeamEnum.Team_1;
    }

    public void Update()
    {
        Routine();
    }
}
