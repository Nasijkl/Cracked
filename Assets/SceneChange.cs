using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // 跳转到指定场景
    public void ChangeScene()
    {
        Debug.Log("11");
        SceneManager.LoadScene("Map");
    }

    // 关闭游戏
    public void QuitGame()
    {
        Application.Quit();

    }
}
