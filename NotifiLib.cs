using BepInEx;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GTAG_NotificationLib
{
    [BepInPlugin("64Will64.HabibiLars.NotificationLib", "NotificationLib (Hands)", "1.0")]
    public class NotifiLib : BaseUnityPlugin
    {
        GameObject HUDObj;
        GameObject HUDObj2;
        GameObject MainCamera;
        GameObject LeftHand;
        Text Testtext;
        Material AlertText = new Material(Shader.Find("GUI/Text Shader"));
        int NotificationDecayTime = 150;
        int NotificationDecayTimeCounter = 0;
        string[] Notifilines;
        string newtext;
        public static string PreviousNotifi;
        bool HasInit = false;
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin NotificationLib is loaded!");
        }
        private void Init()
        {
            //this is mostly copy pasted from LHAX, which was also made by me.
            //LHAX got leaked the day before this. so i might as well make this public cus people asked me to.
            MainCamera = GameObject.Find("Main Camera");
            LeftHand = GameObject.Find("LeftHand Controller");
            HUDObj = new GameObject();//GameObject.CreatePrimitive(PrimitiveType.Cube);
            HUDObj2 = new GameObject();
            HUDObj2.name = "NOTIFICATIONLIB_HUD_OBJ";
            HUDObj.name = "NOTIFICATIONLIB_HUD_OBJ";
            HUDObj2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            HUDObj.AddComponent<Canvas>();
            HUDObj.AddComponent<CanvasScaler>();
            HUDObj.AddComponent<GraphicRaycaster>();
            HUDObj.GetComponent<Canvas>().enabled = true;
            HUDObj.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            HUDObj.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
            HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 5);
            //HUDObj.CreatePrimitive()
            HUDObj.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);//new Vector3(-67.151f, 11.914f, -82.749f);
            HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - 4.6f);
            HUDObj.transform.parent = HUDObj2.transform;
            HUDObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
            var Temp = HUDObj.GetComponent<RectTransform>().rotation.eulerAngles;
            Temp.y = -270f;
            HUDObj.transform.localScale = new Vector3(1f, 1f, 1f);
            HUDObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(Temp);
            GameObject TestText = new GameObject();
            TestText.transform.parent = HUDObj.transform;
            TestText.transform.localEulerAngles = new Vector3(89.9802f, 285.9983f, 0);
            Testtext = TestText.AddComponent<Text>();
            Testtext.text = "";
            Testtext.fontSize = 10;
            Testtext.font = GameObject.Find("COC Text").GetComponent<Text>().font;
            Testtext.rectTransform.sizeDelta = new Vector2(260, 70);
            Testtext.alignment = TextAnchor.LowerLeft;
            Testtext.rectTransform.localScale = new Vector3(0.01f, 0.01f, 1f);
            Testtext.rectTransform.localPosition = new Vector3(-1.5f, -.9f, -.6f);
            Testtext.material = AlertText;
        }
        
        private void FixedUpdate()
        {
            //This is a bad way to do this.
            if(HasInit == false)
            {
                if(GameObject.Find("Main Camera") != null)
                {
                    Init();
                    HasInit = true;
                }
            }
            HUDObj.transform.position = new Vector3(LeftHand.transform.position.x, LeftHand.transform.position.y, LeftHand.transform.position.z);
            HUDObj.transform.rotation = LeftHand.transform.rotation;
            //HUDObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
            if (Testtext.text != "") //THIS CAUSES A MEMORY LEAK!!!!! -no longer causes a memory leak
            {
                NotificationDecayTimeCounter++;
                if (NotificationDecayTimeCounter > NotificationDecayTime)
                {
                    Notifilines = null;
                    newtext = "";
                    NotificationDecayTimeCounter = 0;
                    Notifilines = Testtext.text.Split(Environment.NewLine.ToCharArray()).Skip(1).ToArray();
                    foreach (string Line in Notifilines)
                    {
                        if (Line != "")
                        {
                            newtext = newtext + Line + "\n";
                        }
                    }

                    Testtext.text = newtext;
                }
            }
            else
            {
                NotificationDecayTimeCounter = 0;
            }
        }

        public static void SendNotification(string NotificationText)
        {
            GameObject thing = GameObject.Find("NOTIFICATIONLIB_HUD_OBJ/NOTIFICATIONLIB_HUD_OBJ");
            if (!NotificationText.Contains(Environment.NewLine)) { NotificationText = NotificationText + Environment.NewLine;  }
            thing.transform.GetChild(0).GetComponent<Text>().text = thing.transform.GetChild(0).GetComponent<Text>().text + NotificationText;
            PreviousNotifi = NotificationText;
        }
        public static void ClearAllNotifications()
        {
            GameObject thing = GameObject.Find("NOTIFICATIONLIB_HUD_OBJ/NOTIFICATIONLIB_HUD_OBJ");
            thing.transform.GetChild(0).GetComponent<Text>().text = "";
        }
        public static void ClearPastNotifications(int amount)
        {
            GameObject thing = GameObject.Find("NOTIFICATIONLIB_HUD_OBJ/NOTIFICATIONLIB_HUD_OBJ");
            string[] Notifilines = null;
            string newtext = "";
            Notifilines = thing.transform.GetChild(0).GetComponent<Text>().text.Split(Environment.NewLine.ToCharArray()).Skip(amount).ToArray();
            foreach (string Line in Notifilines)
            {
                if (Line != "")
                {
                    newtext = newtext + Line + "\n";
                }
            }

            thing.transform.GetChild(0).GetComponent<Text>().text = newtext;
        }
    }
}
