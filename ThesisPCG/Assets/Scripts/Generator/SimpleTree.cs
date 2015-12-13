using UnityEngine;

namespace Generator
{
    //TODO
    //add method to change crwon and stump size
    
    class SimpleTree : Block
    {
        public Block Crown;
        public Block Stump;
        public override void SetColor(Color color)
        {
            Crown.SetColor(color);
            Stump.SetColor(color);
          
        }
        public override void SetScale(Vector3 scale)
        {
            
            var crownPos = Crown.transform.position;
            //raise the crwon such that its resting position stays the same
            Crown.transform.localPosition+=new Vector3(0,  (scale.y-1)/2, 0);
            Crown.SetScale(scale);
            //Stump.SetScale(scale);
        }

    }
}
