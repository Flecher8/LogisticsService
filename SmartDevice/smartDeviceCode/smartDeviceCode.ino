#include <WiFi.h>
#include <HTTPClient.h>
#include <HardwareSerial.h>
#include <TinyGPS++.h>


// WIFI settings
const char* ssid = "****";
const char* password = "****";



//Domain name 
String serverName = "https://192.168.1.197:45455/";
String writeCoordinatesAPI = "api/SmartDeviceCommunication/WriteCoordinates";
String sensorActivationAPI = "api/SmartDeviceCommunication/ActivateSensor";

String connectionCheckAPI = "api/SmartDeviceCommunication/TestC";



// Parameters to send Smart device GPS location
const int smartDeviceId = 4;
double latitude = 0;
double longitute = 0;
// Parameters to send Sensor activation
const int sensorId = 2;
bool sensorDataSend; 

// Variables needed to take GPS location
int satellites1 = 0;
int accuracy = 6;
bool coordinatesAreCorrect;



// last time used GPS
unsigned long lastTime = 0;
// last time used Sensor
unsigned long lastTimeUsedSensor = 0; 



// Timer set to 1 minute (60.000)
// GPS
unsigned long timerDelay = 60000;
// Sensor
unsigned long timerDelaySensor = 6000;



// GPS Settings
HardwareSerial neogps(1);
// GPS Object
TinyGPSPlus gps;
// GPS Connections to board
#define RXD2 2
#define TXD2 15

// Sensor settings
// Триг - 0, Эхо - 4
const int TRIGGER_PIN  = 0;
const int ECHO_PIN = 4;
int duration;
int distance1;


// Count how many times coordinates not correct 
const int maxNumberOfActivations = 100;
// How many times GPS returns incorrect coordinates ( max = maxNumberOfActivations )
int gpsDontWork = 0;

void countTimesGpsNotWork()
{
  if(gpsDontWork >= maxNumberOfActivations)
  {
    return;
  }
  gpsDontWork++;
}


void sendDataToServer(String path)
{
    //Send an HTTP POST request every 10 minutes
    //Check WiFi connection status
    if(WiFi.status()== WL_CONNECTED){
      HTTPClient http;
      
      String serverPath = path;
      
      // Your Domain name with URL path or IP address with path
      http.begin(serverPath.c_str());

      // If you need Node-RED/server authentication, insert user and password below
      //http.setAuthorization("REPLACE_WITH_SERVER_USERNAME", "REPLACE_WITH_SERVER_PASSWORD");
      
      // Send HTTP GET request
      int httpResponseCode = http.GET();
      
      if (httpResponseCode>0) {
        Serial.print("HTTP Response code: ");
        Serial.println(httpResponseCode);
        String payload = http.getString();
        Serial.println(payload);
      }
      else {
        Serial.print("Error code: ");
        Serial.println(httpResponseCode);
      }
      // Free resources
      http.end();
    }
    else {
      Serial.println("WiFi Disconnected");
    }
}


// Send smart device location to server
void sendDataGPSLocation()
{
    String serverPath = serverName + writeCoordinatesAPI + "?smartDeviceId=" + smartDeviceId +"&latitude=" + String(latitude, accuracy) + "&longitute=" + String(longitute, accuracy);
    sendDataToServer(serverPath);
    lastTime = millis();
}


// Test data
void setTestData()
{
  latitude = 49.99112333;
  longitute = 36.27771117;
}



// Send smart device coordinates to server
void getGPSLocation()
{
  if (neogps.available() > 0)
  {
      if (gps.encode(neogps.read()))
      {
              
          satellites1 = gps.satellites.value();
          
          if(gps.satellites.value() > 0 && gps.location.isValid())
          {
              Serial.println("[GPS]:");
              Serial.println("satellites: " + String(satellites1));
              
              if(gps.location.lat() != 0 && gps.location.lng() != 0)
              {
                latitude  = gps.location.lat();
                longitute = gps.location.lng();
                Serial.println("C: ");
                Serial.println(gps.location.lat(), accuracy);
                Serial.println(gps.location.lng(), accuracy);
                coordinatesAreCorrect = true;
              }
          }
      }
  }
  // If the coordinates were not correct from the GPS sensor, then increase the counter
  if(coordinatesAreCorrect == false)
  {
    countTimesGpsNotWork(); 
  }
  
  // Return test data if GPS doesnt work
  if((millis() - lastTime) > timerDelay && gpsDontWork >= maxNumberOfActivations && coordinatesAreCorrect == false)
  {
    //Serial.println("[GPS]: Set test data");

    setTestData();
    coordinatesAreCorrect = true;
    // reset 
    gpsDontWork = 0;
  }
  // If time is valid and coordinates valid, send gps location to server
  if ((millis() - lastTime) > timerDelay && coordinatesAreCorrect == true)
  {
      Serial.println("[GPS]: Sending data");
      sendDataGPSLocation();
      coordinatesAreCorrect = false;
  }
  
}





