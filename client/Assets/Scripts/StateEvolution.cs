using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateEvolution
{
    public Statevector soul;
    public string gate;
    public string target;
    public float angle;

    public StateEvolution(Statevector soul, string gate, string target, float angle)
    {
        this.soul = soul;
        this.gate = gate;
        this.target = target;
        this.angle = angle;
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}
