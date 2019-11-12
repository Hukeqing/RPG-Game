using System;
using System.Security.Cryptography;
using Scripts.Unit;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        public Transform firePoint;
        public float maxFireDistant = 100;
        public float fireCoolDown = 0.15f;
        public float lineCoolDown = 0.05f;

        public int damage = 10;

        private AudioSource fireClip;
        private PlayerControl pc;
        
        public GameObject fireLight;
        public ParticleSystem fireEffect;
        public GameObject hitEffect;
        
        private LineRenderer fireLine;
        private Ray fireRay;
        private RaycastHit fireTarget;
        private float nextFire;
        private float lineDisableTime;

        private void Start()
        {
            fireLine = firePoint.GetComponent<LineRenderer>();
            fireClip = GetComponent<AudioSource>();
            pc = GetComponent<PlayerControl>();
            fireLine.enabled = false;
            fireLight.SetActive(false);
            nextFire = 0;
            lineDisableTime = 0;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && Time.time > nextFire && !pc.IsDie)
            {
                nextFire = Time.time + fireCoolDown;

                lineDisableTime = lineCoolDown;
                
                var firePosition = firePoint.position;
                fireLine.enabled = false;
                fireLight.SetActive(false);
                fireLine.SetPosition(0, firePosition);
                fireRay.origin = firePosition;
                fireRay.direction = firePoint.forward;
                if (Physics.Raycast(fireRay, out fireTarget, maxFireDistant))
                {
                    fireLine.SetPosition(1, fireTarget.point);
                    var newEffect = Instantiate(hitEffect, fireTarget.point, transform.rotation);
                    newEffect.transform.LookAt(transform);
                    newEffect.GetComponent<ParticleSystem>().Play();
                    Destroy(newEffect, 0.5f);
                    
                    if (fireTarget.collider.gameObject.layer == 17)
                    {
                        var enemyHealth = fireTarget.collider.GetComponent<Health>();
                        enemyHealth.ApplyDamage(damage);
                    }
                }
                else
                {
                    fireLine.SetPosition(1, firePosition + maxFireDistant * firePoint.forward);
                }

                fireLine.enabled = true;
                fireEffect.transform.position = firePosition;
                fireEffect.Stop();
                fireEffect.Play();
                fireClip.Stop();
                fireClip.Play();
                fireLight.SetActive(true);
            }

            if (lineDisableTime > 0)
            {
                lineDisableTime -= Time.deltaTime;
                if (lineDisableTime <= 0)
                {
                    fireLine.enabled = false;
                    fireLight.SetActive(false);
                }
            }
        }
    }
}