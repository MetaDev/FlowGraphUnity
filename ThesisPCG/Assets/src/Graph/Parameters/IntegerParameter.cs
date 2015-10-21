using System;
using UnityEngine;

namespace Graph
{
	//a parameter exposes a dynamic property to be set and read
	//the type of the parameter is the type of the first set value
	//this is a wrapper because type checking is not enough, some values also have to keep in which iteration they were calculated
	//it can only be altered by its owner node
	public class IntegerParameter : Parameter
	{
		public int GetValue ()
		{
			return (int)Value;
		}

		public IntegerParameter (string name) : base (name)
		{
		}

		public void SetValue (int value)
		{
			this.Value = value;
		}

	
	}
}

