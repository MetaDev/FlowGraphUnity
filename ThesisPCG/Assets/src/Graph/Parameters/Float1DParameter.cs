using Graph.Parameters;
using System;

namespace Graph.Parameters
{
    public class Float1DParameter : Parameter, I1DParameter<float>
    {
        public float[] GetValue()
        {
            throw new NotImplementedException();
        }

        public void SetValue(float[] value)
        {
            throw new NotImplementedException();
        }

        public  float GetValue(int i)
        {
            throw new NotImplementedException();
        }

        public  void SetValue(int i, float value)
        {
            throw new NotImplementedException();
        }

        public Float1DParameter(string name, int size) : base(name)
        {
            this.SetValue(new float[size]);
        }


    }
}
