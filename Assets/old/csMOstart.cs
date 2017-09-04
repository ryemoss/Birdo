using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class csMOstart: MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClick());
    }

    void OnClick()
    {
        PlayerData.roundcoins = 0;
        PlayerData.roundbirds = 0;
        PlayerData.roundexp = 0;
        PlayerData.roundaccuracy = 00.0f;
        PlayerData.roundshots = 0;

        SceneManager.LoadScene("Scene_Play");
    }

}