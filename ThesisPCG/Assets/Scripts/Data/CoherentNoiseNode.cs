using Graph;
using System.Collections.Generic;
using Graph.Parameters;
using AForge.Math;
using UniRx;


namespace Data.Noise
{
    class CoherentNoiseNode : PropagatorNode
    {

    
        private double Frequency;

        private double Amplitude;
        private double Persistence;

        private int Octaves;

        private PerlinNoise noise;

        private int Dimensions;


        public CoherentNoiseNode(string OutputName, double frequency = 1, int octaves = 1, int amplitude = 1, double persistence = 0.5d, int dimensions = 2) : base("Noisenode: "+ OutputName, new Vector3fParameter[] { new Vector3fParameter("Position") }, new DoubleParameter(OutputName))
        {
           
            this.Amplitude = amplitude;
            this.Octaves = octaves;
            this.Frequency = frequency;
            this.Persistence = persistence;
            this.Dimensions = dimensions;
            noise = new PerlinNoise(Octaves, Persistence, Frequency, Amplitude);
        }




        protected override IObservable<Parameter> Propagate(IObservable<IList<Parameter>> observable)
        {
          return observable.Select((parameters) =>
            {
                var position = TargetNode.GetParameterFromList("Position", parameters).As<Vector3fParameter>();
                return (Parameter) new DoubleParameter(this.OutputParameter().Name, noise.Function2D(position.GetValue().x, position.GetValue().z));
            });
        }
    }
}
