using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int playerAmount;
    [Range(5f, 10f)][SerializeField] private int maxPlayerPerRow;
    [Range(0f, 2f)][SerializeField] private float xGap;
    [Range(0f, 2f)][SerializeField] private float yGap;
    [Range(0f, 10f)][SerializeField] private float yOffset;
    
    [SerializeField] private List<int> towerCountList = new List<int>();
    [SerializeField] private List<GameObject> towerList = new List<GameObject>();
    public static Tower TowerInstance;

    private void Start()
    {
        TowerInstance = this;
    }

    public void CreateTower(int stickManNo)
    {
        playerAmount = stickManNo;
        FillTowerList();
        StartCoroutine(BuildTowerCoroutine());
      
    }
    void FillTowerList()
    {
        for (int i = 1; i <= maxPlayerPerRow; i++)
        {
            if (playerAmount < i)
            {
                break;
            }
            playerAmount -= i;
            towerCountList.Add(i);
        }
        
        for (int i = maxPlayerPerRow; i > 0; i--) 
        {
            if (playerAmount >= i)
            {
                playerAmount -= i;
                towerCountList.Add(i);
                i++;
            }
        }
    
    }
    
    IEnumerator BuildTowerCoroutine()
    {
            var towerId = 0;
            transform.DOMoveX(0f, 0.5f).SetEase(Ease.Flash);
    
            yield return new WaitForSecondsRealtime(0.55f);
            
            foreach (int towerHumanCount in towerCountList)
            {
                foreach (GameObject child in towerList)
                {
                    child.transform.DOLocalMove( child.transform.localPosition + new Vector3(0, yGap, 0), 0.2f).SetEase(Ease.OutQuad);
                }
                
                var tower = new GameObject("Tower" + towerId);
               
                tower.transform.parent = transform;
                tower.transform.localPosition = new Vector3(0, 0, 0);
                
                towerList.Add(tower);
                
                var towerNewPos = Vector3.zero;
                float tempTowerHumanCount = 0;
                
                for (int i = 1; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    child.transform.parent = tower.transform;
                    child.transform.localPosition = new Vector3(tempTowerHumanCount * xGap, 0, 0);
                    towerNewPos += child.transform.position;
                    tempTowerHumanCount++;
                    i--;
                    
                    if (tempTowerHumanCount >= towerHumanCount)
                    {
                        break;
                    }
                }
                
                tower.transform.position = new Vector3(-towerNewPos.x / towerHumanCount, tower.transform.position.y - yOffset, tower.transform.position.z);
             
                towerId++;
                yield return new WaitForSecondsRealtime(0.2f);
            }
    }
}
