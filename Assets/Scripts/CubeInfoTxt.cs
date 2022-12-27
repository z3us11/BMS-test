using UnityEngine;
using TMPro;

public class CubeInfoTxt : MonoBehaviour
{
    public static TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = "No Cube Selected";
    }

    public static void UpdateText(string txt)
    {
        text.text = txt;
    }
}
