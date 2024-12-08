using UnityEngine;
using Interlace;
using TeaSteep;
using Assets.Code.Characters;
using Assets.Code;
using Assets.Entities;

namespace Assets.Game.MockAssets
{
    public class MockPrepShip : PrepShipBase
    {
        private const string Enter_Key = "Enter";
        private const string Transition_Key = "Transition";

        private Animator animator;
        private int selectedShip;

        [SerializeField]
        private ShipAsset smallShip;
        [SerializeField]
        private ShipAsset mediumShip;
        [SerializeField]
        private ShipAsset largeShip;

        [SerializeField, ReadOnly]
        private GameObject primaryLoadedWeapon;
        [SerializeField, ReadOnly]
        private GameObject secondaryLoadedWeapon;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        #region Set Ship
        public override void SetShip(PrepEntity prep)
        {
            switch (prep.SelectedShip)
            {
                case 0:
                    setSmallShip();
                    break;
            }

            if (selectedShip != prep.SelectedShip)
            {
                SetPrimary(prep);
                SetSecondary(prep);
            }

            selectedShip = prep.SelectedShip;
        }

        private void setSmallShip()
        {
            smallShip.gameObject.SetActive(true);
            mediumShip.gameObject.SetActive(false);
            largeShip.gameObject.SetActive(false);
        }
        #endregion

        #region Get Ship
        private ShipAsset getShip()
        {
            switch (selectedShip)
            {
                case 0:
                    return smallShip;
                case 1:
                    return mediumShip;
                default:
                    return largeShip;
            }
        }
        #endregion

        #region Set Weapons
        public override void SetPrimary(PrepEntity prep)
        {
            if (prep.GetPrimaryWeapon() == "")
                return;
            if (primaryLoadedWeapon != null)
                Destroy(primaryLoadedWeapon);

            var weaponInstance = loadWeapon(prep.GetPrimaryWeapon());
            weaponInstance.gameObject.SetParentAndLocals(getShip().PrimaryAttachPoint);
            primaryLoadedWeapon = weaponInstance.gameObject;
        }

        public override void SetSecondary(PrepEntity prep)
        {
            if (prep.GetSecondaryWeapon() == "")
                return;
            if (secondaryLoadedWeapon != null)
                Destroy(secondaryLoadedWeapon);

            var weaponInstance = loadWeapon(prep.GetSecondaryWeapon());
            weaponInstance.gameObject.SetParentAndLocals(getShip().SecondaryAttachPoint);
            secondaryLoadedWeapon = weaponInstance.gameObject;
        }

        private WeaponPrefab loadWeapon(string assetName)
        {
            PrefabAsset.TryGetPrefab(assetName, out WeaponPrefab prefab);
            var instance = Instantiate(prefab);

            return instance;
        }
        #endregion

        #region Animations
        public override void PlayEnterAnimation()
        {
            animator.SetBool(Enter_Key, true);
        }

        public override void PlayTransitionAnimation()
        {
            animator.SetBool(Transition_Key, true);
        }
        #endregion
    }
}
