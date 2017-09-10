#include <SoftwareSerial.h>

// Bluetooth
const int rxpin = 13; // Receiver 
const int txpin = 12; // Sender 
SoftwareSerial bluetooth(rxpin, txpin);

// Motor
const int motorStepPin = 5; // Motor 
const int directionLedPin = 2; // Direction 
bool isMotorRunning = false;
int motorDirection = 0; // 0 = left, 1 = right
int speed = 300;

// Serial
const char endMarker = '#';
const byte numChars = 32;
char receivedChars[numChars];
boolean newData = false;

// Camera
const int shutterPin = 11;
int exposureTime = 8;
bool isShutterToBeTriggered = false;

// Procedure
bool isProcedureRunning = false;
int stepsPerInterval = 200; 
int shots = 5;

int maxExposureTime = 4000; 
const int bufferTime = 250;

void setup()
{  
  Serial.begin(9600);
  bluetooth.begin(9600);

  Serial.println("Serial ready");
  bluetooth.println("Bluetooth ready");

  pinMode(motorStepPin, OUTPUT);
  pinMode(directionLedPin, OUTPUT);
  pinMode(shutterPin, OUTPUT);
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

  // Procedure
  if (isProcedureRunning)
  {
    for (int i = 0; i < shots; i++)
    {

      // Direction
      if (motorDirection == 0)
        digitalWrite(directionLedPin, LOW);
      else
        digitalWrite(directionLedPin, HIGH);

      // Motor
      for(int j = 0; j < stepsPerInterval; j++)
      {
        digitalWrite(motorStepPin,HIGH);
        delay(1);
        digitalWrite(motorStepPin,LOW);
        delay(1);
      }

      // Turn off direction pin
      digitalWrite(directionLedPin, LOW);
 
      //digitalWrite(en1, HIGH);
      delay (bufferTime);
      digitalWrite(shutterPin, HIGH);
      delay(exposureTime);
      digitalWrite(shutterPin, LOW);
      delay (maxExposureTime);
      delay (bufferTime);
      //digitalWrite(en1, LOW);
    }

    isProcedureRunning = false;
    Serial.println("Finished procedure.");
  }



  // Shutter
  if (isShutterToBeTriggered)
  {
    Serial.println("Shutter!");
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
    digitalWrite(motorStepPin, HIGH);
    delayMicroseconds(speed);
    digitalWrite(motorStepPin, LOW);
    delayMicroseconds(speed);      
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
    if (command.indexOf("sp") != -1)
      speed = command.substring(2).toInt();

    // Camera
    if (strcmp(receivedChars, "shutter") == 0)
      isShutterToBeTriggered = true;

    // Exposure Time
    if (command.indexOf("et") != -1)
      exposureTime = command.substring(2).toInt();

    // Procedure
    if (command.indexOf("pr") != -1) // Example: pr100,10
    { 
      // Get procedure values out of command      
      Serial.println("Procedure command detected.");      
      
      stepsPerInterval = command.substring(2, command.indexOf(",")).toInt();
      shots = command.substring(command.indexOf(",") + 1, command.lastIndexOf(",")).toInt();
      maxExposureTime = command.substring(command.lastIndexOf(",") + 1).toInt();

      Serial.print("Steps per Interval: ");
      Serial.println(stepsPerInterval);
      Serial.print("Number of shots: ");
      Serial.println(shots);
      Serial.print("Max exposure time: ");
      Serial.println(maxExposureTime);

      Serial.println("Starting procedure...");
      isProcedureRunning = true;
    }
 
    newData = false;
  }
}

