#include <WiFi.h>
#include <PubSubClient.h>
#include <WiFiClientSecure.h>
#include <SPI.h>
#include <MFRC522.h>

// Constantes
const char* ssid = "Inteli-COLLEGE";
const char* password = "QazWsx@123";

const char* mqtt_server = "8790e88d997a4447ab7be359300b2097.s2.eu.hivemq.cloud";
const char* mqtt_username = "IoTvos";
const char* mqtt_password = "Inteli23";
const int mqtt_port = 8883;

constexpr uint8_t RST_PIN = 14;
constexpr uint8_t SS_PIN = 5;

const int redPin = 33;
const int greenPin = 27;
const int bluePin = 25;
const int buttonPin = 4;
const int modeButtonPin = 32;
const int buzzerPin = 22;

enum Mode { NORMAL, DALTONICO };

/* Certificado do HiveMQ */;
static const char *root_ca PROGMEM = R"EOF(
-----BEGIN CERTIFICATE-----
MIIFazCCA1OgAwIBAgIRAIIQz7DSQONZRGPgu2OCiwAwDQYJKoZIhvcNAQELBQAw
TzELMAkGA1UEBhMCVVMxKTAnBgNVBAoTIEludGVybmV0IFNlY3VyaXR5IFJlc2Vh
cmNoIEdyb3VwMRUwEwYDVQQDEwxJU1JHIFJvb3QgWDEwHhcNMTUwNjA0MTEwNDM4
WhcNMzUwNjA0MTEwNDM4WjBPMQswCQYDVQQGEwJVUzEpMCcGA1UEChMgSW50ZXJu
ZXQgU2VjdXJpdHkgUmVzZWFyY2ggR3JvdXAxFTATBgNVBAMTDElTUkcgUm9vdCBY
MTCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAK3oJHP0FDfzm54rVygc
h77ct984kIxuPOZXoHj3dcKi/vVqbvYATyjb3miGbESTtrFj/RQSa78f0uoxmyF+
0TM8ukj13Xnfs7j/EvEhmkvBioZxaUpmZmyPfjxwv60pIgbz5MDmgK7iS4+3mX6U
A5/TR5d8mUgjU+g4rk8Kb4Mu0UlXjIB0ttov0DiNewNwIRt18jA8+o+u3dpjq+sW
T8KOEUt+zwvo/7V3LvSye0rgTBIlDHCNAymg4VMk7BPZ7hm/ELNKjD+Jo2FR3qyH
B5T0Y3HsLuJvW5iB4YlcNHlsdu87kGJ55tukmi8mxdAQ4Q7e2RCOFvu396j3x+UC
B5iPNgiV5+I3lg02dZ77DnKxHZu8A/lJBdiB3QW0KtZB6awBdpUKD9jf1b0SHzUv
KBds0pjBqAlkd25HN7rOrFleaJ1/ctaJxQZBKT5ZPt0m9STJEadao0xAH0ahmbWn
OlFuhjuefXKnEgV4We0+UXgVCwOPjdAvBbI+e0ocS3MFEvzG6uBQE3xDk3SzynTn
jh8BCNAw1FtxNrQHusEwMFxIt4I7mKZ9YIqioymCzLq9gwQbooMDQaHWBfEbwrbw
qHyGO0aoSCqI3Haadr8faqU9GY/rOPNk3sgrDQoo//fb4hVC1CLQJ13hef4Y53CI
rU7m2Ys6xt0nUW7/vGT1M0NPAgMBAAGjQjBAMA4GA1UdDwEB/wQEAwIBBjAPBgNV
HRMBAf8EBTADAQH/MB0GA1UdDgQWBBR5tFnme7bl5AFzgAiIyBpY9umbbjANBgkq
hkiG9w0BAQsFAAOCAgEAVR9YqbyyqFDQDLHYGmkgJykIrGF1XIpu+ILlaS/V9lZL
ubhzEFnTIZd+50xx+7LSYK05qAvqFyFWhfFQDlnrzuBZ6brJFe+GnY+EgPbk6ZGQ
3BebYhtF8GaV0nxvwuo77x/Py9auJ/GpsMiu/X1+mvoiBOv/2X/qkSsisRcOj/KK
NFtY2PwByVS5uCbMiogziUwthDyC3+6WVwW6LLv3xLfHTjuCvjHIInNzktHCgKQ5
ORAzI4JMPJ+GslWYHb4phowim57iaztXOoJwTdwJx4nLCgdNbOhdjsnvzqvHu7Ur
TkXWStAmzOVyyghqpZXjFaH3pO3JLF+l+/+sKAIuvtd7u+Nxe5AW0wdeRlN8NwdC
jNPElpzVmbUq4JUagEiuTDkHzsxHpFKVK7q4+63SM1N95R1NbdWhscdCb+ZAJzVc
oyi3B43njTOQ5yOf+1CceWxG1bQVs5ZufpsMljq4Ui0/1lvh+wjChP4kqKOJ2qxq
4RgqsahDYVvTH9w7jXbyLeiNdd8XM2w9U/t7y0Ff/9yi0GE44Za4rF2LN9d11TPA
mRGunUHBcnWEvgJBQl9nJEiU0Zsnvgc/ubhPgXRR4Xq37Z0j4r7g1SgEEzwxA57d
emyPxgcYxn/eR44/KJ4EBs+lVDR3veyJm+kXQ99b21/+jh5Xos1AnX5iItreGCc=
-----END CERTIFICATE-----
)EOF";

// WifiManager
class WifiManager {
private:
    const char* ssid;
    const char* password;

public:
    WifiManager(const char* ssid, const char* password) : ssid(ssid), password(password) {}

