﻿using System;
using System.Collections;
using Controllers.Bullet;
using UnityEngine;
using Controllers.Player;
using Data.UnityObjects;
using Data.ValueObjects;
using Managers;
using Signals;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Instance

        private static PlayerPhysicsController _instance;

        public static PlayerPhysicsController Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("PlayerPhysicsController is null");
                }

                return _instance;
            }
        }
        #endregion
        
        [SerializeField] private PlayerManager manager;
        [SerializeField] private InvulnerabilityData _ınvulnerabilityData;
        [SerializeField] private new Collider collider;
        public bool ableToMove = true;
        
        internal void GetInvulnerabilityData(InvulnerabilityData ınvulnerabilityData)
        {
            _ınvulnerabilityData = ınvulnerabilityData;
        }


        private void Awake()
        {
            _instance = this;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(collider.enabled && other.CompareTag("Enemy"))
            {
                PlayerMovementController.Instance.StopPlayer();
                PlayerMovementController.Instance.IsReadyToMove(false);
                PlayerMovementController.Instance.IsReadyToPlay(false);
                BulletController.Instance.IsReadyToMove(false);
                BulletController.Instance.IsReadyToPlay(false);
                ableToMove = false;
                Debug.Log("düsmana carptin");
                //skor ekranına gönder
            }

            if (collider.enabled && other.CompareTag("Obstacle"))
            {
                PlayerMovementController.Instance.StopPlayer();
                PlayerMovementController.Instance.IsReadyToMove(false);
                PlayerMovementController.Instance.IsReadyToPlay(false);
                BulletController.Instance.IsReadyToMove(false);
                BulletController.Instance.IsReadyToPlay(false);
                ableToMove = false;
                Debug.Log("engele carptin");
                //skor ekranına gönder
            }
            
            if (collider.enabled && other.CompareTag("Treasure"))
            {
                Debug.Log("Treasure!");
            }
        }


        public IEnumerator Invulnerability()
        {
            collider.enabled = false;
            yield return new WaitForSeconds(_ınvulnerabilityData.InvulnerabilityDuration);
            collider.enabled = true;
            yield return new WaitForSeconds(3f);
        }
        
        internal void OnReset()
        {
            ableToMove = true;
        }
    }
}