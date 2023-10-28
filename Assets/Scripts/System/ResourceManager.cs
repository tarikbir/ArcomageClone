using ArcomageClone.Cards;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ArcomageClone
{
    public class ResourceManager : SingletonMB<ResourceManager>
    {
        [SerializeField] private AssetReference _gameScene;

        private Dictionary<string, Sprite> _spritesDict;
        private Dictionary<string, CardSO> _cardsDict;

        private void Start()
        {
            LoadSprites();
            LoadCardData();
        }

        private void LoadSprites()
        {
            var handler = LoadAssets<Sprite>("graphics");
            handler.Completed += (h) =>
            {
                _spritesDict = OnCompleteLoad<Sprite>(handler, (d, i) =>
                {
                    if (!d.ContainsKey(i.name))
                    {
                        d.Add(i.name, i);
                    }
                });
                LoadGameScene(); //TODO: Remove from here and add to general load or game manager
            };
        }

        private void LoadCardData()
        {
            var handler = LoadAssets<CardSO>("cards");
            handler.Completed += (h) =>
            {
                _cardsDict = OnCompleteLoad<CardSO>(handler, (d, i) =>
                {
                    if (!d.ContainsKey(i.name))
                    {
                        d.Add(i.name, i);
                    }
                });
            };
        }

        private AsyncOperationHandle<IList<T>> LoadAssets<T>(string tag, Action<T> callback = null)
        {
            return Addressables.LoadAssetsAsync<T>(tag, callback, true);
        }

        private Dictionary<string, T> OnCompleteLoad<T>(AsyncOperationHandle<IList<T>> handle, Action<Dictionary<string, T>, T> handleDictionary)
        {
            Dictionary<string, T> dict = new();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var item in handle.Result)
                {
                    handleDictionary(dict, item);
                }
            }
            else
            {
                Debug.LogErrorFormat($"Could not load {typeof(T).FullName}");
            }
            return dict;
        }

        public IEnumerable<CardSO> GetAllCards()
        {
            return _cardsDict.Values;
        }

        public Sprite GetSprite(string id)
        {
            return TryGetFromCachedDict(_spritesDict, id);
        }

        public CardSO GetCardData(string id)
        {
            return TryGetFromCachedDict(_cardsDict, id);
        }

        public void LoadGameScene(Action callback = null)
        {
            var handler = _gameScene.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Additive);
            if (callback != null)
            {
                handler.Completed += (handle) =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        callback?.Invoke();
                    }
                };
            }
        }

        private T TryGetFromCachedDict<T>(Dictionary<string, T> dict, string id)
        {
            if (dict.TryGetValue(id, out T item))
            {
                return item;
            }

            return default;
        }
    }
}
