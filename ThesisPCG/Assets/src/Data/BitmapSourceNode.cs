using System;
using Graph;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;
using System.Threading.Tasks;
using Graph.Parameters;
using UniRx;
using System.Security.Policy;

namespace Data
{
	public class BitmapSourceNode : SourceNode 
	{
		
		String FilePath;

		public override void Complete ()
		{
			
			Texture2D LevelBitmap = Resources.Load (this.FilePath) as Texture2D;
			Color[] ColorMapLine = LevelBitmap.GetPixels ();
			Color[,] ColorMap = new Color[LevelBitmap.height, LevelBitmap.width];
			Buffer.BlockCopy (ColorMapLine, 0, ColorMap, 0, ColorMapLine.Length * sizeof(double));
			GetOutputParameter<ColorMapParameter> ().SetValue (ColorMap);
			Debug.Log (ColorMap[0,0]);
		}




		public BitmapSourceNode (string filePath) : base ("Bitmap Source Node", new ColorMapParameter ("color"))
		{
			this.FilePath = filePath;
		
		}
	

	}
}
