using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Normal.Realtime;

using System.Net;
using System.IO;

// using NewtonSoft.Json;

public class Fire : MonoBehaviour
{

    private Player _player;
    private Soul _soul;
    private SoulSync _soulSync;

    [SerializeField] Text text;

    [SerializeField] string _gate;
    [SerializeField] float _angle;

    // Start is called before the first frame update
    void Awake()
    { }

    // Update is called once per frame
    void Update()
    {
        if (!_player)
        {
            GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject playerGameObject in playerGameObjects)
            {
                if (playerGameObject.GetComponent<RealtimeView>().isOwnedLocallyInHierarchy)
                {
                    _player = playerGameObject.GetComponent<Player>();
                }
            }

            GameObject soulGameObject = GameObject.FindWithTag("Soul");
            _soul = soulGameObject.GetComponent<Soul>();
            _soulSync = soulGameObject.GetComponent<SoulSync>();
        }
        text.text = _gate + (_angle > 0 ? String.Format("({0:0.00})", _angle) : "");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string URL = "http://127.0.0.1:5000/evolve_soul";
        string response = string.Empty;
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(URL);
        webRequest.ContentType = "application/json";
        webRequest.Method = "POST";
        using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
        {
            streamWriter.Write(new StateEvolution(new Statevector(_soul.neitherLive, _soul.rightyLives, _soul.leftyLives, _soul.bothLive), _gate, _player.side, _angle).ToJSON());
        }
        HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
        Stream responseStream = webResponse.GetResponseStream();
        using (StreamReader reader = new StreamReader(responseStream))
        {
            response = reader.ReadToEnd();
        }
        Statevector statevector = Statevector.FromJSON(response);
        // Debug.Log(statevector.ToJSON());

        _soulSync.SetNeitherLive(statevector.neitherLive);
        _soulSync.SetRightyLives(statevector.rightyLives);
        _soulSync.SetLeftyLives(statevector.leftyLives);
        _soulSync.SetBothLive(statevector.bothLive);

        Destroy(gameObject);
    }

}
