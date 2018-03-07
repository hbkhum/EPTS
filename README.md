# EPTS
Acquisition data system

BackEnd
The Backend was develop using the model N-tiers, where is posible to observe each module is for specif task, with this is comply the SOLID.

This project uses the next layers: Entity Framework code firts, POCO Class, Repositories[Generic Repository Add,Update, Delete, Get],Services, Apicontroller and SignalR) is the BackEnd.

![alt text](https://github.com/hbkhum/EPTS/blob/master/Architecture.png)


Example:

http://humbertopedraza.dynu.com/epts/webapi/api/BusinessUnit
![alt text](https://github.com/hbkhum/EPTS/blob/master/Web%20API%20Service.png)


he web service can use 'where' and 'sorting' for each endpoint
![alt text](https://github.com/hbkhum/EPTS/blob/master/Web%20Service%20Paging.png)

![alt text](https://github.com/hbkhum/EPTS/blob/master/Web%20API%20Service%20Sort.png)

Source Code:

https://github.com/hbkhum/EPTS/tree/master/Repositories/WebServices/EPTS.Repositories.WebServices.WebAPI


In this project I using 4 User Interfaces
Desk App - WPF
Web App
Mobile App
Class Project

![alt text](https://github.com/hbkhum/EPTS/blob/master/User%20Interface%20type.png)

Demo Web App
Now, respect whit the front end , this project uses, Bootstrap, Jquery, AngularJS 1.6.4.

The application demo:

http://humbertopedraza.dynu.com/epts/#/BusinessUnit

http://humbertopedraza.dynu.com/epts/#/Family

http://humbertopedraza.dynu.com/epts/#/Model

![alt text](https://github.com/hbkhum/EPTS/blob/master/Web%20User%20Interface.png)
Edit Button
![alt text](https://github.com/hbkhum/EPTS/blob/master/Web%20User%20Interface%20Edit.png)
Add Button
![alt text](https://github.com/hbkhum/EPTS/blob/master/Web%20User%20Interface%20Add.png)

Source Code:

https://github.com/hbkhum/EPTS/tree/master/UI/Web%20App/EPTS.UI.Web

In the Devices is a layer for instruments control as Multimeter, Power Supply, SeaLevel(Read digital Input, Write Digital Outputs, Read Analogic voltage, using TCP Sockets), I'm using Event Delegate for read data and after pass data to UI:

Event Delegate 
![alt text](https://github.com/hbkhum/EPTS/blob/master/Scanner.png)
![alt text](https://github.com/hbkhum/EPTS/blob/master/Multimeter.png)
![alt text](https://github.com/hbkhum/EPTS/blob/master/Spectrum%20Analizer.png)
![alt text](https://github.com/hbkhum/EPTS/blob/master/Digital%20IO.png)

this project too uses data adquisition of industrial instruments as multimeter, power supply, spectrum analyzer, oscilloscope, this communication is possile using rs232, rs485, GPIB and Ethernet(Socket)

Source Code
https://github.com/hbkhum/EPTS/tree/master/UI/Class/Devices

The other UI, I'm using WPF MVVM in this application, I'm using the UI Device layer:

![alt text](https://github.com/hbkhum/EPTS/blob/master/WPF%20User%20Interface.png)
![alt text](https://github.com/hbkhum/EPTS/blob/master/WPF%20User%20Interface%20Devices.png)
![alt text](https://github.com/hbkhum/EPTS/blob/master/WPF%20User%20Interface%20Devices%202.png)

Source Code
https://github.com/hbkhum/EPTS/tree/master/UI/Desktop%20App
https://github.com/hbkhum/EPTS/tree/master/UI/Desktop%20App/EPTS.UI.ViewModel
https://github.com/hbkhum/EPTS/tree/master/UI/Desktop%20App/EPTS.UI.WPF

Exists a relationship between Test plan and WPF main screen, where each client (WPF App) download the test plan, but if necessary make some change of limits is reflected instantly, because it's using SignalR.

![alt text](https://github.com/hbkhum/EPTS/blob/master/SignalR.png)

user Interface:
![alt text](https://github.com/hbkhum/EPTS/blob/master/Web%20User%20Interface%20Testing.png)
![alt text](https://github.com/hbkhum/EPTS/blob/master/WPF%20User%20Interface%20Testing.png)

http://humbertopedraza.dynu.com/epts/#/TestPlan
