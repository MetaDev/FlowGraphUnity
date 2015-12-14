using UnityEngine;

namespace Generator
{
    class Grass : Block
    {
        public override void SetScale(Vector3 scale)
        {
            base.SetScale(new Vector3(this.transform.lossyScale.x, scale.y, this.transform.lossyScale.x));
            transform.localPosition += new Vector3(0, (scale.y - 1) / 2, 0);
        }
    }
}
