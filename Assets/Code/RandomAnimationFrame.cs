using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(Animator))]
    public class RandomAnimationFrame : MonoBehaviour
    {
        [SerializeField]
        private float value;

        private void Awake()
        {
            var animator = GetComponent<Animator>();

            value = Random.Range(0.0f, 1.0f);
            animator.Play(0, 0, value);
            //animator.SetBool(0, true);
        }
    }
}
