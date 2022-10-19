using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class stickManManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem blood;
    private Animator StickManAnimator;
    private void Start()
    {
        StickManAnimator = GetComponent<Animator>();
        StickManAnimator.SetBool("run",true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("red") && other.transform.parent.childCount > 0)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

            Instantiate(blood, transform.position, Quaternion.identity);
        }
        
        switch (other.tag)
        {
           case "red":
               if (other.transform.parent.childCount > 0)
               {
                   Destroy(other.gameObject);
                   Destroy(gameObject);
               }
               
               break;
           
           case "jump":

               transform.DOJump(transform.position, 1f, 1, 1f).SetEase(Ease.Flash).OnComplete(PlayerManager.PlayerManagerInstance.FormatStickMan);
               
               break;
        }
        
        if (other.CompareTag("stair"))
        {
            transform.parent.parent = null; // for instance tower_0
            transform.parent = null; // stickman
            GetComponent<Rigidbody>().isKinematic = GetComponent<Collider>().isTrigger = false;
            StickManAnimator.SetBool("run",false);

            if (!PlayerManager.PlayerManagerInstance.moveTheCamera)
                PlayerManager.PlayerManagerInstance.moveTheCamera = true;

            if (PlayerManager.PlayerManagerInstance.player.transform.childCount == 2)
            {
                other.GetComponent<Renderer>().material.DOColor(new Color(0.4f, 0.98f, 0.65f), 0.5f).SetLoops(1000, LoopType.Yoyo)
                    .SetEase(Ease.Flash);
            }
        
        }
    }
}

