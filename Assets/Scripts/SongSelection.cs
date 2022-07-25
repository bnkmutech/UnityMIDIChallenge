using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelection : MonoBehaviour
{
    [SerializeField] public AudioSource selectedSong;
    [SerializeField] public AudioSource Song1_Speed1;
    [SerializeField] public AudioSource Song1_Speed2;
    [SerializeField] public AudioSource Song1_Speed3;
    [SerializeField] public AudioSource Song1_Speed4;
    [SerializeField] public AudioSource Song1_Speed5;

    void Start()
    {
        if(SongManager.speed == 1) selectedSong = Song1_Speed1;
        else if(SongManager.speed == 2) selectedSong = Song1_Speed2;
        else if(SongManager.speed == 3) selectedSong = Song1_Speed3;
        else if(SongManager.speed == 4) selectedSong = Song1_Speed4;
        else if(SongManager.speed == 5) selectedSong = Song1_Speed5;
        else selectedSong = Song1_Speed1;
    }
    
}
