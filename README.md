# Wealth_Management_Services
Latest ASP.NET MVC Project

This is an ASP.NET MVC project which layout is set on a base View (Dashboard) that loads _Partial Views upon _Partial Views as per user's requests.
It is a simulation of a small investment company that has three departments. Areas are set to better structure the different departments and EntityFrame has been used for Data Connection along with LINQ and ADO.NET for Data Access and Manipulation. (See the PNG file for the Site Map). A Sql Server TRIGGER has been set to record  dynamically any Broker stock purchase to the Transaction Billboard of the Managers.

Any user can login as: <br/>
<b>Manager</b>: to view investor data and Broker purchases dynamically. <br/>
<b>Broker</b>: to view investor data and purchase stocks or bonds. <br/>
<b>investor</b>: to view his/her returns and contact broker or register for a 'new account'. But if an investor fails login more than three times, he/she will be locked out and a SQL Server Agent is set to unlock their account after five minutes. BUT unfortunately, I am using a 'Shared Server' account on GoDaddy which doesn't provide for such a tool, so I used a Stored Procedure to release any user that was locked every 10 MINUTES.

*** HOW TO LOGIN: *** <br/>
(See the PNG files for all SQL Server Database tables or use the names below) <br/>
<b>MANAGEMENT</b>: <br/>
You can use this name: <br/>
jim: username = ji123, password = Ji77777$ <br/>
<b>BROKERS</b>: <br/>
You can use this name: <br/>
heathcliff: username = he123, password = He77777$ <br/>
<b>INVESTORS</b>: <br/>
You can use this name: <br/>
bill gates: username = bi123, password = Bi77777$ <br/>
you can also create your own account by following the same naming pattern: <br/>
username = xx123, password = Xx12345$ 

Unfortunately the site is not responsive yet.
