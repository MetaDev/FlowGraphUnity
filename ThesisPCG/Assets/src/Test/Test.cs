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
using Graph.Parameters;


public class Test : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		IntegerParameter par = new IntegerParameter ("test");
		Parameter par2 = new IntegerParameter ("test");
		//par2.SetValue ("2");
		Debug.Log (par2.IsType<IntegerParameter> ());

		int t = Node.NodeType.DATA;
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
