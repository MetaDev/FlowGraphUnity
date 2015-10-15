using System;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
namespace DataLayer
{
	public abstract class Source 
	{
		String Name;
		Attribute SourceAttribtue;
		public Source (string name)
		{
			this.Name = name;


			// create a parametrized distribution instance
			var gamma = new Gamma(2.0, 1.5);

			// distribution properties
			double mean = gamma.Mean;
			double variance = gamma.Variance;
			double entropy = gamma.Entropy;

			// distribution functions
			double a = gamma.Density(2.3); // PDF
			double b = gamma.DensityLn(2.3); // ln(PDF)
			double c = gamma.CumulativeDistribution(0.7); // CDF

			// non-uniform number sampling
			double randomSample = gamma.Sample();
		}
		

	}
}
