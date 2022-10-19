using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMiniGame : MonoBehaviour
{
    public void GotoScene()
    {
        SceneManager.LoadScene("MiniGame");
    }
}
