using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResourceIndex : ScriptableObject
{
    private const string STORAGE_PATH = "Assets/Resources/ResourceIndex";

#if UNITY_EDITOR
    // add types to be indexed here
    private static Dictionary<string, System.Type> typesToIndex = new Dictionary<string, System.Type>
    {
        /**
        FORMAT:
        { "<resource folder path>", typeof(<unityEngine.object>)}

        { "Quests", typeof(QuestSO) },
        { "Characters", typeof(CharacterSO) },

        etc... any types you want to index
        */
        {"Tweens",typeof(PlayerTweenSO)},
        {"TweenStates",typeof(CharacterTweenStatesSO)},
        {"Lights",typeof(LightFlickerDataSO)}

        //{"Projectiles",typeof(ProjectileDataSO)}
    };

    [MenuItem("LongLib/Create Resource Index")]
    private static void CreateIndex()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            Debug.Log("No Resource Folder! Creating...");
            AssetDatabase.CreateFolder("Assets", "Resources");
        }
        
        var index = Resources.Load<ResourceIndex>("ResourceIndex");
        if (index == null) 
        {
            Debug.Log("Could not find ResourceIndex! Creating...");
            index = CreateInstance<ResourceIndex>();
            UnityEditor.AssetDatabase.CreateAsset(index, STORAGE_PATH + ".asset");
        }

        index.resources = new List<ResourceType>();

        UpdateIndex();
    }

    private static void UpdateIndex()
    {
        var index = Resources.Load<ResourceIndex>("ResourceIndex");

        if (index == null) return;
        index.resources.Clear();
        
        //POPULATE RESOURCE INDEX
        foreach (var currentType in typesToIndex)
        {
            ResourceType newResourceType;
            newResourceType.name = currentType.Key;
            newResourceType.type = currentType.Value;
            newResourceType.typeAsString = currentType.Value.FullName;
            newResourceType.assets = new List<ResourceAsset>();


            var all = Resources.LoadAll(currentType.Key, currentType.Value);
            for (int i = 0; i < all.Length; i++)
            {
                // naming convention is id_whateverGoesHere, must be unique
                Object o = all[i];
                int id = -1;
                string[] split = o.name.Split('_');
                if (int.TryParse(split[0], out id) == false)
                {
                    Debug.LogErrorFormat("Invalid naming convention for asset {0}", o.name);
                    continue;
                }

                newResourceType.assets.Add(new ResourceAsset()
                {
                    name = split[1],
                    id = id,
                    assetPath = GetRelativeResourcePath(UnityEditor.AssetDatabase.GetAssetPath(o)),
                });
            }

            index.resources.Add(newResourceType);
        }
    }

    public static string GetRelativeResourcePath(string path)
    {
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        if (path.Contains("/Resources/"))
        {
            string[] rSplit = path.Split(new string[] { "/Resources/" }, System.StringSplitOptions.RemoveEmptyEntries);
            string[] split = rSplit[1].Split('.');
            for (int j = 0; j < split.Length - 1; j++)
            {
                stringBuilder.Append(split[j]);
                if (j < split.Length - 2)
                    stringBuilder.Append('/');
            }
            return stringBuilder.ToString();
        }
        return path;
    }
    
    private void OnValidate()
    {
        //if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == false)
        UpdateIndex();
    }
#endif

    private static Dictionary<string, ResourceAsset> assetTypeDictionary;
    public List<ResourceType> resources = new List<ResourceType>();

  [RuntimeInitializeOnLoadMethod]
  private static void OnInit()
  {
      ResourceIndex index = Resources.Load<ResourceIndex>("ResourceIndex");

      if(!index){
        Debug.LogWarning("Failed to load Resource Index! Is it created?");
        return;
      }

      string assetID = "";

      assetTypeDictionary = new Dictionary<string, ResourceAsset>();

      for(int i = 0;i < index.resources.Count;++i){
        Debug.Log("RESOURCE:" + index.resources[i].name);
        Debug.Log("RESOURCE:" + index.resources[i].typeAsString);
        for (int j = 0;j < index.resources[i].assets.Count;++j){

            Debug.Log("ID");
            Debug.Log(index.resources[i].assets[j].id);

            Debug.Log("full name");
            Debug.Log(index.resources[i].typeAsString); 

            assetID = index.resources[i].typeAsString+"_"+index.resources[i].assets[j].id;
            Debug.Log("ASSET: " + assetID);

            //Check for duplicates
            /*
            if (assetTypeDictionary.ContainsKey(assetID))
            {
                //Debug.LogErrorFormat("Duplicate asset id: " + asset.id + " of type " + resource.type.ToString());
                continue;
            }
            */
            assetTypeDictionary.Add(assetID, index.resources[i].assets[j]);
        }
      }
      /*
      foreach(ResourceType resource in index.resources)
      {
          Debug.Log("RESOURCE:" + resource.name);
          foreach(ResourceAsset asset in resource.assets)
          {
              assetID = resource.type.ToString()+"_"+asset.id;
              Debug.Log("ASSET: " + assetID);

              //Check for duplicates
              if (assetTypeDictionary.ContainsKey(assetID))
              {
                  Debug.LogErrorFormat("Duplicate asset id: " + asset.id + " of type " + resource.type.ToString());
                  continue;
              }
              assetTypeDictionary.Add(assetID, asset);
          }
      }
      */
  }


    /// <summary>
    /// Returns objects of types T given ID
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public static T GetAsset<T>(int id,string type) where T : UnityEngine.Object
    {
        ResourceAsset asset;

        if (assetTypeDictionary.TryGetValue(type+"_"+id, out asset))
            return Resources.Load<T>(asset.assetPath);
        return null;
    }

    /// <summary>
    /// Returns all objects of types T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetAllAssets<T>() where T : Object
    {
        List<T> assetList = new List<T>();
        
        List<string> keylist = new List<string>(assetTypeDictionary.Keys);

        //HACKY HACKY
        for(int i = 0;i<keylist.Count;i++)
        {
            ResourceAsset asset;
            string[] split = keylist[i].Split('_');

            if(split[0] == typeof(T).FullName)
            {
                if (assetTypeDictionary.TryGetValue(keylist[i], out asset))
                    assetList.Add(Resources.Load<T>(asset.assetPath));
            }
        }
        return assetList;
    }

    [System.Serializable]
    public struct ResourceAsset
    {
        [HideInInspector]
        public string name;
        public int id;
        public string assetPath;
    }

    [System.Serializable]
    public struct ResourceType
    {
        [HideInInspector]
        public string name;
        public System.Type type;
        public string typeAsString;
        public List<ResourceAsset> assets;
    }
}
