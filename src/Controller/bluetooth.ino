#include <SoftwareSerial.h>

const int rxpin = 2; // Receiver
const int txpin = 3; // Sender
const int ledPin = 5;

SoftwareSerial bluetooth(rxpin, txpin);

void setup() {
  Serial.begin(9600);
  bluetooth.begin(9600);

  Serial.println("Serial ready");
  bluetooth.println("Bluetooth ready");

  pinMode(ledPin, OUTPUT);
}

void loop() {

  if (bluetooth.available())
  {
    char c = (char)bluetooth.read();
    Serial.write(c);

    if (c == '1')
    {
      digitalWrite(ledPin, HIGH);
    } else if (c == '0') {
      digitalWrite(ledPin, LOW);
    }
  }

  if (Serial.available())
  {
    char c = (char)Serial.read();
    bluetooth.write(c);
  }
}
