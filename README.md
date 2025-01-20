# BZ Page project
BZ Page project is a web site I designed and partially (about 70%) developed in 2011.

The purpose of the website is to sell goods/services by individuals or organizations.
It consists of two main parts: a public user part that allows you to navigate and search for specific items/services, and an account management part that allows you to list items/services on the website, setup/manage account settings, etc.

The project was designed from the very beginning to be language independent. All labels are stored in the database as sets for each language. When a website runs, it loads all the labels for all supported languages into the server's memory as a set of objects, and when a request comes to the server, it generates a web page using the object for the desired language.

-	BZPDB contains all database (MS SQL server) related files. You can attach mdf file and run scripts from BZPDB\Scripts folder.

-	WEB contains web application files. This web project was created with MS Visual studio.
