using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

namespace Assets.Core
{
    public class UIManager : MonoBehaviour
    {
        #region Instance
        public static UIManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }

            SceneManager.sceneLoaded += (scene, mode) =>
            {
                Debug.Log($"NewScene: {scene.name} -- Mode: {mode}");
                _allUIInScene.Clear();
            };
        }
        #endregion
        [ShowInInspector]
        readonly IList<BaseUI> _allUIInScene = new List<BaseUI>();
        public void AddUIInScene(BaseUI baseUI) => _allUIInScene.Add(baseUI);

        public T GetInstanceUI<T>() where T : class
        {
            T instance = null;
            if (_allUIInScene.Count(item => (item as object) as T != null) > 0)
            {
                var baseUIInstance = _allUIInScene.FirstOrDefault(item => (item as object) as T != null);
                instance = (T)(baseUIInstance as object);
            }
            else
                instance = FindObjectsOfType<BaseUI>().OfType<T>().FirstOrDefault();

            return instance;
        }
    }
}
