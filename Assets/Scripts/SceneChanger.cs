using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int n)
    {
        SceneManager.LoadScene(n);
    }
}