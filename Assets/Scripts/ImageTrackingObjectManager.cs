using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingObjectManager : MonoBehaviour
{
    public ARTrackedImageManager ImageManager;
    public XRReferenceImageLibrary ImageLibrary;

    // Repeat next 3 fields for each model:
    public GameObject Masonry_wall_textured;
    private GameObject instantiatedMasonry_wall_textured;
    private static Guid s_masonry_wall_hatched_GUID;

    public Text textPos;
    Vector2 pos;
    public Slider maxX;
    public Slider minX;
    public Slider maxY;
    public Slider minY;
    public GameObject panelSettings;
    public Text valueX;
    public Text valueY;


    public void openSettings()
    {
        panelSettings.SetActive(true); 
    }
    public void closeSettings()
    {
        panelSettings.SetActive(false);
    }
    void OnEnable()
    {
        s_masonry_wall_hatched_GUID = ImageLibrary[0].guid;
        ImageManager.trackedImagesChanged += ImageManagerOnTrackedImagesChanged;
    }
    private void FixedUpdate()
    {

        if (instantiatedMasonry_wall_textured.transform.position.x < minX.value | instantiatedMasonry_wall_textured.transform.position.x > maxX.value
            | instantiatedMasonry_wall_textured.transform.position.y < minY.value | instantiatedMasonry_wall_textured.transform.position.y > maxY.value)
            textPos.text = "(0.0, 0.0)";
        else 
        { 
            pos = instantiatedMasonry_wall_textured.transform.position;
            textPos.text = pos.ToString();
        }
        valueX.text = minX.value.ToString() +'\n' + maxX.value.ToString();
        valueY.text = maxY.value.ToString() + '\n' + minX.value.ToString();
    }

    void OnDisable()
    {
        ImageManager.trackedImagesChanged -= ImageManagerOnTrackedImagesChanged;
    }


    void ImageManagerOnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {

        // added, instantiate prefab
        foreach (ARTrackedImage image in obj.added)
        {

            if (image.referenceImage.guid == s_masonry_wall_hatched_GUID)
            {
                instantiatedMasonry_wall_textured = Instantiate(Masonry_wall_textured, image.transform.position, image.transform.rotation);
                instantiatedMasonry_wall_textured.SetActive(false);
                break;
            }
           /* else if (image.referenceImage.guid == s_masonry_footing_hatched_GUID)
            {
                instantiatedMasonry_footing_textured = Instantiate(Masonry_footing_textured, image.transform.position, image.transform.rotation);
                instantiatedMasonry_footing_textured.SetActive(false);
                break;
            }
            else if (image.referenceImage.guid == s_masonry_roof_edge_hatched_GUID)
            {
                instantiatedMasonry_roof_edge_textured = Instantiate(Masonry_roof_edge_textured, image.transform.position, image.transform.rotation);
                instantiatedMasonry_roof_edge_textured.SetActive(false);
                break;
            }
            else if (image.referenceImage.guid == s_roof_ridge_hatched_GUID)
            {
                instantiatedRoof_ridge_textured = Instantiate(Roof_ridge_textured, image.transform.position, image.transform.rotation);
                instantiatedRoof_ridge_textured.SetActive(false);
                break;
            }
            else if (image.referenceImage.guid == s_wooden_wall_hatched_GUID)
            {
                instantiatedWooden_wall_textured = Instantiate(Wooden_wall_textured, image.transform.position, image.transform.rotation);
                instantiatedWooden_wall_textured.SetActive(false);
                break;
            }
            else if (image.referenceImage.guid == s_wooden_footing_hatched_GUID)
            {
                instantiatedWooden_footing_textured = Instantiate(Wooden_footing_textured, image.transform.position, image.transform.rotation);
                instantiatedWooden_footing_textured.SetActive(false);
                break;
            }
            else if (image.referenceImage.guid == s_wooden_roof_edge_hatched_GUID)
            {
                instantiatedWooden_roof_edge_textured = Instantiate(Wooden_roof_edge_textured, image.transform.position, image.transform.rotation);
                instantiatedWooden_roof_edge_textured.SetActive(false);
                break;
            }
            else if (image.referenceImage.guid == s_wooden_window_hatched_GUID)
            {
                instantiatedWooden_window_textured = Instantiate(Wooden_window_textured, image.transform.position, image.transform.rotation);
                instantiatedWooden_window_textured.SetActive(false);
                break;
            }*/

            // TODO Instead of many 'else if' do the switch here and in all next foreach.
        }

        

        //updated, set prefab position and rotation
        foreach (ARTrackedImage image in obj.updated)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                // image is tracking or tracking with limited state, show visuals and update it's position and rotation
                if (image.referenceImage.guid == s_masonry_wall_hatched_GUID)
                {
                    instantiatedMasonry_wall_textured.SetActive(true);
                    instantiatedMasonry_wall_textured.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);


                    break;
                }
                /*else if (image.referenceImage.guid == s_masonry_footing_hatched_GUID)
                {
                    instantiatedMasonry_footing_textured.SetActive(true);
                    instantiatedMasonry_footing_textured.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    break;
                }
                else if (image.referenceImage.guid == s_masonry_roof_edge_hatched_GUID)
                {
                    instantiatedMasonry_roof_edge_textured.SetActive(true);
                    instantiatedMasonry_roof_edge_textured.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    break;
                }
                else if (image.referenceImage.guid == s_roof_ridge_hatched_GUID)
                {
                    instantiatedRoof_ridge_textured.SetActive(true);
                    instantiatedRoof_ridge_textured.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    break;
                }
                else if (image.referenceImage.guid == s_wooden_wall_hatched_GUID)
                {
                    instantiatedWooden_wall_textured.SetActive(true);
                    instantiatedWooden_wall_textured.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    break;
                }
                else if (image.referenceImage.guid == s_wooden_footing_hatched_GUID)
                {
                    instantiatedWooden_footing_textured.SetActive(true);
                    instantiatedWooden_footing_textured.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    break;
                }
                else if (image.referenceImage.guid == s_wooden_roof_edge_hatched_GUID)
                {
                    instantiatedWooden_roof_edge_textured.SetActive(true);
                    instantiatedWooden_roof_edge_textured.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    break;
                }
                else if (image.referenceImage.guid == s_wooden_window_hatched_GUID)
                {
                    instantiatedWooden_window_textured.SetActive(true);
                    instantiatedWooden_window_textured.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    break;
                }*/
            }
            else
            {
                // image is no longer tracking, disable visuals.
                if (image.referenceImage.guid == s_masonry_wall_hatched_GUID)
                {
                    instantiatedMasonry_wall_textured.SetActive(false);
                }
                /*else if (image.referenceImage.guid == s_masonry_footing_hatched_GUID)
                {
                    instantiatedMasonry_footing_textured.SetActive(false);
                }
                else if (image.referenceImage.guid == s_masonry_roof_edge_hatched_GUID)
                {
                    instantiatedMasonry_roof_edge_textured.SetActive(false);
                }
                else if (image.referenceImage.guid == s_roof_ridge_hatched_GUID)
                {
                    instantiatedRoof_ridge_textured.SetActive(false);
                }
                else if (image.referenceImage.guid == s_wooden_wall_hatched_GUID)
                {
                    instantiatedWooden_wall_textured.SetActive(false);
                }
                else if (image.referenceImage.guid == s_wooden_footing_hatched_GUID)
                {
                    instantiatedWooden_footing_textured.SetActive(false);
                }
                else if (image.referenceImage.guid == s_wooden_roof_edge_hatched_GUID)
                {
                    instantiatedWooden_roof_edge_textured.SetActive(false);
                }
                else if (image.referenceImage.guid == s_wooden_window_hatched_GUID)
                {
                    instantiatedWooden_window_textured.SetActive(false);
                }*/
            }
        }



        // removed, destroy instanciated instance
        foreach (ARTrackedImage image in obj.removed)
        {


            if (image.referenceImage.guid == s_masonry_wall_hatched_GUID)
            {
                Destroy(instantiatedMasonry_wall_textured);
            }
            /*else if (image.referenceImage.guid == s_masonry_footing_hatched_GUID)
            {
                Destroy(instantiatedMasonry_footing_textured);
            }
            else if (image.referenceImage.guid == s_masonry_roof_edge_hatched_GUID)
            {
                Destroy(instantiatedMasonry_roof_edge_textured);
            }
            else if (image.referenceImage.guid == s_roof_ridge_hatched_GUID)
            {
                Destroy(instantiatedRoof_ridge_textured);
            }
            else if (image.referenceImage.guid == s_wooden_wall_hatched_GUID)
            {
                Destroy(instantiatedWooden_wall_textured);
            }
            else if (image.referenceImage.guid == s_wooden_footing_hatched_GUID)
            {
                Destroy(instantiatedWooden_footing_textured);
            }
            else if (image.referenceImage.guid == s_wooden_roof_edge_hatched_GUID)
            {
                Destroy(instantiatedWooden_roof_edge_textured);
            }
            else if (image.referenceImage.guid == s_wooden_window_hatched_GUID)
            {
                Destroy(instantiatedWooden_window_textured);
            }*/
        }
    }
}
