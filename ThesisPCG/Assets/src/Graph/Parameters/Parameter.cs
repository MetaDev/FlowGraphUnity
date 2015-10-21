using System;
using UnityEngine;

//this class represents the port of a node

namespace Graph
{
	//a parameter exposes a dynamic property to be set and read
	//the type of the parameter is the type of the first set value
	//this is a wrapper because type checking is not enough, some values also have to keep in which iteration they were calculated
	//it can only be altered by its owner node
	public abstract class Parameter
	{
		
		public String Name {
			get { return Name; } 
			set {
				this.Name = value;
			}
		}

		protected object Value {
			get { return Value; }
			set { this.Value = value; }
		}


		public Parameter (string name)
		{
			this.Name = name;
		}

		public bool Match (Parameter other)
		{
			return other.GetType () == this.GetType () && other.Name == this.Name;
		}

		public bool IsType<T> () where T :Parameter
		{
			return this is T;
		}

		//this will be used for backtracking later in edit propagation
		// a blackbox parameters is processed inside the generator node and the process' result is assigned to a generators result propery
		//a whitebox parameter is immutable and is directly linked to the generators result property
		//if a parameters weight as input is a scalar that reflects it's amount of influence in the output it is white box
		//data can be blackbox too, for example a simulated population that output its varying properties based on input parameters
		/*	public enum Type
		{
			BLACKBOX,
			WHITEBOX
		}*/

	
	}
}

