using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {
    public AudioSource AttainItem;    //取得物品音效
    public AudioSource ToTheScene;    //背包中物品放到场景中
    public AudioSource Open;           //打开合上箱子柜子
    public AudioSource Gao;            //用镐砸门
    public AudioSource DoorUp;         //石门升起
    public AudioSource DoorOut;        //石门被炸开
    public AudioSource Fire;

    public GameObject BrokenDoor;       //被炸开的石门
    public GameObject GoOut;           //出口
    public GameObject canves;
    public GameObject FireWall;        //点燃的墙上火把
    public GameObject FireWood;         //点燃的火把
    public GameObject Powder;           //火药
    public GameObject PowderWick;       //装芯的火药
    public GameObject nextBox;          //箱子下一层
    public GameObject Rock;             //石头
    public GameObject Room2;

    private static ClickManager Instance;
    public static ClickManager _Instance
    {
        get
        {
            return Instance;
        }
    }

    public GameObject Ho1;

    private bool isDestroy=false;

    public Canvas PackageCanves;    //背包画布

    public Canvas PuzzleCanves;     //拼图画布

    public Transform cameraPos;

    public Camera [] cameras;

    [HideInInspector] public GameObject OnclickObj;      //取得被点击的格子

    public bool isclick=false;

    private bool doorMove=false;

    public bool[] isActive;

    private bool isEnd=false;

    

    public string GridName;

    

    void Awake () {
        Instance = this; 
        cameras[0].gameObject.SetActive(true);         //初始化设置除主相机之外的相机不可用
        for(int i = 1; i < cameras.Length; i++)     
        {
            cameras[i].gameObject.SetActive(false);
        }
       
        nextBox.SetActive(false);
        //CameraActive(cameras[0], isActive[0]);
        //CameraNoActive(cameras[1], isActive[1]);
    }
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))   //按ESC退出
        {
            Application.Quit();
        }
        if (Input.GetMouseButtonDown(0))           //鼠标左键射线检测
        {
            RayCast(Input.mousePosition);
        }
        if (isEnd == false)               //游戏结束按R键无法返回
        {
            if (Input.GetKeyDown(KeyCode.R))          //按R键回到主房间
            {
                FindEnableCamera(Camera.allCameras).gameObject.SetActive(false);
                cameras[0].gameObject.SetActive(true);
            }
        }
        if (doorMove)
        {
            Transform door = GameObject.Find("石门").transform;
            float y = door.position.y;
            if (GameObject.Find("石门") != null)
            {
                GameObject.Find("石门").transform.position = new Vector3(door.position.x, y + Time.deltaTime, door.position.z);
                if (y > 6)
                {
                    GameManager._Instance.ten = 10;
                    doorMove = false;
                }
            }
            
                
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
                case "背包":        //背包画布不可见
                    canves.GetComponent<Canvas>().planeDistance = 100;
                    GameManager._Instance.one = 0;
                    GameManager._Instance.four = 0;
                    GameManager._Instance.five = 0;
                    GameManager._Instance.six = 0;
                    GameManager._Instance.nine = 0;
                    break;
                case "火盆":
                    if (GameManager._Instance.one == 1)
                    {
                        ToTheScene.Play();
                        Instantiate(FireWood, hit.collider.transform.position, Quaternion.identity);
                        if (OnclickObj.transform.childCount != 0)
                            Destroy(OnclickObj.transform.GetChild(0).gameObject);
                        ItemModel.DeleteItem(GridName);
                        GameManager._Instance.one = 0;
                    }
                    
                    break;
                case "碎布":
                    storeItemToPackage(2, hit.collider.gameObject);
                    
                    break;
                case "木棍":
                    storeItemToPackage(1, hit.collider.gameObject);
                    
                    break;
                case "地下室火把(点燃）(Clone)":                   
                    storeItemToPackage(5, hit.collider.gameObject);
                    break;
                case "墙上火把":                //点燃墙上的火把
                    if (GameManager._Instance.two == 2)
                    {
                        ToTheScene.Play();
                        Fire.Play();
                        Destroy(hit.collider.gameObject);
                        Instantiate(FireWall, hit.collider.gameObject.transform.position, Quaternion.identity);
                        if (OnclickObj.transform.childCount != 0)
                            Destroy(OnclickObj.transform.GetChild(0).gameObject);
                        ItemModel.DeleteItem(GridName);
                        GameManager._Instance.two = 0;
                        GameObject.Find("大房间1(Clone)").SetActive(false);

                        Instantiate(Room2, new Vector3(0.1f, 0, 0), Quaternion.identity);
                        
                        Fire.Play();
                    }
                  
                    break;
                case "油灯":                    
                    storeItemToPackage(9, hit.collider.gameObject);//点击一次后禁用自身collider
                    GameObject.Find("油灯").GetComponent<BoxCollider>().enabled = false;
                    break;
                case "剥落的一小块":
                    SetEnableCamera(cameras, 2);
                    break;
               
                case "柜子":
                    Open.Play();
                    SetEnableCamera(cameras, 1);
                    break;
                case "地下室·装火药的罐子":
                    AttainItem.Play();
                    BackpackManager._instance.storeItem(8);
                    GameObject.Find("地下室·装火药的罐子").GetComponent<BoxCollider>().enabled = false;
                    break;
                case "地下室箱子":
                    
                    if (GameManager._Instance.three == 3)
                    {
                        PuzzleCanves.GetComponent<Canvas>().planeDistance = 100;
                    }
                    if (GameManager._Instance.seven == 7)
                    {
                        Open.Play();
                        SetEnableCamera(cameras, 4);
                    }
                    break;
                case "洒火药的位置":
                    if (GameManager._Instance.firstEnd==false)
                    {
                        if (GameManager._Instance.four == 4)
                        {
                            ToTheScene.Play();
                            Instantiate(Powder, hit.collider.transform.position, Quaternion.identity);
                            if (OnclickObj.transform.childCount != 0)
                                Destroy(OnclickObj.transform.GetChild(0).gameObject);
                            ItemModel.DeleteItem(GridName);
                            GameObject.Find("洒火药的位置").GetComponent<BoxCollider>().enabled = false;
                            GameManager._Instance.four = 0;
                        }
                    }
                    
                    break;
                case "火药(Clone)":
                    if (GameManager._Instance.five == 5)
                    {
                        ToTheScene.Play();
                        Instantiate(PowderWick, hit.collider.transform.position, Quaternion.identity);
                        if (OnclickObj.transform.childCount != 0)
                            Destroy(OnclickObj.transform.GetChild(0).gameObject);
                        Destroy(hit.collider.gameObject);
                        ItemModel.DeleteItem(GridName);                       
                        GameManager._Instance.five = 0;
                    }
                    break;
                case "墙体剥落后图片":
                    Destroy(GameObject.Find("墙体剥落后图片"));
                    break;
                case "点燃的墙上火把(Clone)":
                    storeItemToPackage(7, hit.collider.gameObject);
                    break;
                case "插芯的火药(Clone)":
                    if(GameManager._Instance.fivePoint == 11)
                    {
                        DoorOut.Play();
                        GameObject.Find("石门").SetActive(false);
                        Destroy(hit.collider.gameObject);  //销毁插芯的火药
                        if (OnclickObj.transform.childCount != 0)    //从背包中销毁火把
                            Destroy(OnclickObj.transform.GetChild(0).gameObject);
                        ItemModel.DeleteItem(GridName);
                        GameManager._Instance.secondEnd = true;   //开启第二个结局 第一个结局不可见
                    }                                  
                    break;
                case "镐1":
                    storeItemToPackage(10, hit.collider.gameObject);
                    break;
                case "炸开一块的石门":
                    if(GameManager._Instance.six==6)
                    {
                        Gao.Play();
                        GameObject.FindGameObjectWithTag("Door1").SetActive(false);                           
                        if (OnclickObj.transform.childCount != 0)    //从背包中销毁镐
                        Destroy(OnclickObj.transform.GetChild(0).gameObject);
                        ItemModel.DeleteItem(GridName);
                        Instantiate(BrokenDoor, hit.collider.transform.position, Quaternion.identity);
                    }
                    break;
                case "被炸开的石门(Clone)":
                    SetEnableCamera(cameras, 3);
                    GameObject.Find("背包").SetActive(false);
                    isEnd = true;
                    break;
                case "打开的箱子":
                    if (GameManager._Instance.eight == 8)
                    {
                        
                        hit.collider.gameObject.SetActive(false);
                        nextBox.SetActive(true);
                    }
                    break;
                case "石块":
                    storeItemToPackage(11, hit.collider.gameObject);
                    break;
                case "洞":
                    if (GameManager._Instance.secondEnd==false)
                    {
                        if (GameManager._Instance.nine == 9)
                        {
                            GameObject.Find("炸开一块的石门").SetActive(false);
                            ToTheScene.Play();
                            DoorUp.Play();
                            Instantiate(Rock, hit.collider.transform.position, Quaternion.identity);
                            if (OnclickObj.transform.childCount != 0)
                                Destroy(OnclickObj.transform.GetChild(0).gameObject);
                            ItemModel.DeleteItem(GridName);
                            GameManager._Instance.nine = 0;
                            doorMove = true;
                            float x = hit.collider.transform.position.x;
                            float y = hit.collider.transform.position.y;
                            float z = hit.collider.transform.position.z;
                            Instantiate(GoOut, new Vector3(x + 3, y, z), Quaternion.identity);
                            GameManager._Instance.firstEnd = true;  //得到第一个结局  第二个结局不可实施  
                        }
                    }
                    
                    break;
                case "桌子":
                    SetEnableCamera(cameras, 5);
                    break;
                case "出口(Clone)":
                    if (GameManager._Instance.ten == 10)
                    {
                        SetEnableCamera(cameras, 6);
                        GameObject.Find("背包").SetActive(false);
                        isEnd = true;
                    }

                    break;


            }
        }
       
    }
    public void storeItemToPackage(int index,GameObject destroyObject)
    {
        AttainItem.Play();
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
    //设置可用的相机
    public void SetEnableCamera(Camera[] cameras,int EnableIndex)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == EnableIndex)
                cameras[i].gameObject.SetActive(true);
            else
                cameras[i].gameObject.SetActive(false);
        }
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
