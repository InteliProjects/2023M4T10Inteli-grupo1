#include <WiFi.h>
#include <PubSubClient.h>
#include <WiFiClientSecure.h>
#include <SPI.h>
#include <MFRC522.h>

class WiFiManager {
public:
    WiFiManager(const char* ssid, const char* password) 
        : ssid(ssid), password(password) {}

    void connect() {
        Serial.print("Conectando a ");
        Serial.println(ssid);

        WiFi.mode(WIFI_STA);
        WiFi.begin(ssid, password);

        while (WiFi.status() != WL_CONNECTED) {
            delay(500);
            Serial.print(".");
        }

        Serial.println("");
        Serial.println("WiFi conectado");
        Serial.println("Endereço IP: ");
        Serial.println(WiFi.localIP());
    }

private:
    const char* ssid;
    const char* password;
};

class MqttClient {
public:
    MqttClient(const char* server, const char* username, const char* password, int port, WiFiClientSecure& espClient)
        : server(server), username(username), password(password), port(port), client(espClient) {}

    void setup(const char* root_ca) {
        espClient.setCACert(root_ca);
        client.setServer(server, port);
        client.setCallback(MqttClient::callback);
    }

    void connect() {
        while (!client.connected()) {
            Serial.print("Tentando conexão MQTT... ");
            if (client.connect("ESP32Client", username, password)) {
                Serial.println("conectado!");
                client.subscribe("testTopic"); // Subscrever a um tópico específico
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

    static void callback(char* topic, byte* message, unsigned int length) {
        Serial.print("Mensagem recebida [");
        Serial.print(topic);
        Serial.print("] ");
        for (int i = 0; i < length; i++) {
            Serial.print((char)message[i]);
        }
        Serial.println();
    }

private:
    const char* server;
    const char* username;
    const char* password;
    int port;
    PubSubClient client;
    WiFiClientSecure& espClient;
};

class RFIDReader {
public:
    RFIDReader(uint8_t ssPin, uint8_t rstPin) : rfid(ssPin, rstPin) {}

    void setup() {
        SPI.begin();
        rfid.PCD_Init();
    }

    void read(PubSubClient& client) {
        if (rfid.PICC_IsNewCardPresent() && rfid.PICC_ReadCardSerial()) {
            Serial.print("Tag RFID lida: ");
            String content = "";
            for (byte i = 0; i < rfid.uid.size; i++) {
                content.concat(String(rfid.uid.uidByte[i] < 0x10 ? "0" : ""));
                content.concat(String(rfid.uid.uidByte[i], HEX));
            }
            content.toUpperCase();
            if (client.publish("rfidTopic", content.c_str())) {
                Serial.println("UID RFID enviado ao broker MQTT.");
            } else {
                Serial.println("Falha ao enviar UID RFID ao broker MQTT.");
            }
            rfid.PICC_HaltA(); // Parar a leitura da tag RFID
        }
    }

private:
    MFRC522 rfid;
};

// HiveMQ Cloud Let's Encrypt CA certificate
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

// Configurações globais
const char* ssid = "Inteli-COLLEGE";
const char* password = "QazWsx@123";
const char* mqtt_server = "8790e88d997a4447ab7be359300b2097.s2.eu.hivemq.cloud";
const char* mqtt_username = "IoTvos";
const char* mqtt_password = "Inteli23";
const int mqtt_port = 8883;
constexpr uint8_t RST_PIN = 14;
constexpr uint8_t SS_PIN = 5;

WiFiClientSecure espClient;
PubSubClient pubSubClient(espClient);
WiFiManager wifiManager(ssid, password);
MqttClient mqttClient(mqtt_server, mqtt_username, mqtt_password, mqtt_port, espClient);
RFIDReader rfidReader(SS_PIN, RST_PIN);