    void connect() {
        Serial.print("Conectando a ");
        Serial.println(ssid);
        WiFi.mode(WIFI_STA);
        WiFi.begin(ssid, password);
        while (WiFi.status() != WL_CONNECTED) {
            delay(500);
            Serial.print(".");
        }
        Serial.println("\nWiFi conectado");
        Serial.println("Endereço IP: ");
        Serial.println(WiFi.localIP());
    }
};

// MQTTManager
class MQTTManager {
private:
    PubSubClient& client;

public:
    MQTTManager(PubSubClient& client, const char* server, const char* username, const char* password, int port) 
        : client(client) {
        client.setServer(server, port);
        client.setCallback(callback);
    }

    void connect() {
        while (!client.connected()) {
            Serial.print("Tentando conexão MQTT... ");
            if (client.connect("ESP32Client", mqtt_username, mqtt_password)) {
                Serial.println("conectado!");
                client.subscribe("testTopic");
            } else {
                Serial.print("falha, rc=");
                Serial.print(client.state());
                Serial.println(" tentando novamente em 5 segundos");
                delay(5000);
            }
        }
    }

    void loop() {
        client.loop();
    }

    bool publish(const char* topic, const char* payload) {
        return client.publish(topic, payload);
    }

    static void callback(char* topic, byte* payload, unsigned int length) {
        Serial.print("Message arrived [");
        Serial.print(topic);
        Serial.print("] ");
        for (int i = 0; i < length; i++) {
            Serial.print((char)payload[i]);
        }
        Serial.println();
    }
};

// RFIDReader
class RFIDReader {
private:
    MFRC522 rfid;

public:
    RFIDReader(uint8_t ssPin, uint8_t rstPin) : rfid(ssPin, rstPin) {
        SPI.begin(); 
        rfid.PCD_Init(); 
    }

    String readCard() {
        if (rfid.PICC_IsNewCardPresent() && rfid.PICC_ReadCardSerial()) {
            String content = "";
            for (byte i = 0; i < rfid.uid.size; i++) {
                content.concat(String(rfid.uid.uidByte[i], HEX));
            }
            content.toUpperCase();
            rfid.PICC_HaltA();
            return content;
        }
        return "";
    }
};

// LEDController
class LEDController {
private:
    int redPin;
    int greenPin;
    int bluePin;
    Mode currentMode;

public:
    LEDController(int redPin, int greenPin, int bluePin) 
        : redPin(redPin), greenPin(greenPin), bluePin(bluePin), currentMode(NORMAL) {
        pinMode(redPin, OUTPUT);
        pinMode(greenPin, OUTPUT);
        pinMode(bluePin, OUTPUT);
    }

    void updateMode(Mode mode) {
        currentMode = mode;
        updateLED(false); // Atualiza a cor do LED conforme o modo
    }

    void updateLED(bool success) {
        if (currentMode == NORMAL) {
            if (success) {
                setColor(0, 1, 0); // Verde
            } else {
                setColor(1, 0, 0); // Vermelho
            }
        } else { // Modo daltônico
            if (success) {
                setColor(0, 0, 1); // Azul
            } else {
                setColor(1, 1, 0); // Laranja
            }
        }
    }

    Mode getCurrentMode() {
        return currentMode;
    }

private:
    void setColor(float red, float green, float blue) {
        digitalWrite(redPin, !red);
        digitalWrite(greenPin, !green);
        digitalWrite(bluePin, !blue);
    }
};

// ButtonController
class ButtonController {
private:
    int buttonPin;
    int modeButtonPin;

public:
    ButtonController(int buttonPin, int modeButtonPin) 
        : buttonPin(buttonPin), modeButtonPin(modeButtonPin) {
        pinMode(buttonPin, INPUT_PULLUP);
        pinMode(modeButtonPin, INPUT_PULLUP);
    }

    Mode checkModeChange(Mode currentMode) {
        if (digitalRead(buttonPin) == LOW) {
            delay(200); // Debouncing
            return NORMAL;
        }
        if (digitalRead(modeButtonPin) == LOW) {
            delay(200); // Debouncing
            return currentMode == NORMAL ? DALTONICO : NORMAL;
        }
        return currentMode;
    }
};

// Função para emitir um som com o buzzer
void beepBuzzer() {
    digitalWrite(buzzerPin, HIGH);
    delay(1000); // Duração do som
    digitalWrite(buzzerPin, LOW);
}

// Instâncias globais
WiFiClientSecure espClient;
PubSubClient client(espClient);
WifiManager wifiManager(ssid, password);
MQTTManager mqttManager(client, mqtt_server, mqtt_username, mqtt_password, mqtt_port);
RFIDReader rfidReader(SS_PIN, RST_PIN);
LEDController ledController(redPin, greenPin, bluePin);
ButtonController buttonController(buttonPin, modeButtonPin);

void setup() {
    Serial.begin(115200);
    wifiManager.connect();
    espClient.setCACert(root_ca);
    mqttManager.connect();
}

void loop() {
    if (!client.connected()) {
        mqttManager.connect();
    }
    mqttManager.loop(); 

    String rfidContent = rfidReader.readCard();
    if (rfidContent != "") {
        bool success = mqttManager.publish("rfidTopic", rfidContent.c_str());
        ledController.updateLED(success);

        if (success) {
            beepBuzzer();
        }
    }

    Mode newMode = buttonController.checkModeChange(ledController.getCurrentMode());
    if (newMode != ledController.getCurrentMode()) {
        ledController.updateMode(newMode);
    }

    delay(1000); 
}

