using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // ��ת��ָ������
    public void ChangeScene()
    {
        Debug.Log("11");
        SceneManager.LoadScene("Map");
    }

    // �ر���Ϸ
    public void QuitGame()
    {
        Application.Quit();

    }
}
