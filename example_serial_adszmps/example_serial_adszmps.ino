// Suzuno32RVでは必要なので、コメントアウトを解除してください。
// あわせて、ツール→USB SupportからUSB機能を有効にしてください。
// #include <Adafruit_TinyUSB.h>

// ライブラリ "RotaryEncoder by Matthias Hertel" のインストールが必要です
#include <RotaryEncoder.h>

#define PIN_SW1 4
#define PIN_SW2 5
#define PIN_SW3 6
#define PIN_SW4 7
#define PIN_SW5 10
#define PIN_SW6 11

RotaryEncoder encoder1(A2, A3, RotaryEncoder::LatchMode::FOUR3);
RotaryEncoder encoder2(A4, A5, RotaryEncoder::LatchMode::FOUR3);


void setup() {
  Serial.begin(115200);
  pinMode(PIN_SW1, INPUT_PULLUP);
  pinMode(PIN_SW2, INPUT_PULLUP);
  pinMode(PIN_SW3, INPUT_PULLUP);
  pinMode(PIN_SW4, INPUT_PULLUP);
  pinMode(PIN_SW5, INPUT_PULLUP);
  pinMode(PIN_SW6, INPUT_PULLUP);
}

void loop() {
  static unsigned long timer = 0;

  // エンコーダーの状態をチェックします
  encoder1.tick();
  encoder2.tick();

  // 50ミリ秒ごとに状態を送信します
  if (millis() - timer >= 50) {
    bool sw1 = digitalRead(PIN_SW1) == LOW;
    bool sw2 = digitalRead(PIN_SW2) == LOW;
    bool sw3 = digitalRead(PIN_SW3) == LOW;
    bool sw4 = digitalRead(PIN_SW4) == LOW;
    bool sw5 = digitalRead(PIN_SW5) == LOW;
    bool sw6 = digitalRead(PIN_SW6) == LOW;

    Serial.print(sw1 ? "1" : "0");
    Serial.print(sw2 ? "1" : "0");
    Serial.print(sw3 ? "1" : "0");
    Serial.print(sw4 ? "1" : "0");
    Serial.print(sw5 ? "1" : "0");
    Serial.print(sw6 ? "1" : "0");
    Serial.print(",");
    Serial.print(encoder1.getPosition());
    Serial.print(",");
    Serial.print(encoder2.getPosition());

    Serial.print("\r\n");

    // エンコーダーの値を0に戻します
    encoder1.setPosition(0);
    encoder2.setPosition(0);

    timer = millis();
  }
}example_serial_adszmps.ino
