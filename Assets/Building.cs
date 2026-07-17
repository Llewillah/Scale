using UnityEngine;
using System.Collections.Generic;

public class Building : MonoBehaviour, IClickable
{
    //resource index, amount stored
    Dictionary<int,int> storage = new Dictionary<int,int>();
    public void DoUpdate() { }

    public void OnClick() { }
}
