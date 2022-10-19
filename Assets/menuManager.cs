using UnityEngine;

public class menuManager : MonoBehaviour
{
  [SerializeField] private GameObject startMenuObj;
  
  public void StartTheGame()
  {
    startMenuObj.SetActive(false);
    PlayerManager.PlayerManagerInstance.gameState = true;
    
    PlayerManager.PlayerManagerInstance.player.GetChild(1).GetComponent<Animator>().SetBool("run",true);
  }
}
