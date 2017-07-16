#include <SoftwareSerial.h>

// Bluetooth
const int rxpin = 2; // Receiver
const int txpin = 3; // Sender
SoftwareSerial bluetooth(rxpin, txpin);

// Motor
const int motorLedPin = 10; // Motor
const int directionLedPin = 11; // Direction
bool isMotorRunning = false;
int motorDirection = 0; // 0 = left, 1 = right
int speed = 300;

// Serial
const char endMarker = '#';
const byte numChars = 32;
char receivedChars[numChars];
boolean newData = false;

// Camera
const int shutterPin = 0;
const int exposureTime = 100;
bool isShutterToBeTriggered = false;

void setup()
{  
  Serial.begin(9600);
  bluetooth.begin(9600);

  Serial.println("Serial ready");
  bluetooth.println("Bluetooth ready");

  pinMode(motorLedPin, OUTPUT);
  pinMode(directionLedPin, OUTPUT);
}

void loop()
{
  // Write serial data also to bluetooth
  if (Serial.available())
  {    
    char c = (char)Serial.read();
    Serial.write(c);
    bluetooth.write(c);
  }

  readBluetoothSerial();
  processBluetoothInput();

  // Shutter
  if (isShutterToBeTriggered)
  {
    digitalWrite(shutterPin, HIGH);
    delay(exposureTime);
    digitalWrite(shutterPin, LOW);
    isShutterToBeTriggered = false;
  }

  // Move 
  if (isMotorRunning)
  {
    // Direction
    if (motorDirection == 0)
      digitalWrite(directionLedPin, LOW);
    else
      digitalWrite(directionLedPin, HIGH);
      
    // Step
    digitalWrite(motorLedPin, HIGH);
    delay(speed);
    digitalWrite(motorLedPin, LOW);
    delay(speed);      
  }
  else
  {
    digitalWrite(directionLedPin, LOW);
  }
}

void readBluetoothSerial()
{
  static byte ndx = 0;
  char rc;
    
  while (bluetooth.available() > 0 && newData == false)
  {
    rc = bluetooth.read();

    if (rc != endMarker)
    {
      receivedChars[ndx] = rc;
      ndx++;
      
      if (ndx >= numChars) {
        ndx = numChars - 1;
      }
    } else {
      receivedChars[ndx] = '\0'; // terminate the string
      ndx = 0;
      newData = true;
    }
  }
}

void processBluetoothInput()
{
  if (newData)
  {
    String command(receivedChars);
    
    Serial.print("Command: ");
    Serial.println(command);

    // Motor
    if (strcmp(receivedChars, "on") == 0)
      isMotorRunning = true;
    else if (strcmp(receivedChars, "off") == 0)
      isMotorRunning = false;

    // Direction
    if (strcmp(receivedChars, "dl") == 0) // left
      motorDirection = 0;
    else if (strcmp(receivedChars, "dr") == 0) // right
      motorDirection = 1;

    // Speed
    if (command.indexOf("sp") > -1)
      speed = command.substring(2).toInt();

    // Camera
    if (strcmp(receivedChars, "shutter") == 0)
      isShutterToBeTriggered = true;
 
    newData = false;
  }
}

