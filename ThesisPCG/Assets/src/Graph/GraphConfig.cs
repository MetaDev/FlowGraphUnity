using Generator;
using Data;
using UniRx;
using UnityEngine;
using System;

namespace Graph
{
    class GraphConfig : MonoBehaviour
    {
        public SpriteRenderer test;
       public void Draw()
        {

            var sample = new GridNode(100, 100);
            TextureSourceNode tst = new TextureSourceNode(test.sprite.texture);
            var colormap = new SampleMapColorNode(tst);
            var block = new BlockGenerator();
            colormap.LinkTo(sample);
            block.LinkTo(sample, colormap);
          
            Observable.Start(() => sample.Push());
           // var delay = Observable.Empty<Unit>().Delay(TimeSpan.FromSeconds(3));
            //var draw = delay.Concat<Unit>(Observable.Start(() => sample.Push()));
           // draw.Subscribe();


        }
    }
}
