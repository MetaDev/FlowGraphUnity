using System;
using Graph;
using UnityEngine;
using Graph.Parameters;
using System.Collections.Generic;

namespace Data
{
    public class ResourceLoader 
	{
		
		

        public static Color[,] LoadColorMatrix (Texture texture, String mapName) 
		{
            //settings to ensure that each pixel is read correctly
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Trilinear;

            Color[] ColorMapLine = (texture as Texture2D).GetPixels();
            Color[,] value = new Color[texture.width, texture.height];
            for (int i = 0; i < texture.height; i++)
            {
                for (int j = 0; j < texture.width; j++)
                {
                    value[i, j]= ColorMapLine[i * texture.height + j];
                }
            }
            return value;
        }


    }
}