// How many times Sensor correct
int sensorActiveCounter = 0;

void increaseSensorActiveTimes()
{
  if(sensorActiveCounter >= maxNumberOfActivations)
  {
    return;
  }
  sensorActiveCounter++;
}

void decreaseSensorActiveTimes()
{
  if(sensorActiveCounter <= 0)
  {
    return;
  }
  sensorActiveCounter--;
}


// Send to server that sensor activated
void sendDataSensorActivated()
{
    String serverPath = serverName + sensorActivationAPI + "?sensorId=" + sensorId +"&latitude=" + String(latitude, accuracy) + "&longitute=" + String(longitute, accuracy);
    // Return test data if GPS doesnt work
  if(coordinatesAreCorrect == false)
  {
    //Serial.println("[Sensor]: Set test data");

    setTestData();
    coordinatesAreCorrect = true;
    // reset 
    gpsDontWork = 0;
  }
  Serial.println("[Sensor]: Sending data...");
  sendDataToServer(serverPath);
}



// Check is sensor active
bool isSensorActive()
{
  const int numberOfTimesNeededToCheckSensor = 200;
  int n = 0;
  const int distanceWhenSensorIsActive = 5;
  while(n < numberOfTimesNeededToCheckSensor)
  {
    digitalWrite(TRIGGER_PIN, LOW);
    delayMicroseconds(2);
    digitalWrite(TRIGGER_PIN, HIGH);
    delayMicroseconds(10);
    digitalWrite(TRIGGER_PIN, LOW);
    duration = pulseIn(ECHO_PIN, HIGH);
    
    distance1 = duration * 0.034 / 2;
    
    //Serial.println(distance1);
    if (distance1 <= distanceWhenSensorIsActive)
    {
      increaseSensorActiveTimes();
    }
    else
    {
      decreaseSensorActiveTimes();
    }

    n++;
  }
  
  n = 0;
  if(sensorActiveCounter >= maxNumberOfActivations)
  {
    return true;
  }
  return false;
}



// Sensor work
void sensorWork()
{
  if((millis() - lastTimeUsedSensor) > timerDelaySensor)
  {
    if ( isSensorActive() )
    {
      if(sensorDataSend == false)
      {
        Serial.println("[Sensor]: Sensor active data send");
        sendDataSensorActivated();
        sensorDataSend = true;
      }
    }
    else
    {
      Serial.println("[Sensor]: Sensor disactive");
      sensorDataSend = false;
    }
    lastTimeUsedSensor = millis();
  }
}

void setupServerConnection()
{
  while(true)
  {
    //Check WiFi connection status
    if(WiFi.status()== WL_CONNECTED){
      HTTPClient http;
      
      String serverPath = serverName + connectionCheckAPI;
      
      // URL path
      http.begin(serverPath.c_str());
      
      // Send HTTP GET request
      int httpResponseCode = http.GET();
      
      if (httpResponseCode>0) {
        Serial.println("[ConnectionSetup]: Connection with server successfully established");
        // Free resources
        http.end();
        return;
      }
      else {
        Serial.print("[ConnectionSetup]: Error connection ");
        Serial.println(httpResponseCode);
      }
      // Free resources
      http.end();
    }
    else {
      Serial.println("[ConnectionSetup]: WiFi Disconnected");
    }
  }
}




void setup() {
  // Set up console
  Serial.begin(115200);
  // Set up GPS
  neogps.begin(9600, SERIAL_8N1, RXD2, TXD2);
  
  // Set up WIFI
  WiFi.begin(ssid, password);
  Serial.println("[WiFi]: Connecting");
  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("[WiFi]: Connected to WiFi network with IP Address: ");
  Serial.println(WiFi.localIP());
  
  // Set up server connection
  setupServerConnection();
  
  // Startup values
  lastTime = millis();
  coordinatesAreCorrect = false;

  // Set up SENSOR
  pinMode(TRIGGER_PIN, OUTPUT);
  pinMode(ECHO_PIN, INPUT);
  sensorDataSend = false;
  lastTimeUsedSensor = millis();
}

void loop() {
  // Get GPS locations, if correct send to server
  getGPSLocation();

  // Get sensor data
  sensorWork();
}

