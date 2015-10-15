using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;
using System.Collections.Generic;


public class test : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		test2 ();
	}

	void test2 ()
	{
		Task t = new Task (() => Debug.Log ("test"));
		t.Start ();
	}

	void test1 ()
	{
		// create an array with 1000 random values
		double[] samples = SystemRandomSource.Doubles (1000, 100);

		// now overwrite the existing array with new random values
		SystemRandomSource.Doubles (samples, 1255);

		// we can also create an infinite sequence of random values:
		IEnumerable<double> sampleSeq = SystemRandomSource.DoubleSequence ();

		// take a single random value
		System.Random rng = SystemRandomSource.Default;
		double sample = rng.NextDouble ();
		decimal sampled = rng.NextDecimal ();
		Debug.Log (sample);
	}

	void test3 ()
	{
		
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
