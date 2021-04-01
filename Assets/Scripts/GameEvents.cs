using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class GameEvents
{
    public static UnityEvent onHit = new UnityEvent();
    public static UnityEvent createShip = new UnityEvent();
}

