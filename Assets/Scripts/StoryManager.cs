﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {

	public Sprite[] pages;
	public AudioClip[] narrators;
	public Image image;

	private int pageNumber;

	void Start () {
		pageNumber = 0;
		changePage ();
	}

	void Update () {
		if (!audio.isPlaying) {
			pageNumber ++;
			if (pageNumber < pages.Length) {
				changePage ();
			} else if (pageNumber == pages.Length) {
				Application.LoadLevel ("main");
			} 
		}
	}

	private void changePage() {
		audio.clip = narrators [pageNumber];
		audio.Play ();
		image.sprite = pages [pageNumber];
	}
}
