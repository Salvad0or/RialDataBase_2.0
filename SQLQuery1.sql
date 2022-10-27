select * from Bots b
join Client c
on b.ClientId = c.Id
where b.Id = 28