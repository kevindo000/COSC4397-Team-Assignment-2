using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using System;

public class TowerPurchaseManager : MonoBehaviour
{
    private long money = 10000000;

    private List<GameObject> buttonAssets;
    private Dictionary<string, Tower> towers;
    public List<GameObject> availTowers;

    public GameObject projectile;
    public GameObject canvas;

    public string towerURL = string.Empty;
    public string attrURL = string.Empty;
    public string btnURL = string.Empty;
    int xpos = 0;
    // Start is called before the first frame update

    private bool Purchase(string name)
    {
        if (towers.ContainsKey(name))
        {
            Tower t = towers[name];
            long price = t.attr.Cost;
            if(price > money)
            {
                Debug.LogError("The cost is too big!");
                return false;
            }
            else
            {
                Debug.Log(string.Format("Purchasing a {0} ({1} -> {2})", name, money, money - price));
                money -= price;
                GameObject temp = Instantiate(t.obj);
                temp.SetActive(true);
                //if(name == "ice_cream_cone")
                //{
                //    temp.transform.localScale = new Vector3(100, 100, 100);
                //    temp.AddComponent(typeof(MeshRenderer));
                //}
                //else
                //{
                //    temp.transform.localScale = new Vector3(10, 10, 10);
                //}
                temp.transform.localScale = new Vector3(10, 10, 10);
                temp.AddComponent(typeof(CharacterController));
                temp.AddComponent(typeof(SphereCollider));
                temp.GetComponent<SphereCollider>().radius = 0;
                temp.GetComponent<SphereCollider>().isTrigger = false;
                temp.AddComponent(typeof(Move3D));
                temp.GetComponent<Move3D>().a = t.attr;
                temp.GetComponent<Move3D>().projectile = projectile;
                if(name == "ice_cream_cone")
                {
                    temp.transform.localScale = new Vector3(50, 50, 50);
                }
                availTowers.Add(temp);
                return true;
            }
        }
        else
        {
            Debug.LogError("Could not purchase: invalid name!");
            return false;
        }
    }

    private Attributes GetAttributes(string name)
    {
        UnityWebRequest req = UnityWebRequest.Get(attrURL + name + ".json");
        var status = req.SendWebRequest();
        while (!status.isDone) { }
        return JsonConvert.DeserializeObject<Attributes>(req.downloadHandler.text);
    }

    IEnumerator Start()
    {
        //game assets into dict
        //button accesses game asset dict
        availTowers = new List<GameObject>();
        towers = new Dictionary<string, Tower>();
        using (UnityWebRequest tower_req = UnityWebRequestAssetBundle.GetAssetBundle(towerURL, 0))
        {
            yield return tower_req.SendWebRequest();
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(tower_req);
            UnityEngine.Object[] temp = bundle.LoadAllAssets();
            foreach(UnityEngine.Object t in temp)
            {
                if(t.GetType() == typeof(GameObject))
                {
                    Debug.Log(t.name);
                    GameObject go = (GameObject)t;
                    Tower tempTower = new Tower();
                    tempTower.obj = go;
                    tempTower.attr = GetAttributes(go.name);
                    towers[go.name] = tempTower;
                }
            }
        }
        //TODO: LOAD BUTTONS
        buttonAssets = new List<GameObject>();
        using(UnityWebRequest button_req = UnityWebRequestAssetBundle.GetAssetBundle(btnURL, 0))
        {
            yield return button_req.SendWebRequest();
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(button_req);
            UnityEngine.Object[] temp = bundle.LoadAllAssets();
            foreach(UnityEngine.Object t in temp)
            {
                if(t.GetType() == typeof(GameObject))
                {
                    Debug.Log(t.name);
                    GameObject btn = (GameObject)t;
                    GameObject mybtn = Instantiate(btn);
                    mybtn.SetActive(true);
                    mybtn.transform.SetParent(canvas.transform);
                    mybtn.transform.position = new Vector3(xpos + 50, 50, 0);
                    xpos += 100;
                    string newname = t.name.Substring(0, t.name.Length - 4);
                    mybtn.GetComponent<Button>().onClick.AddListener(() => ButtonPress(newname));
                    buttonAssets.Add(mybtn);
                }
                else
                {
                    Debug.LogError(t.name + " " + t.GetType());
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress(string button)
    {
        if (Purchase(button))
        {
            Debug.Log(button + " Purchased!");
        }
        else
        {
            Debug.Log("Could not purchase " + button);
        }
    }
}
