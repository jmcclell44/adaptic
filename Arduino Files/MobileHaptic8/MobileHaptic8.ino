#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BNO055.h>
#include <utility/imumaths.h>
#include <EEPROM.h>
#include <Servo.h>

/* Set the delay between fresh samples */
#define BNO055_SAMPLERATE_DELAY_MS (100)

Adafruit_BNO055 bno = Adafruit_BNO055(55);

Servo myservo00;
Servo myservo01;
Servo myservo02;
Servo myservo03;
Servo myservo04;
Servo myservo05;
Servo myservo06;
Servo myservo07;
Servo myservo08;
Servo myservo09;
Servo myservo10;
Servo myservo11;

float pos00 = 90;
float pos01 = 90;
float pos02 = 90;
float pos03 = 90;
float pos04 = 90;
float pos05 = 90;
//float pos06 = 90;
//float pos07 = 90;
//float pos08 = 90;
//float pos09 = 90;
//float pos10 = 90;
//float pos11 = 90;

float Aread00;
float Aread01;
float Aread02;
float Aread03;
float Aread04;
float Aread05;

float bending;

const byte numChars = 64;
char receivedChars[numChars];
char tempChars[numChars];        // temporary array for use when parsing

      // variables to hold the parsed data
char messageFromPC[numChars] = {0};

boolean newData = false;

int counter = 0;

void setup(void)
{
  //bno.begin(bno.OPERATION_MODE_IMUPLUS);
 
  Serial.begin(115200);
//  while(!Serial || millis()<10000);
  //Serial.println("Orientation Sensor Test"); Serial.println("");

  /* Initialise the sensor */
  if(!bno.begin())
  {
    /* There was a problem detecting the BNO055 ... check your connections */
    Serial.print("Ooops, no BNO055 detected ... Check your wiring or I2C ADDR!");
    while(1);
  }


  bending = 0;

  bno.setExtCrystalUse(true);
}


void loop() {
  bno.setMode(bno.OPERATION_MODE_IMUPLUS);
  // put your main code here, to run repeatedly:
  String input = "";
  
  imu::Quaternion quat = bno.getQuat();
  
  uint8_t system, gyro, accel, mag;
  system = gyro = accel = mag = 0;
  bno.getCalibration(&system, &gyro, &accel, &mag);

  Aread00 = analogRead(A14);
  Aread01 = analogRead(A16);
  Aread02 = analogRead(A15);
  Aread03 = analogRead(A17);
  Aread04 = analogRead(A18);
  Aread05 = analogRead(A19);
  
    recvWithStartEndMarkers();
    if (newData == true) {
        strcpy(tempChars, receivedChars);
            // this temporary copy is necessary to protect the original data
            //   because strtok() used in parseData() replaces the commas with \0
        parseData();
        newData = false;
    }


  if (bending==1){
  if (counter == 0){
  myservo00.attach(0);
  myservo01.attach(1);
  myservo02.attach(2);
  myservo03.attach(3);
  myservo04.attach(4);
  myservo05.attach(5);
  myservo06.attach(6);
  myservo07.attach(7);
  myservo08.attach(8);
  myservo09.attach(9);
  myservo10.attach(10);
  myservo11.attach(11);
  counter = 1;
  }

//  pos1 = map(pos1, 0, 180, 550, 2350);
//  pos2 = map(pos2, 0, 180, 550, 2350);
//  pos3 = map(pos3, 0, 180, 550, 2350);
//  pos4 = map(pos4, 0, 180, 550, 2350);
//  pos5 = map(pos5, 0, 180, 550, 2350);
//  pos6 = map(pos6, 0, 180, 550, 2350);

//  myservo1.writeMicroseconds(pos1);
//  myservo2.writeMicroseconds(pos2);
//  myservo3.writeMicroseconds(pos3);
//  myservo4.writeMicroseconds(pos4);
//  myservo5.writeMicroseconds(pos5);
//  myservo6.writeMicroseconds(pos6);

  myservo00.write(pos00);
  myservo01.write(180-pos00);
  myservo02.write(pos01);
  myservo03.write(180-pos01);
  myservo04.write(pos02);
  myservo05.write(180-pos02);
  myservo06.write(pos03);
  myservo07.write(180-pos03);
  myservo08.write(pos04);
  myservo09.write(180-pos04);
  myservo10.write(pos05);
  myservo11.write(180-pos05);

  }
  else if(bending==0){
    if(counter==1){
    myservo00.detach();
    myservo01.detach();
    myservo02.detach();
    myservo03.detach();
    myservo04.detach();
    myservo05.detach();
    myservo06.detach();
    myservo07.detach();
    myservo08.detach();
    myservo09.detach();
    myservo10.detach();
    myservo11.detach();
    counter = 0;
  }
  }
  input += "&";
  
  input += String(Aread00);
  input += ",";
  input += String(Aread01);
  input += ",";
  input += String(Aread02);
  input += ",";
  input += String(Aread03);
  input += ",";
  input += String(Aread04);
  input += ",";
  input += String(Aread05);
  input += ",";

   /* Display the quat data */
  input += String(quat.w(), 4);
  input += ",";
  input += String(quat.y(), 4);
  input += ",";
  input += String(quat.x(), 4);
  input += ",";
  input += String(quat.z(), 4);
  input += ",";
  
  /* Display the individual values */
  input += String(system, DEC);
  input += ","; 
  input += String(gyro, DEC);
  input += ",";  
  input += String(accel, DEC);
  input += ",";  
  input += String(mag, DEC);
  input += ",";
  input += String(bending);
  input += ",";
  input += String(pos00);
  input += ",";
  input += String(pos01);
  input += ",";
  input += String(pos02);
  input += ",";
  input += String(pos03);
  input += ",";
  input += String(pos04);
  input += ",";
  input += String(pos05);

  input += "$";
  
  Serial.println(input);
  delay(25);
  Serial.flush();
  
  }

  
void recvWithStartEndMarkers() {
    static boolean recvInProgress = false;
    static byte ndx = 0;
    char startMarker = '<';
    char endMarker = '>';
    char rc;

    while (Serial.available() > 0 && newData == false) {
        rc = Serial.read();

        if (recvInProgress == true) {
            if (rc != endMarker) {
                receivedChars[ndx] = rc;
                ndx++;
                if (ndx >= numChars) {
                    ndx = numChars - 1;
                }
            }
            else {
                receivedChars[ndx] = '\0'; // terminate the string
                recvInProgress = false;
                ndx = 0;
                newData = true;
            }
        }

        else if (rc == startMarker) {
            recvInProgress = true;
        }
    }
}


void parseData() {      // split the data into its parts

    char * strtokIndx; // this is used by strtok() as an index
 
    strtokIndx = strtok(tempChars, ","); // this continues where the previous call left off
    bending = atoi(strtokIndx);     // convert this part to an integer

    strtokIndx = strtok(NULL, ",");
    pos00 = atof(strtokIndx);     // convert this part to a float

    strtokIndx = strtok(NULL, ",");
    pos01 = atof(strtokIndx);     // convert this part to a float
    
    strtokIndx = strtok(NULL, ",");
    pos02 = atof(strtokIndx);     // convert this part to a float

    strtokIndx = strtok(NULL, ",");
    pos03 = atof(strtokIndx);     // convert this part to a float

    strtokIndx = strtok(NULL, ",");
    pos04 = atof(strtokIndx);     // convert this part to a float

    strtokIndx = strtok(NULL, ",");
    pos05 = atof(strtokIndx);     // convert this part to a float
}


