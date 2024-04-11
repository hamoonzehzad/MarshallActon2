# Marshall Acton II

## Introduction
One of the main problems with the Marshall ACTON 2 speakers is that it will go to sleep mode after being idle for a couple of minutes and therefore disconnect from the operating system. 
There is no other way to econnect it except by pressing a physical button on the speaker so if you use your speaker in a book shelf for example it will be hard to push the physical button on the speaker every time that you want to reconnect to your speaker.
In order to fix this problem this is an application for checking the state of Bluetooth connection of you Marshall ACTON II speaker and reconnecting it if it is disconnected by going to sleep mode after being idle.

## How It Works
This application has a background service that checks the connectivity of the speaker in every one second. Your speaker should be already paired with your device. The application will find your speaker using its name.
Every single device has different type of bluetooth services that are responsible for different functionalities such as audio, video etc. Every single one of these services has its own unique UUID.
After the application finds the speaker it will try to reconnect its services one by one in parallel fashion.

## Some Notes

1- This application doesn't have anything to do with the low level chipset configuration and it just uses the high level communication APIs provided by the operating system.

2- Although this application is written by .NET 8 which is cross-platform but it's worth to mention that it uses just windows APIs to find the Bluetooth devices so you cannot run this application on Mac or Linux.



