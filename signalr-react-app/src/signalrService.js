// src/signalrService.js
import * as signalR from '@microsoft/signalr';

let connection;

export const startConnection = async (hubUrl, authToken) => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl, {
            accessTokenFactory: () => authToken,
            withCredentials: false
        })
        .withAutomaticReconnect()
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.onreconnecting((error) => {
        console.assert(connection.state === signalR.HubConnectionState.Reconnecting);
        console.log("Connection lost due to error. Reconnecting.", error);
    });

    connection.onreconnected((connectionId) => {
        console.assert(connection.state === signalR.HubConnectionState.Connected);
        console.log("Connection reestablished. Connected with connectionId", connectionId);
    });

    try {
        await connection.start();
        console.assert(connection.state === signalR.HubConnectionState.Connected);
        console.log("Connected to SignalR hub");
    } catch (err) {
        console.assert(connection.state === signalR.HubConnectionState.Disconnected);
        console.error("SignalR Connection Error: ", err);
        setTimeout(() => startConnection(hubUrl, authToken), 5000);  // Try to reconnect after 5 seconds.
    }
};

export const addReceiveMessageListener = (callback) => {
    if (connection) {
        connection.on('ReceiveMessage', (title, message, link) => {
                callback(title, message, link);
        });
    } else {
        console.error("Connection has not been established yet.");
    }
};

export const addNotificationListenerByConnectionId = (callback) => {
    if (connection) {
        connection.on('ReceiveMessage', (message, connectionId) => {
            if (connection.connectionId === connectionId) {
                callback(message);
            }
        });
    } else {
        console.error("Connection has not been established yet.");
    }
};

export const stopConnection = async () => {
    if (connection) {
        await connection.stop();
    }
};
