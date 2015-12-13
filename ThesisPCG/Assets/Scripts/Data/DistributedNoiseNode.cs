using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;
using Graph;
using Graph.Parameters;
using UniRx;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    class DistributedNoiseNode : FiniteSourceNode
    {
        CryptoRandomSource rn;
        double StartRange;
        double EndRange;
        private DistributionType _DistributionType;
        ContinuousUniform cu;
        Cauchy cau;
        private int Size;
        public enum DistributionType
        {
            UNIFORM, CAUCHY
        }
        public DistributedNoiseNode(string OutputName,int size, DistributionType distributionType, double startRange= 0, double endRange = 1) : base("Noise: "+ OutputName, new DoubleParameter(OutputName))
        {
            rn = new CryptoRandomSource();
            cu = new ContinuousUniform(startRange, endRange, rn);
            cau = new Cauchy(0.5, 0.5, rn);
            this._DistributionType = distributionType;
            this.StartRange = startRange;
            this.EndRange = endRange;
            this.Size = size;
        }

       


        private double GetRandom()
        {
            switch (_DistributionType)
            {
                case DistributionType.UNIFORM:
                    return cu.Sample();
                case DistributionType.CAUCHY:
                    return ScaleFromUnitToRange(cau.Sample());


            }
            return 0;
        }
        private double ScaleFromUnitToRange(double unit)
        {
            return StartRange + (StartRange - EndRange) * unit;
        }
      

        public override IEnumerable<Parameter> CreateSequence()
        {
            var list = new List<Parameter>();
            for (int i = 0; i < Size; i++)
            {
                var t = new DoubleParameter(this.OutputParameter().Name, GetRandom());
                list.Add(t);
            }
            return list;
        }

        public override int[] GetShape()
        {
            return new int[] { Size };
        }
    }

}