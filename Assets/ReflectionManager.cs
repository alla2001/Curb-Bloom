using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReflectionManager : SingletonMonoBehaviour<ReflectionManager>
{
    private enum state
    {
        fadein, stay, fadout
    }

    private state imageState;
    public float time = 3;
    public List<Sprite> sprites = new List<Sprite>();
    public Image image;
    public Image bgimage;

    public void ShowReflection()

    {
        image.sprite = sprites[(int)(PlayerStateManager.instance.Degradation / 0.2f)];
        StartCoroutine(wait());
    }

    private IEnumerator wait()
    {
        MovementController.instance.freez = true;
        image.enabled = true;
        bgimage.enabled = true;
        imageState = state.fadein;
        yield return new WaitForSeconds(time / 2);
        imageState = state.stay;
        yield return new WaitForSeconds(time / 2);
        imageState = state.fadout;
        image.enabled = false;
        bgimage.enabled = false;
        MovementController.instance.freez = false;
    }

    private void Update()
    {
        switch (imageState)
        {
            case state.fadein:
                image.color = Vector4.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 1), 4f * Time.deltaTime);
                bgimage.color = Vector4.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 1), 4f * Time.deltaTime);
                break;

            case state.fadout:
                image.color = Vector4.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 0), 4f * Time.deltaTime);
                bgimage.color = Vector4.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 0), 4f * Time.deltaTime);
                break;
        }
    }
}