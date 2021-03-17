using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculIntersection : MonoBehaviour
{
    /* 
     *  the calcul of the intersection
     *  so you need 3 thinfs
     *  the movment of the enmey
     *  the movement of the player
     *  the position of enemy
     *  the position of player
     *  the speed of enemy
     *  the acceleration of enemy
     *  the deceleration of enmey
     *  
     *  i can either take it directly (but not fun)
     *  or calculate it 
     *  but let's start easy
     *  
     *  formula :
     *  I = A + dA * k
     *  where k = (dBy(Ax - Bx) + dBx(By - Ay)/ (dAy dBx - dBy dAx)
     *  if(dAy dBx - dBy dAx != 0) - > 0 would mean that they already touch each other
     * 
     */
}
