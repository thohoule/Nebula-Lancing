using UnityEngine;

namespace Assets.Code
{
    public class Parallax : MonoBehaviour
    {
        private new Renderer renderer;
        private Vector2 offset;

        [SerializeField, Range(0, 1)]
        private float textureSpeed;

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            offset = renderer.material.GetTextureOffset("_MainTex");
        }

        [ExecuteInEditMode]
        private void Update()
        {
            offset += new Vector2(0, textureSpeed * Time.deltaTime);
            renderer.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
