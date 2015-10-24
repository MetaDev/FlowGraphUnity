using System;
using UnityEngine;

//this class represents the port of a node
using System.Threading.Tasks;
using AssemblyCSharp;
using System.Diagnostics;

namespace Graph.Parameters
{
	//a parameter exposes a dynamic property to be set and read
	//the type of the parameter is the type of the first set value
	//this is a wrapper because type checking is not enough, some values also have to keep in which iteration they were calculated
	//it can only be altered by its owner node

	public abstract class Parameter
	{
		private class ParameterBase : IParameter<object>
		{
		
			private String _Name { get; set; }

			private object _Value{ get; set; }


			public ParameterBase (string name)
			{
				this._Name = name;
			}

			public string Name ()
			{
				return _Name;
			}

			public  object GetValue ()
			{
				return _Value;
			}

			public  void SetValue (object value)
			{
				this._Value = value;
			}



	
		}

		private ParameterBase BaseClassInstance;

		//This is where the casting magic happens
		//maybe out some error handling here, but not neccesary
		protected T GetValue<T> ()
		{
			return (T)BaseClassInstance.GetValue ();
		}

		protected void SetValue<T> (T value)
		{
			BaseClassInstance.SetValue ((T)value);

		}

		public Parameter (string name)
		{
			BaseClassInstance = new ParameterBase (name);
		}
		//check if they are the same class and name
		public bool Match (Parameter other)
		{
			return other.GetType () == this.GetType () && other.Name () == this.Name ();
		}

		public String Name ()
		{
			return BaseClassInstance.Name ();
		}

		public  bool IsType<T> () where T : Parameter
		{
			return this.GetType () == typeof(T);
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

