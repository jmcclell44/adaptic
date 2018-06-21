#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BNO055.h>
#include <utility/imumaths.h>
#include <EEPROM.h>
#include <Servo.h>

/* Set the delay between fresh samples */
#define BNO055_SAMPLERATE_DELAY_MS (100)

Adafruit_BNO055 bno = Adafruit_BNO055(55);

Servo myservo1;
Servo myservo2;
Servo myservo3;
Servo myservo4;
Servo myservo5;
Servo myservo6;

float pos1 = 90;
float pos2 = 90;
float pos3 = 90;
float pos4 = 90;
float pos5 = 90;
float pos6 = 90;

float Aread1;
float Aread2;
float Aread3;
float Aread4;
float Aread5;
float Aread6;

float bending[6];

const byte numChars = 64;
char receivedChars[numChars];
char tempChars[numChars];        // temporary array for use when parsing

      // variables to hold the parsed data
char messageFromPC[numChars] = {0};

boolean newData = false;

void setup(void)
{
  //bno.begin(bno.OPERATION_MODE_IMUPLUS);
 
  Serial.begin(115200);
  //Serial.println("Orientation Sensor Test"); Serial.println("");

  /* Initialise the sensor */
  if(!bno.begin())
  {
    /* There was a problem detecting the BNO055 ... check your connections */
    Serial.print("Ooops, no BNO055 detected ... Check your wiring or I2C ADDR!");
    while(1);
  }

for (int i=0; i<6; i++){
  bending[i] = 0;
}
  bno.setExtCrystalUse(true);
}


void loop() {
//  myservo1.attach(8);
//  myservo2.attach(9);
  bno.setMode(bno.OPERATION_MODE_IMUPLUS);
  // put your main code here, to run repeatedly:
  String input = "";
  
  imu::Quaternion quat = bno.getQuat();
  
  uint8_t system, gyro, accel, mag;
  system = gyro = accel = mag = 0;
  bno.getCalibration(&system, &gyro, &accel, &mag);

  Aread1 = analogRead(A0);
  Aread2 = analogRead(A1);
  Aread3 = analogRead(A2);
  Aread4 = analogRead(A3);
  Aread5 = analogRead(A4);
  Aread6 = analogRead(A5);
  
    recvWithStartEndMarkers();
    if (newData == true) {
        strcpy(tempChars, receivedChars);
            // this temporary copy is necessary to protect the original data
            //   because strtok() used in parseData() replaces the commas with \0
        parseData();
        newData = false;
    }


  if (bending[0]==1){
  myservo1.attach(20);
  myservo1.write(pos1);}
  else if(bending[0]==0){
  myservo1.detach();} 

  if (bending[1]==1){
  myservo2.attach(21);
  myservo2.write(pos2);}
  else if(bending[1]==0){
  myservo2.detach();} 

  if (bending[2]==1){
  myservo3.attach(22);
  myservo3.write(pos3);}
  else if(bending[2]==0){
  myservo3.detach();} 

  if (bending[3]==1){
  myservo4.attach(23);
  myservo4.write(pos4);}
  else if(bending[3]==0){
  myservo4.detach();} 

  if (bending[4]==1){
  myservo5.attach(24);
  myservo5.write(pos5);}
  else if(bending[4]==0){
  myservo5.detach();} 

  if (bending[5]==1){
  myservo6.attach(25);
  myservo6.write(pos6);}
  else if(bending[5]==0){
  myservo6.detach();} 


  input += "&";
  
  input += String(Aread1);
  input += ",";
  input += String(Aread2);
  input += ",";
  input += String(Aread3);
  input += ",";
  input += String(Aread4);
  input += ",";
  input += String(Aread5);
  input += ",";
  input += String(Aread6);
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
  input += String(bending[1]);
  input += ",";
  input += String(pos1);
  input += ",";
  input += String(pos2);
  input += ",";
  input += String(pos3);
  input += ",";
  input += String(pos4);
  input += ",";
  input += String(pos5);
  input += ",";
  input += String(pos6);

  input += "$";
  
  Serial.println(input);
  Serial.flush();
//  delay(5);
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
    bending[0] = atoi(strtokIndx);     // convert this part to an integer
    bending[1] = atoi(strtokIndx);     // convert this part to an integer    
    bending[2] = atoi(strtokIndx);     // convert this part to an integer
    bending[3] = atoi(strtokIndx);     // convert this part to an integer
    bending[4] = atoi(strtokIndx);     // convert this part to an integer
    bending[5] = atoi(strtokIndx);     // convert this part to an integer

    strtokIndx = strtok(NULL, ",");
    pos1 = atof(strtokIndx);     // convert this part to a float

    strtokIndx = strtok(NULL, ",");
    pos2 = atof(strtokIndx);     // convert this part to a float
    
    strtokIndx = strtok(NULL, ",");
    pos3 = atof(strtokIndx);     // convert this part to a float

    strtokIndx = strtok(NULL, ",");
    pos4 = atof(strtokIndx);     // convert this part to a float

    strtokIndx = strtok(NULL, ",");
    pos5 = atof(strtokIndx);     // convert this part to a float

    strtokIndx = strtok(NULL, ",");
    pos6 = atof(strtokIndx);     // convert this part to a float
}


