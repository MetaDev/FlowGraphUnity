using Generator;
using Data;
using UniRx;
using UnityEngine;
using Data.Noise;
using UniRx.Diagnostics;
using AForge.Math;
using Graph.Propagator;

namespace Graph
{
    class GraphConfig : MonoBehaviour
    {
        public Texture tex;
        public SimpleTree tree;
        public Bush bush;
        public Grass grass;
        public void Draw()
        {
            var sampleTerrain = new GridNode("Position",100);

            var sampleObjects = new GridNode("Position", 20);
            var height100 = new CoherentNoiseNode("Height", frequency: 0.1, amplitude: 4, persistence: 0.3, octaves: 4);
            var height20 = new CoherentNoiseNode("Height", frequency: 0.1, amplitude: 4, persistence: 0.3, octaves: 4);
            var random = new DistributedNoiseNode("Size", sampleObjects.GetSize(), DistributedNoiseNode.DistributionType.UNIFORM, 1, 3);
            var random1 = new DistributedNoiseNode("Type", sampleObjects.GetSize(), DistributedNoiseNode.DistributionType.UNIFORM,0,7);
            var colormap = new SampleMapColorNode(ResourceLoader.LoadColorMatrix(tex, "ColorMap"));
            var block = new BlockGenerator();
            var scale100 = new Scale(new UnityEngine.Vector3(100, 100, 100));
            var scale20 = new Scale(new UnityEngine.Vector3(100, 100, 100));
            var vegetation = new VegetationGenerator(tree, bush, grass);
            var debug = new DebugGenerator();
            scale100.LinkTo(sampleTerrain);
            scale20.LinkTo(sampleObjects);

            var surface = new SurfaceMeshGenerator(resolution:100);
            height100.LinkTo(scale100);
            height20.LinkTo(scale20);
            colormap.LinkTo(sampleTerrain);
            block.LinkTo(scale100, height100, colormap);
            //new Vector3fParameter("Position"), new DoubleParameter("Height"), new DoubleParameter("Size"), new ColorParameter("Color"), new DoubleParameter("Type"))
            vegetation.LinkTo(scale20, height20, random,colormap, random1);
            surface.LinkTo(scale100, colormap, height100);

            Observable.Start(() =>
            {
                sampleObjects.StartOutput();
            });
            Observable.Start(() =>
            {
                sampleTerrain.StartOutput();
            });
            Observable.Start(() =>
            {
                random.StartOutput();
            });
            Observable.Start(() =>
            {
                random1.StartOutput();
            });

            //StartWithSources(sample, random, noise);




        }
        //this method doesnt work
        private void StartWithSources(params SourceNode[] sources)
        {
            foreach (SourceNode source in sources)
            {
                Debug.Log(source);
                Observable.Start(() =>
                {
                    source.StartOutput();
                });
            }
        }
    }
}
