using System;
using Graph;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;
using System.Threading.Tasks;
using Graph.Parameters;
using UniRx;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Data
{
	public class BitmapSourceNode : SourceNode
	{
		
		String FilePath;
		static ColorMapParameter outColorMap = new ColorMapParameter ("colormap");

		public override void LoadParameters (Parameter[] parameters)
		{
			byte[] test = File.ReadAllBytes (this.FilePath);
			Texture2D image = new Texture2D (1, 1);
			image.LoadImage (test);
			Color[] ColorMapLine = image.GetPixels ();
			Color[,] ColorMapArray = new Color[image.height, image.width];
			foreach (int i in Enumerable.Range(0,image.height)) {
				foreach (int j in Enumerable.Range(0,image.width)) {
					ColorMapArray [i, j] = ColorMapLine [i * image.height + j];
				}
			}
			outColorMap.SetValue (ColorMapArray);
		}




		public BitmapSourceNode (string filePath) : base ("Bitmap Source Node", 1, outColorMap)
		{
			this.FilePath = filePath;
		
		}
	

	}
}
