using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Data.Noise
{
    interface CoherentNoise
    {
        double Function1D(double point);
        double Function2D(Vector2 point);
        double Function3D(Vector3 point);
    }
}
