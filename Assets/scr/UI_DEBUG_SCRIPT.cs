using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_DEBUG_SCRIPT : MonoBehaviour {
    public GameObject _Player;
    float _playerHealth;
    bool _isRealoding;
    float _bullets;
    float _meter;
    // Use this for initialization
    void Start()
    {
        if (_Player != null)
        {
            _playerHealth = _Player.GetComponent<health>().hlt;
            _isRealoding = _Player.GetComponent<Shoot>().reloading;
            _bullets = _Player.GetComponent<Shoot>().magsize;
            _meter = _Player.GetComponent<StreakManager>().meter;
        }
    }
	// Update is called once per frame
	void Update () {
	if(!_Player)
        {
            return;
        }
        _playerHealth = _Player.GetComponent<health>().hlt;
        _isRealoding = _Player.GetComponent<Shoot>().reloading;
        _bullets = _Player.GetComponent<Shoot>().magsize;
        _meter = _Player.GetComponent<StreakManager>().meter;
        GetComponent<Text>().text = "HLTH:" + _playerHealth + " Reloading:" + _isRealoding + "\n bullets:" + _bullets +"\nClients:"+ scoremanager.SC_MAN.players.Count +"\nMeter:"+_meter ;


	}
}
