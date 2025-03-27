using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public UnityEngine.UI.InputField SerialPortNameInput;
    public UnityEngine.UI.Text SerialPortStatusText;
    public SuzuKeyDialReader SuzuKeyDialReader;

    public Transform[] ObjectForSwitches;
    public Transform[] ObjectForDials;

    public Material MaterialReleased;
    public Material MaterialPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSwitch();
        UpdateDial();
    }

    public void OnConnectButtonClicked() {
        string portName = this.SerialPortNameInput.text;
        if (!this.SuzuKeyDialReader.OpenPort(portName)) {
            this.SerialPortStatusText.text = "接続失敗";

        } else {
            this.SerialPortStatusText.text = "接続中";
        }
    }

    public void OnDisconnectButtonClicked() {
        this.SuzuKeyDialReader.Stop();
        this.SerialPortStatusText.text = "切断";
    }

    void UpdateSwitch() {
        for (int i = 0; i < 6; i++) {
            Material mat;
            if (this.SuzuKeyDialReader.CurrentState.Switches[i]) {
                mat = this.MaterialPressed;
            } else {
                mat = this.MaterialReleased;
            }
            this.ObjectForSwitches[i].gameObject.GetComponent<Renderer>().material = mat;
        }
    }

    void UpdateDial() {
        this.ObjectForDials[0].Rotate(Vector3.up, this.SuzuKeyDialReader.CurrentState.Dial1 * 4, Space.World);
        this.ObjectForDials[1].Rotate(Vector3.up, this.SuzuKeyDialReader.CurrentState.Dial2 * 4, Space.World);
        this.SuzuKeyDialReader.ResetDialState();
    }
}
