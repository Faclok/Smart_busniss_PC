/* датчик линии заполняет массив, если есть динамика - в работу
 *  если в работе и нет динамики - отсечка. дельта т на сервер: 
 *       вызываем функцию toSend, передаем таймер и false
 *  PhotoResistor - A0
 *  BME280: SCL - D1; SDA - D2
 *  Beeper - D3
 *  Led - D5
 *  Button- D6
 *  если температура больше 30, вызываем функцию toSend, передаем таймер и true
 *  пока температура не опустится будет гореть светодиод D5 и бипер D6
 *  (дальше код не выполняется, пока не опустится)
*/
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>
#include <Wire.h>
#include <Adafruit_BME280.h>
#include <GyverOLED.h>

#define out A0
#define LEN 20
#define LED 14
#define BEEP 0
#define BUTTON 12
#define SIGNAL 500

GyverOLED<SSH1106_128x64> oled;
int mas[LEN];
unsigned int time1, time2;
float timer;
byte minr, maxr;
boolean inOrder = false;
float inService = 0;

// параметры сети
const char* ssid     = "RT-WIFI-25DA";
const char* password = "fcEH5nFg";

// адрес пхп скрипта
const char* serverName = "http://192.168.1.243/test_laser.php";

// апикей
String apiKeyValue = "tPmAT5Ab3j7F9";

String sensorName = "Laser_1";
String sensorLocation = "Lab";

Adafruit_BME280 bme;  // I2C

void setup() {
  Serial.begin(115200); 
  pinMode(BUTTON, INPUT_PULLUP); 
  oled.init(); 
  
  WiFi.begin(ssid, password);
  Serial.println("Connecting");
  while(WiFi.status() != WL_CONNECTED) { 
    delay(500);
    Serial.print(".");
    oled.clear();
    oled.setScale(3);
    oled.print("  НЕТ  ");
    oled.setCursor(0, 4);
    oled.print("  СЕТИ!  ");
    oled.update();
  }
  Serial.println("");
  Serial.print("Connected to WiFi network with IP Address: ");
  Serial.println(WiFi.localIP());
  
  pinMode(out, INPUT);    
  pinMode(LED, OUTPUT);  
  pinMode(BEEP, OUTPUT);
  
  bool status = bme.begin(0x76);
  if (!status) {
    Serial.println("Could not find a valid BME280 sensor, check wiring or change I2C address!");
    oled.clear();
    oled.setScale(3);
    oled.print("  НЕТ  ");
    oled.setCursor(0, 4);
    oled.print("ДАТЧИКА!");
    oled.update();
    while (1);
  }
}

void display(){
  oled.clear();
  oled.setScale(2);
  oled.home();
  oled.print("СЕТЬ: ");
  if(WiFi.status()== WL_CONNECTED){ oled.print("ДА");}
  else{oled.print("НЕТ");}
  oled.setCursor(0, 3);
  oled.print("ТЕМП:");
  oled.print(bme.readTemperature());  
  oled.setCursor(0, 6);
  //oled.print("ВРАБ:");
  //oled.print(inService);
  oled.print("СВЕТ:");
  oled.print(analogRead(out));
  oled.update();
}

void toSend(float inOrder, bool alarm){
  if(WiFi.status()== WL_CONNECTED){
        WiFiClient client;
        HTTPClient http;
        
        // Your Domain name with URL path or IP address with path
        http.begin(client, serverName);
        
        // Specify content-type header
        http.addHeader("Content-Type", "application/x-www-form-urlencoded");
        
        // Prepare your HTTP POST request data
        //String httpRequestData = "api_key=tPmAT5Ab3j7F9&sensor=BME280&value1=24.75";
        String httpRequestData = "api_key=" + apiKeyValue + "&sensor=" + sensorName
                              + "&location=" + sensorLocation + "&inservice=" + inOrder/60 
                              + "&alarm=" + alarm + "";
        Serial.print("httpRequestData: ");
        Serial.println(httpRequestData);
        
        // You can comment the httpRequestData variable above
        // then, use the httpRequestData variable below (for testing purposes without the BME280 sensor)
        
    
        // Send HTTP POST request
        int httpResponseCode = http.POST(httpRequestData);
         
        if (httpResponseCode>0) {
          Serial.print("HTTP Response code: ");
          Serial.println(httpResponseCode);
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

void loop() { 
  if (digitalRead(BUTTON) == 0){ inService = 0; }
  display();
  if (bme.readTemperature() > 30) {
    digitalWrite(LED, HIGH);
    toSend(timer, true); 
    while (bme.readTemperature() > 30){
      digitalWrite(BEEP, HIGH);
      delay(100);
    }
  }
  else { 
    digitalWrite(LED, LOW); 
    digitalWrite(BEEP, LOW);
    }
  
  
  if (analogRead(out) < SIGNAL && inOrder == false){
    inOrder = true;
    time1 = millis();
    time2 = millis();
    }
  else if(analogRead(out) < SIGNAL && inOrder == true){
    time2 = millis();
    }
  else if(analogRead(out) < SIGNAL && inOrder == true && (millis() - time2 > 20000)) 
  {
    timer = (time2 - time1) / 1000;    
    inOrder = false;
    if (timer > 10){
      toSend(timer, false);
      inService += timer/60;
    }
    Serial.print(" timer: ");  
    Serial.print(timer);
    Serial.print(" min "); 
    Serial.print(minr);
    Serial.print(" MAX "); 
    Serial.println(maxr);
    timer = 0;    
  }
  Serial.println(WiFi.status());
  delay(10);
  
}
