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




Demo:
http://humbertopedraza.dynu.com/epts/webapi/api/Family

Now, respect whit the front end , this project uses, Bootstrap, Jquery, AngularJS 1.6.4.

http://humbertopedraza.dynu.com/epts/#/Family


this project too uses WPF MVVM, and used for data adquisition of industrial instruments as multimeter, power supply, spectrum analyzer, oscilloscope, this communication is possile using rs232, rs485, GPIB and Ethernet(Socket)
