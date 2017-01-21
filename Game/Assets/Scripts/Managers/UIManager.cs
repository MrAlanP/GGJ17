using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager :Singleton<UIManager> {

    public bool inObject;
    public bool useable;
    public enum Alert {stage1, stage2, stage3, stage4, stage5 };
    public Alert alert;
    public Image Echo;
    public Image Foxtrot;
    public Image Alert_One;
    public Image Alert_Two;
    public Image Alert_Three;
    public Image Alert_Four;
    public Image Alert_Five;

    void Start() {

    }

    void Update() {
        if (inObject)
        {
            Echo.GetComponent<Image>().enabled = true;
        }
        else
        {
            Echo.GetComponent<Image>().enabled = false;
        }
        if (useable)
        {
            Foxtrot.GetComponent<Image>().enabled = true;
        }
        else
        {
            Foxtrot.GetComponent<Image>().enabled = false;
        }

        if (alert == Alert.stage1)
        {
            Alert_One.GetComponent<Image>().enabled = true;
            Alert_Two.GetComponent<Image>().enabled = false;
            Alert_Three.GetComponent<Image>().enabled = false;
            Alert_Four.GetComponent<Image>().enabled = false;
            Alert_Five.GetComponent<Image>().enabled = false;
        }
        if (alert == Alert.stage2)
        {
            Alert_One.GetComponent<Image>().enabled = false;
            Alert_Two.GetComponent<Image>().enabled = true;
            Alert_Three.GetComponent<Image>().enabled = false;
            Alert_Four.GetComponent<Image>().enabled = false;
            Alert_Five.GetComponent<Image>().enabled = false;
        }
        if (alert == Alert.stage3)
        {
            Alert_One.GetComponent<Image>().enabled = false;
            Alert_Two.GetComponent<Image>().enabled = false;
            Alert_Three.GetComponent<Image>().enabled = true;
            Alert_Four.GetComponent<Image>().enabled = false;
            Alert_Five.GetComponent<Image>().enabled = false;
        }
        if (alert == Alert.stage4)
        {
            Alert_One.GetComponent<Image>().enabled = false;
            Alert_Two.GetComponent<Image>().enabled = false;
            Alert_Three.GetComponent<Image>().enabled = false;
            Alert_Four.GetComponent<Image>().enabled = true;
            Alert_Five.GetComponent<Image>().enabled = false;
        }
        if (alert == Alert.stage5)
        {
            Alert_One.GetComponent<Image>().enabled = false;
            Alert_Two.GetComponent<Image>().enabled = false;
            Alert_Three.GetComponent<Image>().enabled = false;
            Alert_Four.GetComponent<Image>().enabled = false;
            Alert_Five.GetComponent<Image>().enabled = true;
        }
    }
}
