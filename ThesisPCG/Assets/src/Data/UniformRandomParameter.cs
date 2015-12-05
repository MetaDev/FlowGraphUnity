using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;
using Graph;
using Graph.Parameters;
using UniRx;
using System;
using System.Collections.Generic;

namespace Data
{
	public class UniformRandomParameter : SourceNode
    {
        DoubleParameter OutputType = new DoubleParameter("RandomDouble");
        CryptoRandomSource rn;
        double StartRange;
        double EndRange;
        public UniformRandomParameter (double startRange, double endRange) : base("Uniform")
		{
             rn = new CryptoRandomSource();
            this.StartRange = startRange;
            this.EndRange = endRange;
            OutputType.SetValue(StartRange + (StartRange - EndRange) * rn.NextDouble());
        }

        public override Parameter PortParameter()
        {
            return OutputType;
        }


       public override IObservable<Parameter> AsObservable()
        {
            if (this.ObservableSource == null)
            {
                var t = new DoubleParameter("RandomDouble");
                ObservableSource = Observable.Create<Parameter>((observer) =>
                {
                    t.SetValue(StartRange + (StartRange - EndRange) * rn.NextDouble());
                    observer.OnNext(t);
                    return Disposable.Empty;
                }).Publish();
            }
           
            return ObservableSource;
        }

        public override IEnumerable<Parameter> StreamParameter()
        {
            throw new NotImplementedException();
        }
    }

}