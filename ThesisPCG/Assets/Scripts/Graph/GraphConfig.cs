using Generator;
using Data;
using UniRx;
using UnityEngine;
using Data.Noise;
using UniRx.Diagnostics;
using AForge.Math;
using Graph.Propagator;
using System.Collections.Generic;

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

            //Graph configuration

            //Sources

            //samples with resolution
            var resolutionSamples = new GridNode("Position", 100);

            //vegetation random sources
            var randomForObjectSize = new DistributedNoiseNode("Size", resolutionSamples.GetSize(), DistributedNoiseNode.DistributionType.UNIFORM, 1, 4);
            var randomForObjectSampleFilter = new DistributedNoiseNode("Bound", resolutionSamples.GetSize(), DistributedNoiseNode.DistributionType.UNIFORM, 0, 15);
            var randomForObjectType = new DistributedNoiseNode("Type", resolutionSamples.GetSize(), DistributedNoiseNode.DistributionType.UNIFORM, 0, 10);

            //Propagators

            //filters
            var filterObjectPostions = new Filter("Position", 1f);
            filterObjectPostions.SetSources(resolutionSamples, randomForObjectSampleFilter);

            //Scales for position of terrain and objects based on resolution
            var scaleTerrainPosition = new Scale(new UnityEngine.Vector3(100, 100, 100));
            scaleTerrainPosition.SetSources(resolutionSamples);

            var scaleObjectPosition = new Scale(new UnityEngine.Vector3(100, 100, 100));
            scaleObjectPosition.SetSources(filterObjectPostions);

            //perline noise for height of terrain an objects
            var heightTerrain = new CoherentNoiseNode("Height", frequency: 0.1, amplitude: 4, persistence: 0.3, octaves: 4);
            heightTerrain.SetSources(scaleTerrainPosition);

            var heightObjects = new CoherentNoiseNode("Height", frequency: 0.1, amplitude: 4, persistence: 0.3, octaves: 4);
            heightObjects.SetSources(scaleObjectPosition);

            //read color from bitmap using sample
            var colormap = new SampleMapColorNode(ResourceLoader.LoadColorMatrix(tex, "ColorMap"));
            colormap.SetSources(resolutionSamples);
            var colormapObjects = new SampleMapColorNode(ResourceLoader.LoadColorMatrix(tex, "ColorMap"));
            colormapObjects.SetSources(filterObjectPostions);

            //Generators

            //geometry
            var block = new BlockGenerator();
            block.SetSources(scaleTerrainPosition, heightTerrain, colormap);
            var vegetation = new VegetationGenerator(tree, bush, grass);
            vegetation.SetSources(scaleObjectPosition, heightObjects, randomForObjectSize, colormapObjects, randomForObjectType);

            //to console
            //var debug = new DebugGenerator();
            //debug.SetSources(colormapObjects);

            //Graph evaluation

            //start sources
            Observable.Start(() =>
            {
                resolutionSamples.StartOutput();
            });
            Observable.Start(() =>
            {
                randomForObjectSize.StartOutput();
            });
            Observable.Start(() =>
            {
                randomForObjectType.StartOutput();
            });
            Observable.Start(() =>
            {
                randomForObjectSampleFilter.StartOutput();
            });






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
