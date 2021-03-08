using UnityEngine;
using System.Collections;

public class spawnManager : MonoBehaviour
{
    GameObject[] spawnPoints;
    // Use this for initialization
    void Start()
    {

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void KillPlayer(GameObject G)
    {

        Debug.Log("here");
        G.SetActive(false);
        StartCoroutine(Respawn(G));
    }

    IEnumerator Respawn(GameObject G)
    {

        G.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        G.GetComponent<randplayer>().randSpawn();

        yield return new WaitForSeconds(2);
        G.SetActive(true);
        G.GetComponent<localPlayer>().regesterMe();
        G.GetComponent<health>().hlt = 100;
        G.GetComponent<health>().hltGUI.setGUI(G.GetComponent<health>().hlt / 100);
        Debug.Log("res");
        G.GetComponent<Shoot>().Start();


    }



}
