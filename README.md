# （仮）マクロパッドシールド基板

キースイッチ4つとロータリーエンコーダーを2つ搭載した、Arduino UNOシールド基板です。UNO互換ボードに差し込んで入力インターフェースとして利用できます。

Arduino UNO R4, Arduino Leonardo, Suzuno32RV, Suzuduino UNOなどのUSB機能内蔵マイコンボードと組み合わせれば、USBマクロパッドの製作にも活用できます。

注意： Arduino UNO **R3** (AVR ATmega328P) はUSB入力デバイス（キーボード・マウス相当）としては利用できません。（UNO R3はUSBシリアル通信のみの対応です。）

This is a UNO-compatible shield board with 4 key switches and 2 rotary encoders for input interface.

With Arduino UNO R4, Arduino Leonardo, Suzuno32RV or Suzuduino UNO, you can build a USB macropad.

NOTE: Arduino UNO **R3** doesn't have a USB-HID function (NOT working as USB keyboard and mouse). Only USB-Serial function is available through USB port.

## サンプルコード / Sample code

**Arduino UNO R4, Arduino Leonardo**

Arduino UNO R4とArduino LeonardoはArduino純正ボードで、USB機能を内蔵しています。基本的なUSB入力デバイスとして利用できます。

ライブラリ "RotaryEncoder by Matthias Hertel" をインストールしてください。

example_arduino_adszmps/example_arduino_adszmps.ino

**Suzuno32RV / Suzuduino UNO**

Suzuno32RV / Suzuduino UNOは、RISC-VマイコンWCH CH32V203を搭載したUNO形状のマイコンボードです。TinyUSBライブラリを活用して、さらに深いカスタマイズができます。

ライブラリ "RotaryEncoder by Matthias Hertel" をインストールしてください。

example_suzuno32rv_adszmps/example_suzuno32rv_adszmps.ino

## ピン割り当て / Pin assignment

| 機能 | UNO R4, Leonardo | Suzuno32RV |
|---|---|---|
| Switch 1 | 4 | PA15 |
| Switch 2 | 5 | PB3 |
| Switch 3 | 6 | PB4 |
| Switch 4 | 7 | PB5 |
| Rotary Encoder 1 A | A2 | PA2 |
| Rotary Encoder 1 B | A3 | PA3 |
| Rotary Encoder 1 Push (SW5) | 10 | PA4 |
| Rotary Encoder 2 A | A4 | PB0 |
| Rotary Encoder 2 B | A5 | PB1 |
| Rotary Encoder 2 Push (SW6) | 11 | PA7 |
