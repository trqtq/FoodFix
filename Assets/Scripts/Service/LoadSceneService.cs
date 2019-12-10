using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneService 
{
    /// <summary>
    /// 排队异步加载场景
    /// </summary>

        AssetBundle ab;

        public LoadSceneService()
        {
            SceneManager.sceneLoaded += (s, l) =>
            {
                IsLoading = false;
#if !UNITY_EDITOR
       if (ab != null)
                {
                    ab.Unload(false);
                }
#endif
                CheckloadQueue();
            };
        }

        /// <summary>
        /// 加载队列
        /// </summary>
        Queue<Action> loadQueue = new Queue<Action>();

        /// <summary>
        /// 当前是否已经在加载场景
        /// </summary>
        public bool IsLoading = false;

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        public void AsyncLoadScene(string sceneName)
        {
            loadQueue.Enqueue(() =>
            {
               Loom.Current. StartCoroutine(IELoadScene(sceneName));
            });

            CheckloadQueue();
        }

        void CheckloadQueue()
        {
            if (!IsLoading)
            {
                if (loadQueue.Count > 0)
                {
                    var action = loadQueue.Dequeue();

                    action();
                    IsLoading = true;
                }
            }
        }
        //具体的场景ab下载实在这里完成的
        IEnumerator IELoadScene(string sceneName)
        {
#if !UNITY_EDITOR
           var sceneABconfig = AssetsModel.GetAssetConfigWithName(Consts.SceneAB);
            var path = GetLoadPath(sceneABconfig.Url);
            WWW www = WWW.LoadFromCacheOrDownload(path, sceneABconfig.Version);
      
            while (!www.isDone) yield return null;
            if (www.error != null)
            {
                yield break;
            }
            ab = www.assetBundle;
#endif

            var async = SceneManager.LoadSceneAsync(sceneName);
            //显示加载UI   TODO
            yield break;
        }

        private string GetLoadPath(string url)
        {
            string platform = string.Empty;
#if UNITY_ANDROID
            platform = "Android";
#elif UNITY_IOS
            platform = "iOS";
#endif

        // return PathExtention.ToPath1(General.DownloadPath, platform, url);
        return null; //返回加载路径   TODO
    }
    }
