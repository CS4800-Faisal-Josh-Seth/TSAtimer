using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
public string nextLevel;
    // Start is called before the first frame update
   
	/* public void Replay()
   {
   		FindObjectOfType<GameManager>().Reset();
   }
   */

   public void QuitGame()
   {
   		Application.Quit();
   }

   public void NextLevel()
   {
   	SceneManager.LoadScene(nextLevel);
   }
}
