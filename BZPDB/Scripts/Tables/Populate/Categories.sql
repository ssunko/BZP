/* u_CategoryTypes:
1	For sale	1
2	Cars & vehicles	1
3	Housing	1
4	Personals	1
5	Services	1
6	Jobs	1
7	Pets	1
8	Community	1
9	Resumes	1
10	Free Stuff	1
*/
begin tran
declare @TypeID smallint,
		@CategoryID smallint
select	@CategoryID = 1

select @TypeID = TypeID from u_CategoryTypes where Type = 'For sale'

insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Antiques', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Appliances', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Arts & crafts', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Baby & kids', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Bicycles', @TypeID, 1	
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Books & magazines', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Business', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Cameras & photo', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Cell Phones & PDAs', @TypeID, 1	
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Clothing, Shoes & accessories', @TypeID, 1	
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Collectibles & coins', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Computers & accessories', @TypeID, 1	
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Electronics', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Farm, Garden & outdoor', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Furniture', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Health & beauty', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Home decor', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Jewelry & watches', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Materials', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Movies & music', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Musical instruments', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Photo & video', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Pottery & glass', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Sporting goods', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Stamps & cards', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Tickets', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Tools', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Toys & games', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Trade & barter', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Travel', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Video gaming', @TypeID, 1
select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Everything else', @TypeID, 1


select @TypeID = TypeID from u_CategoryTypes where Type = 'Cars & vehicles'

	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Boats', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Cars', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Classic cars', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Motorcycles & scooters', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Parts & accessories', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Powersports', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Tires', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Trailers & RVs', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Trucks, vans, & SUVs', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Everything else', @TypeID, 1
	
select @TypeID = TypeID from u_CategoryTypes where Type = 'Housing'
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Apartments for rent', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Homes for rent', @TypeID, 1	
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Homes for sale', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Roommates & shared', @TypeID, 1	
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Housing swap', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Short term & sublets', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Vacation rentals', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Offices & commercial', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Land for sale', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Parking & storage', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Housing wanted', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Real estate services', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Other', @TypeID, 1	
	
select @TypeID = TypeID from u_CategoryTypes where Type = 'Personals'
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Romantic & platonic', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Man seeking Woman', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Woman seeking Man', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Woman seeking Woman', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Man seeking Man', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Let''s party', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Lost friends', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Rants & raves', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Everything else', @TypeID, 1
	
select @TypeID = TypeID from u_CategoryTypes where Type = 'Services'
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Art, Music, & decor', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Automotive', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Beauty & health', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Computer', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Cleaning & maintenance', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Elderly care', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Events & occasions', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Farm & garden', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Financial & mortgages', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Household', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Legal & lawyer', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Lessons & tutoring', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Marine', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Moving & storage', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Pet', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Repair & remodel', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Retail & merchandise', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Skilled trades', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Therapeutic', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Travel & vacation', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Writing, Editing & translation', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Other', @TypeID, 1
	
select @TypeID = TypeID from u_CategoryTypes where Type = 'Jobs'
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Accounting & finance', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Administrative & office', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Architecture & engineering', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Art, Media & design', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Biotech, R&D, & science', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Business & management', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Construction & trades', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Customer service', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Education & training', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Food, Beverage & hospitality', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'General labor', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Graphic & web design', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Government', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Human resources', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'IT & software development', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Legal & paralegal', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Manufacturing', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Marketing & PR', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Medical & healthcare', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Networking', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Real estate', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Retail & wholesale', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Sales & biz dev', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Salon, spa, & fitness', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Security', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Skilled trade & craft', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Technical support', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Transportation', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'TV, Film & video', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Web design', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Writing & editing', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Everything else', @TypeID, 1
	
select @TypeID = TypeID from u_CategoryTypes where Type = 'Pets'
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Accessories', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Animal services', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Birds', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Cats & kittens', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Dogs & puppies', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Fishes & aquariums', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Livestock', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Reptiles', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Lost & found', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Other', @TypeID, 1
	
select @TypeID = TypeID from u_CategoryTypes where Type = 'Community'
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Activities', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Announcements', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Artists & musicians', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Childcare & babysitting', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Classes & lessons', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Garage sales', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'General', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Pets', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Local news', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Lost & found', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Politics', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Rideshare & carpool', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Volunteers', @TypeID, 1
	select @CategoryID = @CategoryID + 1
insert u_Categories(CategoryID, Category, TypeID, Active)
select @CategoryID, 'Other', @TypeID, 1

-- commit tran
-- rollback tran