use BZP
GO
insert u_Classifieds(
	ClassifiedID,
	Title,
	[Text],
	StateID,
	CityID,
	ZipID,
	Phone,
	Email,
	CategoryID,
	SubCategoryID,
	Created,
	Stands,
	Active)
	
select 	
	NEWID(),
	'Computer P4 3.2GHz 2Gb + monitor CHEAP!!!',
	'For sale Computer P4 3.2GHz 2Gb.\n 19'''' LCD monitor for free.\n
	Asking $350.00. Negotiable. CHEAP!!!',
	38,
	21570,
	7127,
	'(718)339-2425',
	'sergoh@inbox.ru',
	1,
	1,
	GETDATE(),
	365,
	1