using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

// Set Edit -> Project Settings -> Player -> Other Settings -> Api Compatibility Level to ".NET Framework"
using System.IO.Ports;
using System.Threading;
using System;
using System.IO;

public class SuzuKeyDialReader : MonoBehaviour
{
    [Serializable]
    public class State {
        public bool[] Switches;

        public int Dial1;
        public int Dial2;

        public State Clone() {
            State newState = new State() {
                Switches = (bool[])this.Switches.Clone(),
                Dial1 = this.Dial1, Dial2 = this.Dial2
            };
            return newState;
        }
    }

    public string SerialPortName;
    public int BaudRate = 115200;

    public Task SerialPortTask;

    public State CurrentState = new State() { Switches = new bool[] { }, Dial1 = 0, Dial2 = 0 };

    SerialPort SerialPort;

    CancellationTokenSource TaskCancellationTokenSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool OpenPort(string portName) {
        if (this.SerialPort != null || this.SerialPortTask != null) {
            this.Stop();
        }

        this.SerialPort = new SerialPort(portName, this.BaudRate);
        this.SerialPort.DtrEnable = true;
        this.SerialPort.RtsEnable = true;
        try {
            this.SerialPort.Open();
            this.SerialPort.ReadTimeout = 100;
        } catch (IOException excep) {
            Debug.Log(excep);
            this.SerialPort = null;
            return false;
        }

        this.TaskCancellationTokenSource = new CancellationTokenSource();

        this.SerialPortTask = Task.Run(this.SerialPortTaskMain);

        return true;
    }

    public void Stop() {
        if (this.TaskCancellationTokenSource != null) {
            this.TaskCancellationTokenSource.Cancel();
        }
        if (this.SerialPortTask != null) {
            this.SerialPortTask.Wait();
        }

        if (this.SerialPort != null) {
            if (this.SerialPort.IsOpen) {
                this.SerialPort.Close();
            }
        }

        this.TaskCancellationTokenSource = null;
        this.SerialPortTask = null;
        this.SerialPort = null;
    }

    public void ResetDialState() {
        this.CurrentState.Dial1 = 0;
        this.CurrentState.Dial2 = 0;
    }

    void SerialPortTaskMain() {
        while (!this.TaskCancellationTokenSource.IsCancellationRequested) {
            try {
                string line = this.SerialPort.ReadLine();
                Debug.Log(line);

                var elems = line.Split(",");
                if (elems.Length != 3) {
                    continue;
                }
                for (int i = 0; i < 6; i++) {
                    this.CurrentState.Switches[i] = elems[0][i] == '1';
                }
                this.CurrentState.Dial1 += int.Parse(elems[1]);
                this.CurrentState.Dial2 += int.Parse(elems[2]);

            } catch (TimeoutException) {
                // Nop

            } catch (Exception excep) {
                Debug.LogError(excep);
            }
        }
    }
}
