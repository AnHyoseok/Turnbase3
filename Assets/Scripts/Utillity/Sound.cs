using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTurnBase
{
    //사운드 목록관리
    [System.Serializable]
    public class Sound
    {
        public string name;     // 재생할 사운드 이름 
        public AudioClip clip; //재생할 오디오 클립
        [Range(0f, 1f)]
        public float volume;      //재생 소리 크기
        [Range(0.1f, 3f)]
        public float pitch;       //재생 속도

        public bool loop;         //반복 여부

        [HideInInspector]
        public AudioSource source; //음원을 재생할 오디오 소스
    }
}