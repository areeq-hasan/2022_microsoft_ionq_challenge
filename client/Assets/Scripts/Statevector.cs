using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Statevector
{
    public Vector2 neitherLive;
    public Vector2 rightyLives;
    public Vector2 leftyLives;
    public Vector2 bothLive;

    public Statevector(Vector2 neitherLive, Vector2 rightyLives, Vector2 leftyLives, Vector2 bothLive)
    {
        this.neitherLive = neitherLive;
        this.rightyLives = rightyLives;
        this.leftyLives = leftyLives;
        this.bothLive = bothLive;
    }

    public static Statevector FromJSON(string json)
    {
        return JsonUtility.FromJson<Statevector>(json);
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }

}
