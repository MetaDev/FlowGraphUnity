using System;
using Graph;
using UnityEngine;
using Graph.Parameters;
using System.IO;

namespace Data
{
    public class BitmapSourceNode : SourceNode
	{
		
		String FilePath;
        Parameter OutputType = new ColorMapParameter("Colormap",1,1);

        public override Parameter[] LoadParameters ()
		{
            ColorMapParameter[] outColorMap = new ColorMapParameter[1];
            byte[] test = File.ReadAllBytes (this.FilePath);
			Texture2D image = new Texture2D (1, 1);
			image.LoadImage (test);
			Color[] ColorMapLine = image.GetPixels ();
            Debug.Log(ColorMapLine[1].g);
            outColorMap[0] = new ColorMapParameter("Colormap", image.height, image.width);
            for (int i= 0; i < image.height;i++) {
				for (int j = 0; j < image.width; j++) {
                    //Debug.Log(i + " "+j);
                    outColorMap[0].SetValue(i, j,  ColorMapLine [i * image.height + j]);
				}
			}
            Debug.Log(outColorMap[0].GetValue(2, 3).g);
            return outColorMap;
		}

        public override Parameter GetOutputParameterType()
        {
            return OutputType;
        }

        public BitmapSourceNode (string filePath) : base ("Bitmap Source Node", 1)
		{
			this.FilePath = filePath;
		
		}
	

	}
}
