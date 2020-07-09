using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    private static List<SkinButton> AllSkinButtons = new List<SkinButton>();

    private static SkinButton _highlighted = null;
    public static SkinButton Highlighted{
        get{
            return _highlighted;
        }
        set{
            if(_highlighted==value){
                return;
            }
            if(_highlighted!=null){
                _highlighted.Unhighlight();
            }
            _highlighted = value;
            if(_highlighted!=null)
            _highlighted.Highlight();
        }
    }



    public Sprite targetSkin;
    public bool unlocked = false;
    public Image image;

    public void Start(){
        AllSkinButtons.Add(this);
        GetComponent<Button>().onClick.AddListener(OnClick);
    }


    public void Highlight(){
        GetComponent<Image>().color = SkinUnlockManager.instance.highlightColor;
        GetComponent<RectTransform>().localScale = Vector3.one*1.05f;
    }

    public void Unhighlight(){
        GetComponent<Image>().color = SkinUnlockManager.instance.normalColor;
        GetComponent<RectTransform>().localScale = Vector3.one*1;
    }


    private Coroutine punchScaleRoutine;

    public void PunchScale(float strength = 10,float time = 1,float vibrato = 10){
        if(punchScaleRoutine!=null){
            StopCoroutine(punchScaleRoutine);
        }
        punchScaleRoutine = StartCoroutine(punchScale(strength,time,vibrato));
    }

    IEnumerator punchScale(float strength,float totalTime,float vibrato){
        //using sinus
        float startingValue = GetComponent<RectTransform>().localScale.x;
        float endValue = 1;
        float currentVal = startingValue;
        float vibratoValue = 0;

        float time = 0;
        while(true){
            vibratoValue = Mathf.Sin(time*vibrato)*strength*(totalTime-time)/totalTime;
            currentVal = Mathf.Lerp(startingValue,endValue,time/totalTime);
            if(time>=totalTime){
                time = totalTime;
            }

            GetComponent<RectTransform>().localScale = Vector3.one*(currentVal+vibratoValue);
            if(time>=totalTime){
                break;
            }

            time += Time.deltaTime;
            yield return null;
        }
        GetComponent<RectTransform>().localScale = Vector3.one*(endValue);
        yield return null;
    }

    public void Unlock(){
        unlocked = true;
        image.sprite = targetSkin;
    }

    public void OnClick(){
        Highlighted = this;
    }

    
    
}
