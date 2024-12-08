using UnityEngine;
using TeaSteep;
using Assets.Code.Gameplay;

namespace Assets.Code.Characters.MockAssets
{
    [RequireComponent(typeof(Animator))]
    public class MockPrepShip : PrefabAsset
    {
        private Animator animator;

        [SerializeField]
        private GameObject modelObject;
        [SerializeField]
        private GameObject smallShipObject;
        [SerializeField]
        private GameObject mediumShipObject;
        [SerializeField]
        private GameObject largeShipObject;

        [SerializeField, ReadOnly]
        private GameObject primaryLoadedWeapon;
        [SerializeField, ReadOnly]
        private GameObject secondaryLoadedWeapon;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        #region Set Ship Props
        public void SetShipByPrep(ShipPrep prep)
        {
            var primaryAttachPoint = prep.GetPrimaryAttachPoint();
            var secondaryAttachPoint = prep.GetSecondaryPoint();

            switch (prep.SelectedShip)
            {
                case 0:
                    SetSmallShip(prep.SmallShip, primaryAttachPoint, 
                        secondaryAttachPoint);
                    break;
                case 1:
                    SetMediumShip(prep.MediumShip, primaryAttachPoint,
                        secondaryAttachPoint);
                    break;
                default:
                    SetLargeShip(prep.LargeShip, primaryAttachPoint,
                        secondaryAttachPoint);
                    break;
            }
        }

        public void SetSmallShip()
        {
            smallShipObject.SetActive(true);
            mediumShipObject.SetActive(false);
            largeShipObject.SetActive(false);
        }

        public void SetSmallShip(ShipInfo info,
            Vector3 primaryAttachPoint, Vector3 secondaryAttachPoint)
        {
            SetSmallShip(info.PrimaryWeapon, info.SecondaryWeapon,
                primaryAttachPoint, secondaryAttachPoint);
        }

        public void SetSmallShip(string primaryAsset, string secondaryAsset,
            Vector3 primaryAttachPoint, Vector3 secondaryAttachPoint)
        {
            SetSmallShip();
            LoadPrimaryWeapon(primaryAsset, primaryAttachPoint);
            LoadSecondaryWeapon(secondaryAsset, secondaryAttachPoint);
        }

        public void SetMediumShip()
        {
            smallShipObject.SetActive(false);
            mediumShipObject.SetActive(true);
            largeShipObject.SetActive(false);
        }

        public void SetMediumShip(ShipInfo info,
            Vector3 primaryAttachPoint, Vector3 secondaryAttachPoint)
        {
            SetMediumShip(info.PrimaryWeapon, info.SecondaryWeapon,
                primaryAttachPoint, secondaryAttachPoint);
        }

        public void SetMediumShip(string primaryAsset, string secondaryAsset,
            Vector3 primaryAttachPoint, Vector3 secondaryAttachPoint)
        {
            SetMediumShip();
            LoadPrimaryWeapon(primaryAsset, primaryAttachPoint);
            LoadSecondaryWeapon(secondaryAsset, secondaryAttachPoint);
        }

        public void SetLargeShip()
        {
            smallShipObject.SetActive(false);
            mediumShipObject.SetActive(false);
            largeShipObject.SetActive(true);
        }

        public void SetLargeShip(ShipInfo info,
            Vector3 primaryAttachPoint, Vector3 secondaryAttachPoint)
        {
            SetLargeShip(info.PrimaryWeapon, info.SecondaryWeapon,
                primaryAttachPoint, secondaryAttachPoint);
        }

        public void SetLargeShip(string primaryAsset, string secondaryAsset,
            Vector3 primaryAttachPoint, Vector3 secondaryAttachPoint)
        {
            SetLargeShip();
            LoadPrimaryWeapon(primaryAsset, primaryAttachPoint);
            LoadSecondaryWeapon(secondaryAsset, secondaryAttachPoint);
        }

        public void LoadPrimaryByPrep(ShipPrep prep)
        {
            LoadPrimaryWeapon(prep.GetCurrent().PrimaryWeapon,
            prep.GetPrimaryAttachPoint());
        }

        public void LoadPrimaryWeapon(string assetName, Vector3 attachPoint)
        {
            if (assetName == "")
                return;

            if (TryGetPrefab(assetName, out WeaponPrefab prefab))
            {
                var instance = Instantiate(prefab);
                var attach = attachPoint + prefab.AttachOffset;
                instance.gameObject.SetParentAndLocals(modelObject, attach);

                setPrimaryWeapon(instance.gameObject);
            }
        }

        private void setPrimaryWeapon(GameObject instance)
        {
            if (primaryLoadedWeapon != null)
                Destroy(primaryLoadedWeapon);

            primaryLoadedWeapon = instance;
        }

        public void LoadSecondaryByPrep(ShipPrep prep)
        {
            LoadSecondaryWeapon(prep.GetCurrent().SecondaryWeapon,
                prep.GetSecondaryPoint());
        }

        public void LoadSecondaryWeapon(string assetName, Vector3 attachPoint)
        {
            if (assetName == "")
                return;


        }
        #endregion

        #region Animations
        public void PlayEnterAnimation()
        {
            animator.SetBool("Enter", true);
        }

        public void PlayTransitionAnimation()
        {
            animator.SetBool("Transition", true);
        }
        #endregion
    }
}
