using System;
using PlayerScripts;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DefaultNamespace
{
    public class GunScr : IWeapon
    {
        
        [NonSerialized]public PlayerMovement PlayerSk;
        [NonSerialized]public FollowPlayer FollowScr;
        [NonSerialized]public WeaponScr WeaponScr;
        [NonSerialized]public bool IsGun;
        
        public GameObject bulletPrefab;
        public Transform firePoint;
        public Transform gun;
        public float fireRate = 0.3f;
        public int magazineSize = 10;
        public int currentAmmo = 10;
        public bool reloading = false;
        
        public float reloadTime = 2f;
        
        private float _reloadedTime = 0f;
        private float _nextFireTime = 0f;
        
        public float RotationOffset;

        public float recoil;
        
        private float _returnRotation;
        public float returnSpeed;
        [SerializeField]private float rotationRecoil;

        private int _modifier;
        
        public override void Attack()
        {
            if (Time.time >= _nextFireTime && currentAmmo > 0)
            {
                Fire();
                _nextFireTime = Time.time + fireRate;
            }
        }
        void Fire()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            var bulletScr = bullet.GetComponent<Bullet>();
            bulletScr.batColl = PlayerSk.gameObject.GetComponent<Collider2D>();
            // Optional: add spread or aim direction here
            currentAmmo--;
            
            WeaponScr.MoveBack(firePoint.gameObject, recoil);

            if (currentAmmo <= 0)
            {
                reloading = true;
                _reloadedTime = Time.time + reloadTime;
            }
            
            float randRotation = Random.Range(rotationRecoil/2, rotationRecoil);
            
            FollowScr.currentRotationOffset = randRotation*_modifier;

        }
        
        void Start()
        {
            currentAmmo = magazineSize;
            _returnRotation = RotationOffset;
        }

        void Update()
        {
            if(IsGun)
            {
                if (PlayerSk.movementInput.x >= 0.1f)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    _modifier = 1;
                }

                if (PlayerSk.movementInput.x <= -0.1f)
                {
                    transform.localScale = new Vector3(1, -1, 1);
                    _modifier = -1;
                }
                
                if (math.abs(FollowScr.currentRotationOffset) <= 1)
                {
                    FollowScr.currentRotationOffset = _returnRotation;
                }
            }
            
            if (reloading)
            {
                if (Time.time >= _reloadedTime)
                {
                    Reload();
                }
            }
        }

        void Reload()
        {
            reloading = false;
            currentAmmo = magazineSize;
            // Add reload sound or animation here
        }
    }
}