using UnityEngine;

namespace Assets.Code
{
    public class WedgeTest : MonoBehaviour
    {
        private Wedge wedge;

        [SerializeField]
        private float wedgeStart = -15;
        [SerializeField]
        private float wedgeEnd = 15;
        [SerializeField]
        private GameObject target;

        private void Start()
        {
            wedge = new Wedge(new Vector2(transform.forward.x, transform.forward.z),
                wedgeStart, wedgeEnd);
        }

        private void Update()
        {
            wedge = new Wedge(new Vector2(transform.forward.x, transform.forward.z),
                wedgeStart, wedgeEnd);
            var targetDirection = target.transform.position - transform.position;

            if (wedge.IsWithin(new Vector2(targetDirection.x, targetDirection.z),
                out float relevant))
                DebugText.Set(string.Format("With In: {0}", relevant));
            else
                DebugText.Set(string.Format("Outside Of: {0}", relevant));
        }
    }
}
