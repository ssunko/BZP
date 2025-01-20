set nocount on
declare @i int

select @i = 1

while @i < 11
begin
	select '   ' + Type + ':' from u_CategoryTypes where TypeID = @i
	select ' ' = Category from u_Categories where TypeID = @i
	select @i = @i + 1
end

