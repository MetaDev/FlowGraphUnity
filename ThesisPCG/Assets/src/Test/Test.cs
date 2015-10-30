using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;
using System.Collections.Generic;
using AForge.Math;
using MathNet.Numerics;
using Graph;
using System;
using UnityEngine.Networking;
using UniRx;
using Graph.Parameters;
using System.Runtime.Remoting.Messaging;
using Generator;
using Data;


public class Test : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		nodeTest ();
	}

	void nodeTest ()
	{
		var sample = new SampleNode (5, 5);
		//var colormap = new SampleMapColorNode ();
		var block = new BlockGenerator ();
		var mapsource = new BitmapSourceNode ("Assets/Resources/bitmaps/bitmaptest.png");






//		colormap.LinkTo (sample);
//
//		colormap.LinkTo (mapsource);
//
//		block.LinkTo (colormap);

		block.LinkTo (sample);


		mapsource.Push ();
		sample.Push ();
	}

	void parameterTest ()
	{

		IntegerParameter par = new IntegerParameter ("test");
		Parameter par2 = new IntegerParameter ("test");

		par.SetValue (2);
		//par2.setValue(2) -> not possible

		//true
		Debug.Log (par2.Is<IntegerParameter> ());

	}

	void AForgeTest ()
	{

	}



}
