using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

	public void HideSplash() {
		FindObjectOfType<GameManager>().LoadScene ("Menu");
	}
}
