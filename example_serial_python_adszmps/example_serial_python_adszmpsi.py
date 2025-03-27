from typing import List, Dict, Any, Optional, NoReturn
import sys
import time
from serial import Serial
import serial.tools.list_ports
from threading import Thread


class SuzuKeyDialReader:
    def __init__(self) -> None:
        self.switches = [ False ] * 6
        self.dials = [ 0 ] * 2
        self.serialport: Optional[Serial] = None
        self.run_read_serial_main = False
        self.read_serial_thread: Optional[Thread] = None

    def open(self, portname: str, baudrate: int) -> None:
        self.stop()
        self.serialport = Serial(portname, baudrate, timeout=1)
        self.run_read_serial_main = True
        self.read_serial_thread = Thread(target=self.read_serial_main)
        self.read_serial_thread.start()

    def stop(self) -> None:
        if self.read_serial_thread:
            self.run_read_serial_main = False
            self.read_serial_thread.join(5)
            self.read_serial_thread = None
        if self.serialport:
            self.serialport.close()
            self.serialport = None

    def read_serial_main(self) -> None:
        print("")
        while self.run_read_serial_main and self.serialport.is_open:
            input = self.serialport.readline()
            if input is None or len(input) == 0:
                continue
            try:
                input = input.decode('ascii')
                elems = input.split(',')
                for i in range(6):
                    self.switches[i] = elems[0][i] == '1'
                self.dials[0] = int(elems[1])
                self.dials[1] = int(elems[2])

                self.print_state()

            except Exception as excep:
                print(excep)
                continue

    def print_state(self) -> None:
        print("\r", end='')
        print("SW: ", end='')
        for sw in self.switches:
            print("*" if sw else "-", end="")
            print(" ", end='')
        print('  Dials: ', end='')
        for dial in self.dials:
            if dial == 0:
                print('--', end='')
            elif dial > 0:
                print('>>', end='')
            else:
                print('<<', end='')
            print(' ', end='')
        sys.stdout.flush()


    def reset_dial_state(self) -> None:
        self.dials[0] = 0
        self.dials[1] = 0


class App:
    def __init__(self):
        self.suzukeydialreader = SuzuKeyDialReader()
        self.portname = ""
        self.baudrate = 115200

    def main(self) -> NoReturn:
        if len(sys.argv) <= 1:
            self.print_help()
            exit(-1)
        self.portname = sys.argv[1]
        self.suzukeydialreader.open(self.portname, self.baudrate)
        print("Start")
        try:
            while True:
                time.sleep(0.1)
        except KeyboardInterrupt:
            print("Stop")

        finally:
            self.suzukeydialreader.stop()
            print("Bye.")


    def print_help(self) -> None:
        print("""
SuzuKeyDial Python example; communicate over USB-Serial

Usage:
    python example_serial_python_adszmps.py (SerialPortName)

Available Serial ports:
""".strip())
        available_ports = serial.tools.list_ports.comports()
        for port in available_ports:
            print(f"\t{port.name}")


if __name__ == '__main__':
    App().main()
