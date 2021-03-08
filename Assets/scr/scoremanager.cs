using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
public class scoremanager : MonoBehaviour
{
    public static scoremanager SC_MAN;
    public Dictionary<string, int> players = new Dictionary<string, int>();
    public List<string> _playerList;
    public GameObject GUIOBJ;
    //public Text[] labels;
    localPlayer[] lp;
    // Use this for initialization
    void Awake()
    {

    }
    void Start()
    {
        SC_MAN = this;
        _playerList = new List<string>();
    }
    public static void turn()
    {




    }

    public void RegisterPlayer(string NID, int scr)
    {
        players.Add(NID, scr);
    }
    public void UnRegisterPlayer(string NID, int scr)
    {
        players.Remove(NID);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<string, int> ent in players)
        {
            if (GameObject.Find(ent.Key + "player") == null)
            {
                _playerList.Add(ent.Key);
            }
        }
        foreach (string s in _playerList)
        {
            players.Remove(s);
        }
        _playerList.Clear();
        _playerList = new List<string>(players.Keys);
      //  Debug.Log(_playerList);

        if (players.Count > 0)
        {
            int iterCount = 0;
            foreach(string s in _playerList)
            {
                if (GameObject.Find(s + "player").activeInHierarchy == true)
                    players[s] = GameObject.Find(s + "player").GetComponent<localPlayer>().score;
                if (GUIOBJ.activeInHierarchy == true)
                {
                    if (iterCount > 0)
                        GameObject.Find("score text").GetComponent<Text>().text += ("\n Player" + s + "\t\t\tSCORE = " + players[s]);
                    else
                        GameObject.Find("score text").GetComponent<Text>().text = ("\n Player" + s + "\t\t\tSCORE = " + players[s]);

                }
                iterCount++;
            }
        }
        _playerList.Clear();
        //  lp = new localPlayer[players.Count];
        //  players.Values.CopyTo(lp, 0);
        //  Debug.Log(lp.Length + "players");
    }



}
