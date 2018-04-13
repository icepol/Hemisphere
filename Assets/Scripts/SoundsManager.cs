using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {

	[SerializeField]
	AudioClip explode;

	[SerializeField]
	AudioClip match;

	[SerializeField]
	AudioClip ring;

	[SerializeField]
	AudioClip swap;

	[SerializeField]
	AudioClip top;

	[SerializeField]
	AudioClip spawn;

	[SerializeField]
	AudioClip buttonClick;

	void Awake() {
		DontDestroyOnLoad(this);
	}

	public void Play(AudioClip clip, Vector2? position = null) {
		AudioSource.PlayClipAtPoint (clip, position ?? Vector2.zero);
	}

	public void Explode(Vector2? position = null) {
		Play (explode, position);
	}

	public void Match(Vector2? position = null) {
		Play (match, position);
	}

	public void Ring(Vector2? position = null) {
		Play (ring, position);
	}

	public void Swap(Vector2? position = null) {
		Play (swap, position);
	}

	public void Top(Vector2? position = null) {
		Play (top, position);
	}

	public void Spawn(Vector2? position = null) {
		Play (spawn, position);
	}

	public void ButtonClick(Vector2? position = null) {
		Play (buttonClick, position);
	}
}
