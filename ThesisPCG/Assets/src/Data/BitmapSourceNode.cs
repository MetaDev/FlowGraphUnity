using System;
using Graph;
using UnityEngine;
using Graph.Parameters;
using System.Collections.Generic;

namespace Data
{
    public class TextureSourceNode : ColorMapParameter
	{
		
		

        public TextureSourceNode (Texture2D image) : base ("Bitmap")
		{
        
            Color[] ColorMapLine = image.GetPixels();
            Color[,] value = new Color[image.width, image.height];
            for (int i = 0; i < image.height; i++)
            {
                for (int j = 0; j < image.width; j++)
                {
                    //Debug.Log(ColorMapLine[i * image.height + j]);
                    value[i, j]= ColorMapLine[i * image.height + j];
                }
            }
            this.SetValue(value);
        }


    }
}
