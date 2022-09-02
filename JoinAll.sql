SELECT * FROM Client c
JOIN ClientBankAccout cb
ON c.Id = cb.ClientId
JOIN CAR car
ON c.Id = car.ClientId
Join CarCharacteristics cs
ON car.Id = cs.CarId
WHERE c.Phone = '78978978979'


SELECT * FROM Client c
WHERE c.Phone = '98989898989'