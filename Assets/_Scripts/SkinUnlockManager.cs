using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnlockManager : MonoSingleton<SkinUnlockManager>
{
    public List<SkinButton> buttons;

    private bool currentlyUnlocking = false;
    public AnimationCurve unlockTiming;
    public int swapAmount;
    public Color highlightColor;
    public Color normalColor;

    [Header("Punch Settings")]
    public float strength;
    public float vibrato;
    public float time;

    public void Start(){
        Setup();
    }

    private void Setup(){
        while(PersistentData.instance.unlockData.data.unlocks.Count!=buttons.Count){
            PersistentData.instance.unlockData.data.unlocks.Add(false);
        }
        int counter = 0;
        foreach (var item in buttons)
        {
            item.Unhighlight();
            if(PersistentData.instance.unlockData.data.unlocks[counter]){
                item.Unlock();
            }

            counter++;
        }
    }

    public void UnlockClicked(){
        if(currentlyUnlocking){
            return;
        }
        StartCoroutine(UnlockRoutine());
    }

    IEnumerator UnlockRoutine(){
        currentlyUnlocking = true;
        List<SkinButton> locked = new List<SkinButton>();
        foreach (var item in buttons)
        {
            if(!item.unlocked){
                locked.Add(item);
            }
        }
        if(locked.Count == 0){
            currentlyUnlocking = false;
            SaveToData();
            yield break;
        }

        if(locked.Count>1){
            float ratio = 0;
            int random;
            for (int i = 0; i < swapAmount; i++)
            {
                ratio = (i+0.0f)/(swapAmount-1f);
                float waitAmount = unlockTiming.Evaluate(ratio);

                random = Random.Range(0,locked.Count);
                while(locked[random] == SkinButton.Highlighted){
                    random = Random.Range(0,locked.Count);
                }
                SkinButton.Highlighted = locked[random];


                yield return new WaitForSeconds(waitAmount);
            }


        }else{
            SkinButton.Highlighted =locked[0];
        }
        SkinButton.Highlighted.Unlock();
        SkinButton.Highlighted.PunchScale(strength,time,vibrato);
        SaveToData();


        currentlyUnlocking = false;
    }

    private void SaveToData(){
        int counter = 0;
        foreach (var item in buttons)
        {
            PersistentData.instance.unlockData.data.unlocks[counter] = item.unlocked;

            counter++;
        }
        PersistentData.instance.SaveData();
    }
}
