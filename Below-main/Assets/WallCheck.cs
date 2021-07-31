using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField]
    Transform[] wallChecks;
    [SerializeField]
    float checkSize = 1;
    [SerializeField]
    LayerMask walls;
    bool front, back, right, left;


    //Returns an Int between 1 and 8 depending on which sides are touching a wall.
    //Returns 0 if not toucing a wall.
    public int IsTouchingWall()
    {
        //Reset all Checks
        front = false;
        back = false;
        right = false;
        left = false;
        
        //Checks to see if left,right,front, or back are touching a wall.
        if(Physics.CheckSphere(wallChecks[0].position,checkSize,walls) ||
            Physics.CheckSphere(wallChecks[1].position, checkSize, walls))
        {
            front = true;
        }
        /*
        if (Physics.CheckSphere(wallChecks[2].position, checkSize, walls) ||
            Physics.CheckSphere(wallChecks[3].position, checkSize, walls))
        {
            back = true;
        }
        if (Physics.CheckSphere(wallChecks[4].position, checkSize, walls) ||
            Physics.CheckSphere(wallChecks[5].position, checkSize, walls))
        {
            right = true;
        }
        if (Physics.CheckSphere(wallChecks[2].position, checkSize, walls) ||
            Physics.CheckSphere(wallChecks[3].position, checkSize, walls))
        {
            back = true;
        }
        */

        //Returns the proper number
        if(front && !back && !right && !left)
        {
            return 1;
        }
        if (front && !back && right && !left)
        {
            return 2;
        }
        if (!front && !back && right && !left)
        {
            return 3;
        }
        if (!front && back && right && !left)
        {
            return 4;
        }
        if (!front && back && !right && !left)
        {
            return 5;
        }
        if (!front && back && !right && left)
        {
            return 6;
        }
        if (!front && !back && !right && left)
        {
            return 7;
        }
        if (front && !back && !right && left)
        {
            return 8;
        }
        return 0;
    } 
}
