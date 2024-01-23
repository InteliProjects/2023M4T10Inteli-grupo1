#include <MFRC522.h>
#include <SPI.h>
#include <WiFi.h>

constexpr uint8_t RST_PIN = 14;        // Configura o pino RST
constexpr uint8_t SS_PIN = 5;          // Configura o pino SS (SDA)
constexpr uint8_t GREEN_LED_PIN = 27;  // Pino do LED verde
constexpr uint8_t RED_LED_PIN = 26;    // Pino do LED vermelho
constexpr uint8_t YELLOW_LED_PIN = 25; // Pino do LED amarelo

// Classe para gerenciar operações de LED
class LED {
public:
  explicit LED(uint8_t pin) : pin(pin) {
    pinMode(pin, OUTPUT);
    off();
  }
  void on() { digitalWrite(pin, HIGH); }
  void off() { digitalWrite(pin, LOW); }

private:
  const uint8_t pin;
};
// Classe para encapsular a funcionalidade do sistema RFID
class RFIDSystem {
public:
  RFIDSystem(const char *ssid, const char *password)
      : rfid(SS_PIN, RST_PIN), ssid(ssid), password(password),
        greenLed(GREEN_LED_PIN), redLed(RED_LED_PIN),
        yellowLed(YELLOW_LED_PIN) {
    SPI.begin();
    rfid.PCD_Init();
    WiFi.begin(this->ssid, this->password);
  }
  void setup() {
    Serial.begin(115200);
    waitForWifiConnection();
  }
  void loop() {
    checkWifiConnection();
    checkRFID();
  }

private:
  MFRC522 rfid;
  const char *ssid;
  const char *password;
  LED greenLed;
  LED redLed;
  LED yellowLed;
  void waitForWifiConnection() {
    Serial.print("Conectando ao Wi-Fi...");
    while (WiFi.status() != WL_CONNECTED) {
      delay(500);
      Serial.print(".");
    }
    Serial.println("\nConectado ao Wi-Fi!");
    yellowLed.on();
  }
  void checkWifiConnection() {
    if (WiFi.status() == WL_CONNECTED) {
      yellowLed.on();
    } else {
      yellowLed.off();
    }
  }
  void checkRFID() {
    if (rfid.PICC_IsNewCardPresent() && rfid.PICC_ReadCardSerial()) {
      redLed.off();
      greenLed.on();
      printRFID();
      delay(1000);
      greenLed.off();
      rfid.PICC_HaltA();
    } else {
      redLed.on();
    }
  }
  void printRFID() {
    Serial.print("Tag RFID lida: ");
    for (byte i = 0; i < rfid.uid.size; i++) {
      Serial.print(rfid.uid.uidByte[i] < 0x10 ? " 0" : " ");
      Serial.print(rfid.uid.uidByte[i], HEX);
    }
    Serial.println();
  }
};
// Instância do sistema RFID
RFIDSystem rfidSystem("Inteli-COLLEGE", "QazWsx@123");
void setup() { rfidSystem.setup(); }
void loop() { rfidSystem.loop(); }
