# BZ Page project
BZ Page project is a web site I designed and partially (about 70%) developed in 2011.
-	BZPDB contains all database (MS SQL server) related files. You can attach mdf file and run scripts from BZPDB\Scripts folder.

-	WEB contains web application files. This web project was created with MS Visual studio.

Project was designed from the very beginning to be language independent. All labels are stored in DB as sets for each language. When web site starts - it loads all labeles for all supported languages into server memory as a set of objects and when request comes to the server, it generates web page using object for needed language.
