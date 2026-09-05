using System;
using UnityEngine;

public class AvatarPreferences : MonoBehaviour
{
    [SerializeField] Sprite blackAvatar;
    [SerializeField] Sprite greenAvatar;
    [SerializeField] Sprite pinkAvatar;
    [SerializeField] Sprite yellowAvatar;
    [SerializeField] Sprite blackAvatarOnCollision;
    [SerializeField] Sprite greenAvatarOnCollision;
    [SerializeField] Sprite pinkAvatarOnCollision;
    [SerializeField] Sprite yellowAvatarOnCollision;
    public event EventHandler OnAvatarRefresh;

    string currentAvatar;
    private void Start()
    {
        if(currentAvatar == null)
        {
            SetAvatar();
        }
    }

    public void SetAvatar(string avatar = "black")
    {
        PlayerPrefs.SetString("avatar", avatar);
        currentAvatar = PlayerPrefs.GetString("avatar");
    }
    public void RefreshAvatar()
    {
        OnAvatarRefresh?.Invoke(this, EventArgs.Empty);
        currentAvatar = PlayerPrefs.GetString("avatar");

    }

    public Sprite GetAvatar()
    {
        if (currentAvatar == "green")
        {
            return greenAvatar;
        }
        else if (currentAvatar == "pink")
        {
            return pinkAvatar;
        }
        else if (currentAvatar == "yellow")
        { 
            return yellowAvatar;
        }
        else
        {
            return blackAvatar;
        }
    }
    public Sprite GetAvatarOnCollision()
    {
        if (currentAvatar == "green")
        {
            return greenAvatarOnCollision;
        }
        else if (currentAvatar == "pink")
        {
            return pinkAvatarOnCollision;
        }
        else if (currentAvatar == "yellow")
        {
            return yellowAvatarOnCollision;
        }
        else
        {
            return blackAvatarOnCollision;
        }
    }
}
