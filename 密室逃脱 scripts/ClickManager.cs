using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {

    private static ClickManager Instance;
    public static ClickManager _Instance
    {
        get
        {
            return Instance;
        }
    }

    public Canvas PackageCanves;

    public Transform cameraPos;

    public Camera [] cameras;

    [HideInInspector] public GameObject OnclickObj;      //取得被点击的格子

    public bool isclick=false;

    public bool[] isActive;

    public string GridName;

    public GameObject canves;
    public GameObject FireWall;
    public GameObject Powder;
    public GameObject PowderWick;

    void Awake () {
        Instance = this;
        cameras[0].gameObject.SetActive(true);
        for(int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        //CameraActive(cameras[0], isActive[0]);
        //CameraNoActive(cameras[1], isActive[1]);
    }
	
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RayCast(Input.mousePosition);
        }
        if (Input.GetKeyDown(KeyCode.R))          //按R键回到主房间
        {
            FindEnableCamera(Camera.allCameras).gameObject.SetActive(false);
            cameras[0].gameObject.SetActive(true);
        }
        //设置渲染画布的相机为可用相机
        PackageCanves.GetComponent<Canvas>().worldCamera = FindEnableCamera(Camera.allCameras);

    }

    private void RayCast(Vector3 screenPos)         //射线检测鼠标点击事件 ！！不能为collider 2D!!否则检测不到
    {               
        Ray ray = FindEnableCamera(Camera.allCameras).ScreenPointToRay(screenPos);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            Debug.Log(hit.collider.gameObject. name);
            switch (hit.collider.gameObject.name)
            {
                case "背包":
                    canves.GetComponent<Canvas>().planeDistance = 100;
                    GameManager._Instance.one = 0;
                    break;
                case "火盆":
                    storeItemToPackage(3, hit.collider.gameObject);
                    break;
                case "碎布":
                    storeItemToPackage(2, hit.collider.gameObject);
                    break;
                case "木棍":
                    storeItemToPackage(1, hit.collider.gameObject);
                    break;
                case "墙上火把":
                    storeItemToPackage(6, hit.collider.gameObject);
                    break;
                case "油灯":                    
                    storeItemToPackage(9, hit.collider.gameObject);//点击一次后禁用自身collider
                    GameObject.Find("油灯").GetComponent<BoxCollider>().enabled = false;
                    break;
                case "剥落的一小块":
                    for (int i = 0; i < cameras.Length; i++)
                    {
                        if (i == 2)
                            cameras[i].gameObject.SetActive(true);
                        else
                            cameras[i].gameObject.SetActive(false);
                    }
                    break;
                case "放灯的位置":
                    if (GameManager._Instance.one == 1)       //在对应位置实例化物体并且销毁背包中物品
                    {
                        Instantiate(FireWall, hit.collider.transform.position, Quaternion.identity);
                        Debug.Log(GridName);
                        if (OnclickObj.transform.childCount != 0)
                            Destroy(OnclickObj.transform.GetChild(0).gameObject);
                        ItemModel.DeleteItem(GridName);
                        GameManager._Instance.one = 0;
                    }
                    break;
                case "柜子":
                    for (int i = 0; i < cameras.Length; i++)
                    {
                        if (i == 1)
                            cameras[i].gameObject.SetActive(true);
                        else
                            cameras[i].gameObject.SetActive(false);
                    }
                    break;
                case "地下室·装火药的罐子":
                    BackpackManager._instance.storeItem(8);
                    GameObject.Find("地下室·装火药的罐子").GetComponent<BoxCollider>().enabled = false;
                    break;
                case "地下室箱子":
                    if (GameManager._Instance.three == 3)
                    {
                        Debug.Log("拼图启动");
                    }
                    break;
                case "洒火药的位置":
                    if (GameManager._Instance.four == 4)
                    {
                        Instantiate(Powder, hit.collider.transform.position, Quaternion.identity);
                        if (OnclickObj.transform.childCount != 0)
                            Destroy(OnclickObj.transform.GetChild(0).gameObject);
                        ItemModel.DeleteItem(GridName);
                        GameManager._Instance.four = 3;    //表示洒过火药
                    }
                    if (GameManager._Instance.four == 3&& GameManager._Instance.five == 5)
                    {
                        Instantiate(PowderWick, hit.collider.transform.position, Quaternion.identity);
                        if (OnclickObj.transform.childCount != 0)
                            Destroy(OnclickObj.transform.GetChild(0).gameObject);
                        ItemModel.DeleteItem(GridName);
                        Destroy(GameObject.Find("火药(Clone)"));
                        GameManager._Instance.four = 3;
                        GameManager._Instance.five = 0;
                    }
                    break;
                case "墙体剥落后图片":
                    Destroy(GameObject.Find("墙体剥落后图片"));
                    break;

            }
        }
       
    }
    public void storeItemToPackage(int index,GameObject destroyObject)
    {
        BackpackManager._instance.storeItem(index);
        Destroy(destroyObject);
    }

    public Camera FindEnableCamera(Camera[] Cameras)     //返回场景中可用的相机
    {
        for(int i = 0; i < Cameras.Length; i++)
        {
            if (Cameras[i].enabled == true)
                return Cameras[i];
        }
        return null;
    }
    //弃用的查找相机
    //public void CameraActive(Camera camera,bool isactive)
    //{
    //    camera.gameObject.SetActive(true);
    //    isactive = true;
    //}
    //public void CameraNoActive(Camera camera, bool isactive)
    //{
    //    camera.gameObject.SetActive(false);
    //    isactive = false;
    //}
    //public Camera findActive(bool [] isactive,Camera [] cameras)
    //{
    //    for(int i = 0; i < isActive.Length; i++)
    //    {
    //        if (isActive[i] == true)
    //            return cameras[i];
    //    }
    //    return false;
    //}


}
