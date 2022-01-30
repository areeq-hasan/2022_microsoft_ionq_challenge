using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Normal.Realtime;

public class Soul : MonoBehaviour
{

    [SerializeField] public Text soulText;

    // Synced Fields
    public Vector2 neitherLive = new Vector2(0.9f, 0.0f);
    public Vector2 rightyLives = new Vector2(0.0f, 0.0f);
    public Vector2 leftyLives = new Vector2(0.0f, 0.0f);
    public Vector2 bothLive = new Vector2(Mathf.Sqrt(1.0f - Mathf.Pow(0.9f, 2)), 0.0f);

    private SoulSync _soulSync;

    void Awake()
    {
        _soulSync = GetComponent<SoulSync>();
    }

    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // Update is called once per frame
    void Update()
    {
        soulText.text = String.Format("P(|00>) = {0:0.00}%\nP(|01>) = {1:0.00}%\nP(|10>) = {2:0.00}%\nP(|11>) = {3:0.00}%", Math.Pow(neitherLive.magnitude, 2) * 100, Math.Pow(rightyLives.magnitude, 2) * 100, Math.Pow(leftyLives.magnitude, 2) * 100, Math.Pow(bothLive.magnitude, 2) * 100);
    }
}
