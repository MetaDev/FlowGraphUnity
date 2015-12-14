using UnityEngine;
namespace Generator
{
    class Bush : Block
    {
        
        public override void SetScale(Vector3 scale)
        {
            base.SetScale(new Vector3(this.transform.localScale.x, scale.y, this.transform.localScale.z));
            transform.localPosition += new Vector3(0, (scale.y - 1) / 2, 0);
        }
    }
}
