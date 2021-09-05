using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class AssetBundleManager : MonoBehaviour
{

    public static AssetBundleManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string BundlesRootPath
    {
        get
        {
#if UNITY_EDITOR
            return Application.streamingAssetsPath;

#elif UNITY_ANDROID
            return Application.persistentDataPath;
#endif
        }
    }

    Dictionary<string, AssetBundle> loadedBundles = new Dictionary<string, AssetBundle>();

    public AssetBundle LoadBundle(string bundleName)
    {
        if(loadedBundles.ContainsKey(bundleName))
        {
            return loadedBundles[bundleName];
        }

        AssetBundle ret = AssetBundle.LoadFromFile(Path.Combine(BundlesRootPath, bundleName));

        if(ret == null)
        {
            Debug.Log(bundleName + " does not exist");
        }

        else
        {
            loadedBundles.Add(bundleName, ret);
        }

        return ret;
    }

    public T GetAsset<T>(string bundleName, string asset) where T : Object
    {
        //initialize ret to null
        T ret = null;

        //get the bundle
        AssetBundle bundle = LoadBundle(bundleName);

        //check if you got a bundle
        if (bundle != null)
        {
            //get asset of type T in the bundle
            //if no asset found return null
            ret = bundle.LoadAsset<T>(asset);
        }

        return ret;
    }

}

