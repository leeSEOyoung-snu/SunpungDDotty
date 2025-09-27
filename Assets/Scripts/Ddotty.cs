using System;
using UnityEngine;

[Serializable]
public class Ddotty
{
	public int id;
	public Sprite sprite;
	public float scale;

	public Ddotty()
	{
		sprite = Resources.LoadAll<Sprite>("DdottySheet")[id];
	}
}