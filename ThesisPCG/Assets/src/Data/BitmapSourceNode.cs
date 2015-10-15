using System;
using Graph;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;
using System.Threading.Tasks;

namespace Data
{
	public class BitmapSourceNode : SourceNode
	{
		Color[][] ColorMap;
		String FilePath;

		public void LinkTo (ITargetNode target)
		{
			throw new NotImplementedException ();
		}

	


		public override Task Completion ()
		{
			return new Task (() => {
				Texture2D LevelBitmap = Resources.Load (this.FilePath) as Texture2D;
				Color[] ColorMapLine = LevelBitmap.GetPixels ();
				//TODO
			}
			);
		}




		public BitmapSourceNode (string filePath)
		{
			this.FilePath = filePath;
		
		}
	

	}
}
