using System;


namespace DataLayer
{
	public abstract class RandomParameter 

	{
		int StartRange;
		int EndRange;

		public RandomParameter (int startRange, int endRange)
		{
			this.StartRange = startRange;
			this.EndRange = endRange;
		}
	

	}
}
