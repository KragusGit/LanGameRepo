using UnityEngine;
using System.Collections;

public class score : MonoBehaviour {
	private int scores;
	private GameObject ScoreGUI;
	// Use this for initialization
	void Start () {
		ScoreGUI = scoremanager.SC_MAN.GUIOBJ;
		hideGui();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			showGui();
		}
		if (Input.GetKeyUp (KeyCode.Tab)) {
			hideGui();
		}
	}
	public int GetScore(){
		return scores;
	}

	public void SetScore(int scoreValue){
		scores = scoreValue;
	}

	public void AddScore(int scoreValue){
		SetScore (scores + scoreValue);
	}

	public void showGui(){
		ScoreGUI.SetActive(true);
	}
	public void hideGui(){
		ScoreGUI.SetActive(false);
	}

}
