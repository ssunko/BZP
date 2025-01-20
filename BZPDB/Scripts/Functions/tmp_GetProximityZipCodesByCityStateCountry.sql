CREATE FUNCTION [dbo].[GetProximityZipCodesByCityStateCountry] 
(
@Country varchar(2), 
@City varchar(200),
@State varchar(2),
@MinRadius int,
@MaxRadius int,
@Units char(1)
)
RETURNS 
@Zips TABLE 
(
ZipCode varchar(5)
)
AS
BEGIN
DECLARE @Min int, @Max int, @center Geography

IF(@Units = 'k') --Kilometers
SELECT @Min = (@MinRadius * 1000), @Max = (@MaxRadius * 1000)
ELSE -- Miles
SELECT @Min = (@MinRadius * 1609.344), @Max = (@MaxRadius * 1609.344)

SET @center = (SELECT geography::STGeomFromText('POINT(' + CONVERT(VARCHAR(100),AVG(Longitude)) +' ' + CONVERT(VARCHAR(100),AVG(Latitude)) +')',4326) 
FROM ZipCodes g 
WHERE Country = @Country AND City = @City and StateAbbreviation = @State)

INSERT INTO @Zips
SELECT ZipCode
FROM zipcodes
WHERE GeogCol1.STDistance(@center) BETWEEN @Min AND @Max

RETURN 
END
GO 
