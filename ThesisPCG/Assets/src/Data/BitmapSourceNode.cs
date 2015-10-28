using System;
using Graph;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;
using System.Threading.Tasks;
using Graph.Parameters;
using UniRx;
using System.IO;
using System.Linq;

namespace Data
{
	public class BitmapSourceNode : SourceNode
	{
		
		String FilePath;

		public override void Complete ()
		{
			byte[] test = File.ReadAllBytes (this.FilePath);
			Texture2D image = new Texture2D (1, 1);
			image.LoadImage (test);
			Color[] ColorMapLine = image.GetPixels ();
			Color[,] ColorMap = new Color[image.height, image.width];
			foreach (int i in Enumerable.Range(0,image.height)) {
				foreach (int j in Enumerable.Range(0,image.width)) {
					ColorMap [i, j] = ColorMapLine [i * image.height + j];
				}
			}
			var par = new ColorMapParameter (this.OutputParameter.Name);
			par.SetValue (ColorMap);
			this.OutputParameterSequence.Add (par);
		}




		public BitmapSourceNode (string filePath) : base ("Bitmap Source Node", 1, new ColorMapParameter ("color"))
		{
			this.FilePath = filePath;
		
		}
	

	}
}
