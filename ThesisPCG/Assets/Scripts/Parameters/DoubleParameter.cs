using Graph.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph.Parameters
{
    public class DoubleParameter : Parameter, IParameter<double>
    {
        public DoubleParameter(string name, double value=default(double)) : base(name,value)
        {
        }
       

        public  double GetValue()
        {
            return this.GetValue<double>();
        }

      
    }
}
