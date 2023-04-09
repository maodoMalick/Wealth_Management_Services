# Wealth_Management_Services
Fictional Financial Company

This is an ASP.NET project which layout is set on a base View (Dashboard) that loads _Partial Views upon _Partial Views as per user's requests.
It is a simulation of a small investment company that has three departments. Areas have been used to better structure the different departments and EntityFrame has been used for Data Connection along with LINQ and ADO.NET for Data Access and Manipulation. (See the PNG file for the Site Map)

Any user can login as:
Manager: to view investor data and Broker purchases dynamically.
Broker: to view investor data and purchase stocks or bonds.
investor: to view his/her returns and contact broker or register for a 'new account'. But if an investor fails login more than three times, he/she will be locked out and a SQL Server Agent is set to unlock their account after five minutes. BUT unfortunately, I am using a 'Shared Server' account on GoDaddy which doesn't provide for such a tool.

*** HOW TO LOGIN: *** <br/>
(See the PNG files for all SQL Server Database tables or use the names below) <br/>
MANAGEMENT: <br/>
You can use this name: <br/>
jim: username = ji123, password = Ji77777$ <br/>
BROKERS: <br/>
You can use this name: <br/>
heathcliff: username = he123, password = He77777$ <br/>
INVESTORS: <br/>
You can use this name: <br/>
bill gates: username = bi123, password = Bi77777$ <br/>
you can also create your own account by following the same naming pattern: <br/>
username = xx123, password = Xx12345$ 

Unfortunately the site is not responsive yet.
