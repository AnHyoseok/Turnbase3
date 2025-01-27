using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace MyTurnBase
{
    //오디오를 관리하는 클래스
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        #region Variables
        public Sound[] sounds;

        private string bgmSound = "";       //현재 플레이 되는 배경음
        public string BgmSound
        {
            get
            {
                return bgmSound;
            }
        }
        #endregion

        protected override void Awake()
        {
            //싱글톤 구현부
            base.Awake();

            //오디오매니저 초기화
            foreach (var sound in sounds)
            {
                sound.source = this.gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;

            }
        }

        public void Play(string name)
        {
            Sound sound = null;

            //매개변수 이름과 같은 클립 찾기
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    break;
                }   
            }
            // 매개변수 이름과 같은 클립이 없으면
            if (sound == null)
            {
                Debug.Log($"Cannot Find {name}");
                return;
            }


            sound.source.Play();
        }

        public void Stop(string name)
        {
            Sound sound = null;

            //매개변수 이름과 같은 클립 찾기
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    //
                    if(s.name == bgmSound)
                    {
                        bgmSound = "";
                    }
                    break;
                }
            }
            // 매개변수 이름과 같은 클립이 없으면
            if (sound == null)
            {
                Debug.Log($"Cannot Find {name}");
                return;
            }
            sound.source.Stop();
        }

        //메뉴 배경음
        public void PlayBgm(string name)
        {
            //배경음 이름 체크
            if (bgmSound == name)
            {
                return;
            }

            //배경음 정지
            StopBgm();

            Sound sound = null;

            //매개변수 이름과 같은 클립 찾기
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    bgmSound = s.name;
                    sound = s;
                    break;
                }
            }
            // 매개변수 이름과 같은 클립이 없으면
            if (sound == null)
            {
                Debug.Log($"Cannot Find {name}");
                return;
            }

            sound.source.Play();
        }

        //메뉴 배경음
        public void StopBgm()
        {
            Stop(bgmSound);
        }
    }
}