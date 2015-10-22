using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;
using System.Collections.Generic;
using AForge.Math;
using Graph;
using System;
using UnityEngine.Networking;
using UniRx;


public class Test : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Parameter par = new IntegerParameter ("test");

	}

	void test2 ()
	{
		Task t = new Task (() => Debug.Log ("test"));
		t.Start ();
	}

	void test1 ()
	{



	}

	void test3 ()
	{

	}
	// Update is called once per frame
	void Update ()
	{

	}

	public static void Main (string[] args)
	{
		
		new Test ().test1 ();
	}
}
