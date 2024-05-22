using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bowling : MonoBehaviour {
	//触发重载场景的变换位置，其实只用到了Y轴
	public Transform ReSetZone;
	// Use this for initialization
	void ResetScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	void Update () {
		if(this.gameObject.transform.position.y<ReSetZone.transform.position.y) 
		{
			ResetScene();
		}
	}

}
