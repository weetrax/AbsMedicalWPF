INSERT INTO `rfid`.`country` (`Id`, `Name`) VALUES ('FR', 'France'), ('BE', 'Belgique');

INSERT INTO `rfid`.`school` (`Guid`, `Name`, `Address`, `PostalCode`, `City`, `CountryId`, `Email`, `Phone`) VALUES ('149e8107-16bf-4771-95d3-b48933a9ede2', 'IUT Sophia Antipolis', '650 route des Colles', '06560', 'Sophia Antipolis', 'FR', 'martines.magnin@gmail.com', '0655884477');

INSERT INTO `rfid`.`doctor` (`Guid`, `Firstname`, `Lastname`, `Password`, `Email`, `Phone`, `Address`, `PostalCode`, `City`, `CountryId`, `MailConfigurationGuid`) VALUES ('5bd4b179-ae28-473d-af41-877b8b3fc3ac', 'Carl', 'White', '9e5267ca171a20b9a28d63ff7237f2b8', 'martines.magnin@gmail.com', '0622114455', '12 avenue Jean Médecin', '06300', 'Nice', 'FR', NULL);

INSERT INTO `rfid`.`student` (`Guid`, `StudentId`, `Firstname`, `Lastname`, `Email`, `Phone`, `Birthdate`, `Birthplace`, `Address`, `PostalCode`, `City`, `CountryId`, `SchoolGuid`, `SocialSecurityNumber`) VALUES ('ab839316', 'ma410183', 'Adrien', 'Magnin', 'adrien-06@hotmail.fr', '0628370988', '1996-01-20', 'Toulouse', '225 ch des Ames du Purgatoire', '06600', 'Antibes', 'FR', '149e8107-16bf-4771-95d3-b48933a9ede2', 'ab839316ab839316');

INSERT INTO `rfid`.`student` (`Guid`, `StudentId`, `Firstname`, `Lastname`, `Email`, `Phone`, `Birthdate`, `Birthplace`, `Address`, `PostalCode`, `City`, `CountryId`, `SchoolGuid`, `SocialSecurityNumber`) VALUES ('04617c4a', '04617c4a', 'Stefano', 'Martines', 'stefano.martines@hotmail.com', '0699835052', '1995-06-10', 'Tourcoing', '16 avenue Léon Montier', '06590', 'Theoule sur Mer', 'FR', '149e8107-16bf-4771-95d3-b48933a9ede2', '04617c4a04617c4a');
