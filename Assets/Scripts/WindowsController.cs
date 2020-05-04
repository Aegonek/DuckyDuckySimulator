using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsController : MovementController
{
    private float lastClicked = 0.0f;
    private void Update()
    {
        //base.Update();
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            MovePlayerToTarget(ray);

            if (lastClicked != 0 && Time.time - lastClicked < 0.8f && movementMode != MovementModeEnum.Dashing)
            {
                InitiateDash();
            }
            lastClicked = Time.time;
        }
        CheckForDashEnd();
    }
}
