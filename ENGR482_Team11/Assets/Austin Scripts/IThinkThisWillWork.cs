﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    /// <summary>
    /// Working off of the NavigationRotateResponder.cs and GazeResponder.cs to make a script that rotates an object when gaze input is active
    /// </summary>
    public class IThinkThisWillWork : MonoBehaviour, IFocusable//, IInputHandler
    {
        [Tooltip("Rotation speed controls the speed of rotation. (Degrees/sec I think)")]
        public float rotationSpeed = 1.0f;

        public bool rotating;

        // Use this for initialization
        void Start()
        {
            Debug.Log("Rotation Speed is: " + rotationSpeed);
        }

        // Update is called once per frame
        void Update()
        {
            if(rotating)
            {
                // Rotate object along the Y axis using.
                transform.Rotate(Vector3.up * rotationSpeed, Space.World);
            }
        }

        public void OnFocusEnter()
        {
            // Rotate object on Y-axis when gaze enters.
            rotating = true;
            if (gameObject.GetComponent<Billboard>())
                gameObject.GetComponent<Billboard>().enabled = false; //Disable billboarding to enable rotation
        }

        public void OnFocusExit()
        {
            // Stop rotation when gaze exits.
            
            if (gameObject.GetComponent<Billboard>())
            {
                StartCoroutine(ExecuteAfterTime(.3f));// wait a bit to make rotation smoother if gazing at edge of a disc or something and it rotates out of view
                gameObject.GetComponent<Billboard>().enabled = true; //Enable billboarding again

            }
            rotating = false;
        }

        //public void OnInputDown(InputEventData eventData)
        //{
        //    //rotationSpeed = rotationSpeed * 2;
        //}

        //public void OnInputUp(InputEventData eventData)
        //{
        //    //rotationSpeed = rotationSpeed / 2;
        //}

        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
        }

    }

}

