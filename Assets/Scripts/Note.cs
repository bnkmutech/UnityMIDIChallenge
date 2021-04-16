using UnityEngine;

public class Note : MonoBehaviour
{
    public KeyCode KeyCode { get; set; }
    public string KeyCodeStr => KeyCode.ToString();
}
