﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour {

	[SerializeField]
	int level = 1;

	[SerializeField]
	float shapesRadius = 4;

	[SerializeField]
	float colorsRadius = 4;

	[SerializeField]
	GameObject colorSphere;

	[SerializeField]
	GameObject itemWrapperPrefab;

	[SerializeField]
	GameObject shapeParent;

	[SerializeField]
	Sprite[] shapes;

	[SerializeField]
	Color[] colors;

	GameObject shapesRing;
	GameObject colorsRing;

	GameObject wrappers;
	Animator wrappersAnimator;
	ScoreCounter scoreCounter;

	Spawner spawner;
	SoundsManager soundsManager;
	GameManager gameManager;
	GameControlls gameControlls;

	void Awake() {
		wrappers = GameObject.Find ("Wrappers");
		wrappersAnimator = wrappers.GetComponent<Animator> ();

		spawner = GameObject.FindObjectOfType<Spawner> ();
		soundsManager = GameObject.FindObjectOfType<SoundsManager> ();
		scoreCounter = GameObject.FindObjectOfType<ScoreCounter> ();
		gameManager = GameObject.FindObjectOfType<GameManager> ();
		gameControlls = GameObject.FindObjectOfType<GameControlls> ();

		shapesRing = new GameObject ();
		shapesRing.AddComponent<WrapperController> ();
		shapesRing.transform.SetParent(wrappers.transform);

		colorsRing = new GameObject ();
		colorsRing.AddComponent<WrapperController> ();
		colorsRing.transform.SetParent(wrappers.transform);

		GameManager.onLevelIncremented += IncrementLevel;
	}

	// Use this for initialization
	void Start () {
		// reset level
		GameManager.Level = 1;

		print ("game area start");

		// start game
		StartLevel ();
	}

	void StartLevel() {
		if (soundsManager) {
			soundsManager.Ring ();
		}

		Vector2 shapesDefaultPosition = new Vector2 (shapesRadius, 0);
		Vector2 colorsDefaultPosition = new Vector2 (colorsRadius, 0);

		// add objects
		for (int i = 0; i < GetItemCount(); i++) {
			float rotationOffset = 360f / GetItemCount() * i;

			CreateShape (shapesDefaultPosition, rotationOffset, shapes[i]);
			CreateColor (colorsDefaultPosition, rotationOffset, colors[i]);
		}

		// random default rotation for wrappers
		StartCoroutine (InitRotations ());

		StartCoroutine (ShowAll ());
	}

	void StartSpawning() {
		// start spawning circles
		spawner.StartSpawning();
	}

	void StopLevel() {
		if (soundsManager) {
			soundsManager.Ring ();
		}

		spawner.StopSpawning();

		StartCoroutine (HideAll ());
	}

	void IncrementLevel() {
		StartCoroutine (ReloadAll());
	}

	IEnumerator ReloadAll() {
		StopLevel ();

		// wait until all elemnts are removed
		yield return new WaitForSeconds (2.5f);

		StartLevel ();
	}

	IEnumerator ShowAll() {
		// zoom in
		wrappersAnimator.SetTrigger("Show");

		// wait for animation
		yield return new WaitForSeconds (1);

		// show shapes and colors
		foreach (Transform child in shapesRing.transform) {
			Animator shapeAnimator = child.gameObject.GetComponentInChildren<Animator> ();
			shapeAnimator.SetTrigger ("Show");
		}

		foreach (Transform child in colorsRing.transform) {
			Animator shapeAnimator = child.gameObject.GetComponentInChildren<Animator> ();
			shapeAnimator.SetTrigger ("Show");
		}

		// wait for animation
		yield return new WaitForSeconds (1);

		StartSpawning ();
	}

	IEnumerator HideAll() {
		foreach (Transform child in shapesRing.transform) {
			Animator shapeAnimator = child.GetComponentInChildren<Animator> ();
			shapeAnimator.SetTrigger ("Hide");
		}

		foreach (Transform child in colorsRing.transform) {
			Animator shapeAnimator = child.GetComponentInChildren<Animator> ();
			shapeAnimator.SetTrigger ("Hide");
		}

		// wait for animation
		yield return new WaitForSeconds (1);

		// zoom out
		wrappersAnimator.SetTrigger("Hide");

		// wait for animation
		yield return new WaitForSeconds (1);

		// remove all objects
		foreach (Transform child in shapesRing.transform) {
			Destroy (child.gameObject);
		}

		foreach (Transform child in colorsRing.transform) {
			Destroy (child.gameObject);
		}
	}

	void CreateShape(Vector2 defaultPosition, float rotation, Sprite shapeSprite) {
		// wrapper for one item
		GameObject itemWrapper = Instantiate(itemWrapperPrefab);

		itemWrapper.transform.SetParent (shapesRing.transform);
		itemWrapper.transform.localPosition = new Vector2 (0, 0);

		// shape item
		GameObject shapeItem = Instantiate (shapeParent, itemWrapper.transform) as GameObject;

		// add shape image
		shapeItem.GetComponent<Shape>().SetShape(shapeSprite);

		shapeItem.transform.localPosition = defaultPosition;

		// rotation offset
		itemWrapper.GetComponent<ItemWrapper>().SetRotation(rotation);
	}

	void CreateColor(Vector2 defaultPosition, float rotation, Color color) {
		// wrapper for one item
		GameObject itemWrapper = new GameObject ();

		itemWrapper.transform.SetParent (colorsRing.transform);
		itemWrapper.transform.localPosition = new Vector2 (0, 0);

		// rotation offset
		itemWrapper.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));

		// color item
		GameObject colorItem = Instantiate (colorSphere, itemWrapper.transform) as GameObject;
		colorItem.transform.localPosition = defaultPosition;

		// change color
		SpriteRenderer renderer = colorItem.GetComponent<SpriteRenderer> ();
		renderer.color = color;
	}

	IEnumerator InitRotations() {
		// wait for a while until rotation will be initialized
		yield return new WaitForSeconds(1);

		float rotation = Random.Range (0, 360);

		shapesRing.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));

		float offset = (360 / (GetItemCount() * 2));
		colorsRing.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation + offset));
	}

	public GameObject ShapesRing {
		get {
			return shapesRing;
		}
	}

	public GameObject ColorsRing {
		get {
			return colorsRing;
		}
	}

	public Sprite GetRandomShape() {
		return shapes[Random.Range (0, Mathf.Clamp(GetItemCount(), 0, shapes.Length - 1))];
	}

	public Color GetRandomColor() {
		return colors[Random.Range (0, Mathf.Clamp(GetItemCount(), 0, shapes.Length - 1))];
	}

	int GetItemCount() {
		return (level > 0 ? level : GameManager.Level) + 1;
	}

	public void CheckGameOver() {
		// save score
		Settings.LastScore = scoreCounter.Score;

		// hide game area
		StartCoroutine(GameOver());
	}

	IEnumerator GameOver() {
		// stop spawning and movig
		spawner.Pause ();
		gameControlls.Pause ();

		yield return HideAll();

		// change scene
		gameManager.LoadScene ("GameOver");
	}

	public void Pause() {
		spawner.Pause ();
		gameControlls.Pause ();
	}

	public void Resume() {
		spawner.Resume ();
		gameControlls.Resume ();
	}
}
