INSERT INTO `rfid`.`country` (`Id`, `Name`) VALUES ('1', 'France'), ('2', 'Belgique');

INSERT INTO `rfid`.`school` (`Guid`, `Name`, `Address`, `PostalCode`, `City`, `Email`, `Tel`, `TypeId`, `CountryId`) VALUES ('317c4e0a-d008-4a38-abe9-cfa91e2dc4d0', 'IUT Sophia Antipolis', '350 route des Colles', '06560', 'Valbonne', 'iut.unice.fr', '0445474878', '1', '1'), ('c33262eb-0c8d-47e6-9202-be1dcc5a0f57', 'Lycée Carnot', '100 Boulevard Carnot', '06400', 'Cannes', 'carnot@cannes.fr', '0455221144', '1', '1'), ('0975f821-d74b-4f13-b603-8707afd8eb43', 'Lycée Bristol', '10 avenue Saint Nicolas', '06400', 'Cannes', 'bristol@cannes.fr', '0499542541', '1', '1');
 
INSERT INTO `rfid`.`doctor` (`Guid`, `Lastname`, `Firstname`, `Address`, `PostalCode`, `City`, `Email`, `Password`, `CountryId`) VALUES ('5372adb6-078f-4ec6-a2fc-b52e63c570b7 ', 'Jean', 'Medecin', '16 avenue du vieux port', '06000', 'Nice', 'jean@nice.fr', '9e5267ca171a20b9a28d63ff7237f2b8', '1');

