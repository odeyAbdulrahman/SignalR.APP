// src/App.js
import React, { useEffect, useState } from 'react';
import { startConnection, addReceiveMessageListener, addNotificationListenerByConnectionId, stopConnection } from './signalrService';
import './App.css';

const App = () => {
    const [notifications, setNotifications] = useState([]);
    const authToken = 'eyJhbGciOiJSUzI1NiIsImtpZCI6IkRDRjU0QTYzNkFBOUM2N0ZBNTZEODdBMkNFQjUzRkVGIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE3MTczODkyMTAsImV4cCI6MTcxNzY0ODQxMCwiaXNzIjoiaHR0cDovL3N0Zy1pbnQtaWRlbnRpdHktZG1hLnNoai5hZSIsImNsaWVudF9pZCI6IlNpbmdsUGFnZUNsaWVudCIsInN1YiI6IjVjY2U3MzEzLTg1NjUtNDFmYi1hNjkwLTAxNGE0Y2NjN2E2YSIsImF1dGhfdGltZSI6MTcxNzM4OTIxMCwiaWRwIjoibG9jYWwiLCJyb2xlIjoiQ2xpZW50IiwibmFtZSI6IjU1NTU1IiwiQ2xpZW50UHJvZmlsZUlkIjoiNDgiLCJMaWNlbnNlTm8iOiI1NTU1NSIsIkRlYWxpbmdUeXBlSWQiOiJDb25zdWx0YW50cyIsIkxpY2Vuc2VDYXRlZ29yeUlkIjoiNSIsIkFjdGl2ZVN0YXR1cyI6IlRydWUiLCJDbGllbnRBY3RpdmVTdGF0dXNJZCI6IkFjdGl2ZSIsIlZlcmlmaWNhdGlvblN0YXR1c0lkIjoiQWNjZXB0ZWRfUmVxdWVzdCIsImp0aSI6IkZBN0E5M0E0QUQyREFGNzUwQzc0RkQyMDM4N0Q3MTVDIiwiaWF0IjoxNzE3Mzg5MjEwLCJzY29wZSI6WyJVc2VyTWFuYWdlbWVudEFQSSJdLCJhbXIiOlsicHdkIl19.BXx1rGiGELlyWK9UYt2xqKQYikVnseV28cekZool1L8sc1DnS7E_93WdVqQk6VoWm0rhRlwIR231mMAQp08Qp7DF3FktO4lapCGK8X_r_n9uUVpShDIkOLpwhQ0NsrXw_zNP_M8OENDFG9DRNEjcI4vSKGDUxXPX00QEyPRgvkwRvEYDp87xvXmfvazsHvbsDa1_TnjWsqe8EiwTxIcIM0J98YJBhbTH5OqXza9DnGAgQdAxuc3ngmzgPTQ90AFO1bBnI6SCyQ79cR5IfE_PROkwZgJoaIwtdHi4ZS1PzyOoiDBRvkXjTNVD7OdUMjp0NZym_JyGwpi7-q4M_WO2rw'; // Replace with your actual auth token

    useEffect(() => {
        const hubUrl = 'http://localhost:8011/notificationHub'; // Use your actual hub URL
        startConnection(hubUrl, authToken);

        const handleNotification = (title, message, link) => {
            setNotifications((prevNotifications) => [...prevNotifications, {title, message, link}]);
        };

        addReceiveMessageListener(handleNotification);
        addNotificationListenerByConnectionId(handleNotification);
        
        return () => {
            stopConnection();
        };
    }, [authToken]);

    return (
        <div className="App">
            <h1>Notifications</h1>
            <ul>
                {notifications.map((notification, index) => (

                    <li key={index}>
                      <strong>title   :</strong> {notification.title} <br></br>
                      <strong>message :</strong> {notification.message} <br></br>
                      <strong>link    :</strong> {notification.link}</li>
                ))}
            </ul>
        </div>
    );
};

export default App;
