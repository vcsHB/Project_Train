using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoundManage
{

    public class SoundController : MonoSingleton<SoundController>
    {
        [SerializeField] private int _defaultPoolAmount;
        private Stack<SoundPlayer> _pool = new();
        [SerializeField] private SoundPlayer _soundPlayerPrefab;


        private void Start()
        {
            for (int i = 0; i < _defaultPoolAmount; i++)
            {
                GeneratePoolObject();
            }
        }

        private void OnDestroy()
        {
            
            _pool.Clear();
        }


        public SoundPlayer PlaySound(SoundSO soundSO, Vector2 position)
        {
            SoundPlayer player = _pool.Count > 0
                    ? _pool.Pop()
                    : GenerateSoundPlayer();
            player.gameObject.SetActive(true);
            player.transform.position = position;
            player.PlaySound(soundSO);
            return player;
        }

        private SoundPlayer GenerateSoundPlayer()
        {
            SoundPlayer soundPlayer = Instantiate(_soundPlayerPrefab, transform);
            soundPlayer.OnSoundPlayCompleteEvent += HandleSoundPlayOver;
            soundPlayer.gameObject.SetActive(false);
            return soundPlayer;
        }

        private void HandleSoundPlayOver(SoundPlayer player)
        {
            player.ResetItem();
            player.gameObject.SetActive(false);
            _pool.Push(player);
        }

        private void GeneratePoolObject()
        {
            SoundPlayer newPlayer = GenerateSoundPlayer();
            _pool.Push(newPlayer);
        }


    }
}