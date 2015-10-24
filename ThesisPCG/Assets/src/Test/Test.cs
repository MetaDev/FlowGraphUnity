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
		
	}

	void nodeTest ()
	{
		int t = Node.NodeType.DATA;
	}

	void parameterTest ()
	{

		IntegerParameter par = new IntegerParameter ("test");
		Parameter par2 = new IntegerParameter ("test");

		par.SetValue (2);
		//par2.setValue(2) -> not possible

		//true
		Debug.Log (par2.IsType<IntegerParameter> ());

	}

	void test3 ()
	{

	}
	// Update is called once per frame
	void Update ()
	{

	}


}
