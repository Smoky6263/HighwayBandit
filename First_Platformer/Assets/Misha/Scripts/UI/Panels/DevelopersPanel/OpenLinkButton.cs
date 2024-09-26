using UnityEngine;

public class OpenLinkButton : ButtonScript
{
    [SerializeField] private string _URL;

    public void OpenURL()
    {
        Application.OpenURL(_URL);
    }
}
