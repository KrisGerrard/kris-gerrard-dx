# kris-gerrard-dx
# This is a sample project from my Portfolio, for more information please see http://www.kgcoding.com/Projects/Dev?id=1
## Kris Gerrard / www.kgcoding.com / kris.gerrard@gmail.com


#### Database
Hosted in Azure, MS SQL version 2014.
However a local backup of the DB in DigitalXDB_20161123.BAK is in the root folder, and can be imported into a local SQL instance. 
The DB was imported into Azure using ImportScript.sql. When loaded cascading deletes for orders need to be enabled. 


#### WCF
Please note the basic HTTP binding has been modified to allow unlimited size for testing, particularly when pulling the entire inventory.
When Integration testing a glitch can sneak in requiring the Service Reference to be updated in the testing project, once this is done the problem disappears.
Connection string is set for use with the hosted Azure DB.


#### WEB
Connects to WCF service endpoint, therefore requires WCF project to be running.
