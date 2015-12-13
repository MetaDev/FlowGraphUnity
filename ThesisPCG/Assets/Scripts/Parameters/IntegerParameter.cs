using System;
using UnityEngine;
using System.Threading.Tasks;

namespace Graph.Parameters
{
	//a parameter exposes a dynamic property to be set and read
	//the type of the parameter is the type of the first set value
	//this is a wrapper because type checking is not enough, some values also have to keep in which iteration they were calculated
	//it can only be altered by its owner node
	public class IntegerParameter : Parameter , IParameter<int>
	{
		

		public IntegerParameter (string name, int value=default(int)) : base (name,value)
		{
		}

		
		public int GetValue ()
		{
			return this.GetValue<int> ();
		}

		
	
	}
}

