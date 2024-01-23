import React, { useEffect } from "react";
import mqtt from "mqtt";

const host = 'wss://8790e88d997a4447ab7be359300b2097.s2.eu.hivemq.cloud:8884/mqtt';
const clientId = 'IOTVOS';

const mqttOptions = {
    username: 'IoTvos',
    password: 'Inteli23',
    clientId: clientId
};

function ConnectClient() {
    const [client, setClient] = React.useState(null);
    const [connectionStatus, setConnectionStatus] = React.useState(false);
    const [latestMessage, setLatestMessage] = React.useState("");

    useEffect(() => {

        const clientInstance = mqtt.connect(host, mqttOptions);

        const onConnect = () => {
            console.log('Connected to MQTT Broker');
            setConnectionStatus(true);

            const subTopic = 'rfidTopic';
            clientInstance.subscribe(subTopic);
        }

        const onMessage = (topic, message) => {
            console.log('Received message:', message.toString());
            setLatestMessage(message.toString());
        }

        console.log('Connecting');
        clientInstance.on('connect', onConnect);
        clientInstance.on('message', onMessage);

        clientInstance.on('reconnect', () => {
            console.log('Reconnecting');
        });

        setClient(clientInstance);

        return () => {
            console.log('Disconnecting');
            clientInstance.end();
        }

    }, []);

    return { client, connectionStatus, latestMessage };
}

export default ConnectClient;