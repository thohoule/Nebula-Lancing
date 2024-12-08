using UnityEngine;


namespace Assets.Code
{
    public class Parallax2D : MonoBehaviour
    {
        private new Renderer renderer;
        private Vector2 offset;

        [SerializeField]
        private GameObject target;
        [SerializeField]
        private float width;
        [SerializeField]
        private float height;

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            offset = renderer.material.GetTextureOffset("_MainTex");
        }

        private void Update()
        {
            float x = target.transform.position.x / width;
            float y = target.transform.position.z / height;

            offset = new Vector2(x, y);
            renderer.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
