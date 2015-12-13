using Graph;
using System;
using System.Collections.Generic;
using Graph.Parameters;
using UniRx;
using UnityEngine;

namespace Generator
{
    class VegetationGenerator : TargetNode
    {
        private SimpleTree _Tree;
        private Bush _Bush;
        private Grass _Grass;
        public VegetationGenerator(SimpleTree tree, Bush bush, Grass grass) : base("Vegetation", new Vector3fParameter("Position"), new DoubleParameter("Height"), new DoubleParameter("Size"), new ColorParameter("Color"), new DoubleParameter("Type"))
        {
            this._Tree = tree;
            this._Bush = bush;
            this._Grass = grass;
        }
        public override void Sink(IObservable<IList<Parameter>> observable)
        {
            observable.Subscribe((parameters) =>
            {
                Vector3fParameter position = TargetNode.GetParameterFromList("Position", parameters).As<Vector3fParameter>();
                ColorParameter color = TargetNode.GetParameterFromList("Color", parameters).As<ColorParameter>();
                DoubleParameter height = TargetNode.GetParameterFromList("Height", parameters).As<DoubleParameter>();
                DoubleParameter size = TargetNode.GetParameterFromList("Size", parameters).As<DoubleParameter>();
                DoubleParameter type = TargetNode.GetParameterFromList("Type", parameters).As<DoubleParameter>();
                int typeInt = (int) Math.Round(type.GetValue() * 3);
                Block obj=null;
               
                switch (typeInt)
                {
                    case 0:
                        obj = _Tree.CreateNew();
                        break;
                    case 1:
                        obj = _Bush.CreateNew();
                        break;
                    default:
                        obj = _Grass.CreateNew();
                        break;

                }
   
                obj.SetColor(color.GetValue());
                var vectPos = new Vector3(position.GetValue().x, (float)(height.GetValue() + (size.GetValue() / 2)), position.GetValue().z);
                var vectSize = new Vector3((float)size.GetValue(), (float)size.GetValue(), (float)size.GetValue());
                obj.SetScale(vectSize);
                obj.SetPosition(vectPos);
                
            });
        }
    }
}
