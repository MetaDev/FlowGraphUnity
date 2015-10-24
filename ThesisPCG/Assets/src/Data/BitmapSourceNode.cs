using System;
using Graph;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;
using System.Threading.Tasks;
using Graph.Parameters;
using UniRx;

namespace Data
{
	public class BitmapSourceNode : SourceNode , ISourceNode<ColorMapParameter>
	{
		Color[][] ColorMap;
		String FilePath;



		public new UniRx.IObservable<ColorMapParameter> AsObservable ()
		{
			return Observable.Return (GetOutputParameter ());
		}

		public new ColorMapParameter GetOutputParameter ()
		{
			return base.GetOutputParameter<ColorMapParameter> ();
		}


		public override void  Complete ()
		{
			
			this._Process = new Task (() => {
				Texture2D LevelBitmap = Resources.Load (this.FilePath) as Texture2D;
				Color[] ColorMapLine = LevelBitmap.GetPixels ();
				//TODO
			}
			);
			Process ().Start ();
		}




		public BitmapSourceNode (string name, string filePath, Parameter outputParameter) : base (name, outputParameter)
		{
			this.FilePath = filePath;
		
		}
	

	}
}
