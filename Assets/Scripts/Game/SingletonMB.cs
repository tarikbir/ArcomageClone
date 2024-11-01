using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace ArcomageClone
{
    /// <summary>
    ///     Singleton Monobehavior Implementation. Refs below:
    ///     1. https://gist.github.com/rickyah/271e3aa31ff8079365bc
    ///     2. https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
    ///     3. https://gist.github.com/mstevenson/4325117
    ///     4. https://stackoverflow.com/questions/46438184/how-to-create-a-generic-singleton-class-in-unity
    ///     5. http://wiki.unity3d.com/index.php/Singleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DisallowMultipleComponent]
    public class SingletonMB<T> : MonoBehaviour where T : class
    {
        void HandleDuplication()
        {
            //if not null we grab all possible objects of this type
            var allSingletonsOfThis = FindObjectsByType(typeof(T), FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            if (_isSilent)
            {
                foreach (var duplicated in allSingletonsOfThis)
                    //if the singleton is silent, just destroy the sparing objects
                    if (!ReferenceEquals(duplicated, Instance))
                        Destroy(duplicated);
            }
            else
            {
                //if not silent, we raise an error with the names of the all the objets
                var singletonsNames = string.Empty;
                foreach (var duplicated in allSingletonsOfThis)
                    singletonsNames += duplicated.name + ", ";

                //throws an error with all objects that have this monobehavior as message
                var message = "[" + GetType() + "] Something went really wrong, " +
                                "there is more than one Singleton: \"" + typeof(T) +
                                "\". GameObject names: " +
                                singletonsNames;

                throw new SingletonMBException(message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        #region Exceptions

        public class SingletonMBException : Exception
        {
            public SingletonMBException(string message) : base(message)
            {
            }
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Fields

        //multi thread locker
        static readonly object _locker = new object();

        [FoldoutGroup("Singleton Attributes", false)]
        [InfoBox("Mark it whether this singleton will be destroyed when the scene changes")]
        [SerializeField]
        bool _isDontDestroyOnLoad;

        [FoldoutGroup("Singleton Attributes", false)]
        [InfoBox("Mark it whether the script raises an exception when another singleton like this is present in the scene")]
        [SerializeField]
        bool _isSilent = true;

        //singleton generic instance
        public static T Instance { get; private set; }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Initialization

        protected virtual void Awake()
        {
            //multi thread lock
            lock (_locker)
            {
                // if null we set the instance to be this and mark the
                // gameobject whether or not is destroyed on load
                if (Instance is null)
                    Initialize();
                else if (Instance as SingletonMB<T> != this) HandleDuplication();
            }
        }

        protected virtual void OnDestroy()
        {
            if (Instance as SingletonMB<T> == this) Instance = null;
        }

        void Initialize()
        {
            Instance = this as T;
            if (_isDontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}