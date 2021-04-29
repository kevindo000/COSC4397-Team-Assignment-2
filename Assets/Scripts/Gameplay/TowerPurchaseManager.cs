using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class TowerPurchaseManager : MonoBehaviour
{
    private List<GameObject> buttonAssets;
    private Dictionary<string, Tower> towers;
    public List<GameObject> availTowers;

    public GameObject projectile;
    public GameObject canvas;
    public GameObject ImgTarget;
    public GlobalState glob;

    public string towerURL = string.Empty;
    public string attrURL = string.Empty;
    public string btnURL = string.Empty;
    private bool isstartdone = false;
    // Start is called before the first frame update

    private bool Purchase(string name)
    {
        if (towers.ContainsKey(name))
        {
            Tower t = towers[name];
            long price = t.attr.Cost;
            if (glob.money - price >= 0)
            {
                glob.money -= price;
                GameObject temp = Instantiate(t.obj);
                temp.SetActive(true);
                temp.transform.localScale = new Vector3(10, 10, 10);
                temp.AddComponent(typeof(CharacterController));
                temp.AddComponent(typeof(SphereCollider));
                temp.GetComponent<SphereCollider>().radius = 15;
                temp.GetComponent<SphereCollider>().isTrigger = false;
                temp.AddComponent(typeof(Move3D));
                temp.GetComponent<Move3D>().a = t.attr;
                temp.GetComponent<Move3D>().projectile = projectile;
                temp.transform.SetParent(ImgTarget.transform);
                if (name == "ice_cream_cone")
                {
                    temp.transform.localScale = new Vector3(100, 100, 100);
                }
                availTowers.Add(temp);
                temp.transform.localPosition = new Vector3(0, 5, 0);
                return true;
            }
            else
            {
                Debug.LogError("The cost is too big!");
                return false;
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
            foreach (UnityEngine.Object t in temp)
            {
                if (t.GetType() == typeof(GameObject))
                {
                    Debug.Log(t.name);
                    GameObject go = (GameObject)t;
                    Tower tempTower = new Tower();
                    tempTower.obj = go;
                    tempTower.attr = GetAttributes(go.name);
                    towers[go.name] = tempTower;
                }
            }
            bundle.Unload(false);
        }
        //TODO: LOAD BUTTONS
        buttonAssets = new List<GameObject>();
        using (UnityWebRequest button_req = UnityWebRequestAssetBundle.GetAssetBundle(btnURL, 0))
        {
            int xpos = 0;
            yield return button_req.SendWebRequest();
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(button_req);
            UnityEngine.Object[] temp = bundle.LoadAllAssets();
            foreach (UnityEngine.Object t in temp)
            {
                if (t.GetType() == typeof(GameObject))
                {
                    Debug.Log(t.name);
                    GameObject btn = (GameObject)t;
                    GameObject mybtn = Instantiate(btn);
                    mybtn.SetActive(true);
                    mybtn.transform.SetParent(canvas.transform);
                    mybtn.transform.position = new Vector3(xpos + 25, 25, 0);
                    mybtn.transform.localScale = new Vector3(0.5f, 0.5f, 0);
                    xpos += 50;
                    string newname = t.name.Substring(0, t.name.Length - 4);
                    mybtn.GetComponent<Button>().onClick.AddListener(() => ButtonPress(newname));
                    buttonAssets.Add(mybtn);
                }
                else
                {
                    Debug.LogError(t.name + " " + t.GetType());
                }
            }
            bundle.Unload(false);
        }
        isstartdone = true;
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

    public void ExitGame()
    {
        if (isstartdone) SceneManager.LoadScene("ShopScene");
    }
}
