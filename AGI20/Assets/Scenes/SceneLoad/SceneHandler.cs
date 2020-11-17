using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHandler
{
    // Dummy object to access MonoBehavior
    private class LoadingMonoBehaviour : MonoBehaviour { }

    // Add any scenes we want the handler to use. We should add a main menu, a stage select, the game area and a result area
    public enum Scene
    {
        SpiritScene,
        TravellerAR,
        LoadingScene,
        test
    }

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    // Run SceneHandler.Load(Loader.Scene.<name>) to start the right scene
    public static void Load(Scene scene)
    {
        // Run only if the loader got a chance to be seen, so we can the callback
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
        };

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    // Loads using coroutine, helps when we are using several prefabs
    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if(loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        } else
        {
            return 1f;
        }
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
