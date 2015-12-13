using UnityEngine;
namespace Generator
{
    class Bush : Block
    {
        public override void SetScale(Vector3 scale)
        {
            base.SetScale(new Vector3(1, scale.y, 1));
        }
    }
}
