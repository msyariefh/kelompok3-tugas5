using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankU.Audio;

public class GameplayContoller : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.PlayBGM("GameplayBGM");
    }
}
