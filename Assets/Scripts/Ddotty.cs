using System;
using UnityEngine;

[Serializable]
public class Ddotty
{
	[SerializeField] private int id;
	[SerializeField] private float scale;

	public int Id => id;
	public float Scale => scale;
}