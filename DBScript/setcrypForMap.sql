ALTER TABLE EDG_EDGE DROP COLUMN EDG_NAME_ENC 
GO
DROP SYMMETRIC KEY EDGKey
GO 
DROP CERTIFICATE CertPMap
GO
DROP MASTER KEY 
GO

CREATE MASTER KEY ENCRYPTION
BY PASSWORD = 'PMapPtx'
GO

CREATE CERTIFICATE CertPMap 
ENCRYPTION BY PASSWORD = 'FormClosedEventArgs'
WITH SUBJECT = 'PMapPtx'
GO


CREATE SYMMETRIC KEY EDGKey
WITH ALGORITHM = TRIPLE_DES ENCRYPTION
BY CERTIFICATE CertPMap
GO

ALTER TABLE EDG_EDGE ADD EDG_NAME_ENC varchar(max)
GO

OPEN SYMMETRIC KEY EDGKey DECRYPTION
BY CERTIFICATE CertPMap WITH PASSWORD = 'FormClosedEventArgs'
UPDATE EDG_EDGE
SET EDG_NAME_ENC = ENCRYPTBYKEY(KEY_GUID('EDGKey'),EDG_NAME)

OPEN SYMMETRIC KEY EDGKey DECRYPTION BY CERTIFICATE CertPMap WITH PASSWORD = 'FormClosedEventArgs'
SELECT CONVERT(varchar(max),DECRYPTBYKEY(EDG_NAME_ENC)) AS EDG_NAME
FROM EDG_EDGE
select EDG_NAME from EDG_EDGE

open symmetric key EDGKey decryption by certificate CertPMap with password = 'FormClosedEventArgs' 
select NOD.ID as NOD_ID, EDG.ID as EDG_ID, NOD.ZIP_NUM, ZIP_CITY, convert(varchar(max),decryptbykey(EDG_NAME_ENC)) as EDG_NAME,  
EDG_STRNUM1, EDG_STRNUM2, EDG_STRNUM3, EDG_STRNUM4 
from NOD_NODE NOD 
inner join EDG_EDGE EDG on EDG.NOD_NUM = NOD.ID or EDG.NOD_NUM2 = NOD.ID 
inner join ZIP_ZIPCODE ZIP on ZIP.ZIP_NUM = NOD.ZIP_NUM 
where NOD.ID = 343761 and EDG.ID=440846
