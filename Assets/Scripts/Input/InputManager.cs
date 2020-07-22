using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField]
    private InputMethod inputMethod;

    public bool AllowCameraInput { get; set; }
    public bool AllowPlayerInput { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        inputMethod = Instantiate(inputMethod);
        AllowCameraInput = true;
        AllowPlayerInput = true;
    }
    public InputMethod GetInputMethod() 
    {
        return inputMethod;
    }
}
