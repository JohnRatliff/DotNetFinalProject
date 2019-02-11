USE DotNetFinalProject;

SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE settings;
TRUNCATE TABLE usersettings;
TRUNCATE TABLE accounts;
TRUNCATE TABLE orderproducts;
TRUNCATE TABLE orders;
TRUNCATE TABLE paymentterms;
TRUNCATE TABLE contacts;
TRUNCATE TABLE paymentterms;
TRUNCATE TABLE pricelistproducts;
TRUNCATE TABLE pricelists;
TRUNCATE TABLE uomconversions;
TRUNCATE TABLE uoms;
TRUNCATE TABLE uomtypes;
TRUNCATE TABLE users;
SET FOREIGN_KEY_CHECKS = 1;

INSERT INTO users (Email, FirstName, LastName, UserLevel, Password) VALUES
  ('a@gmail.com', 'Andy', 'Apple', 2, 'AQAAAAEAACcQAAAAEHH5LxSHlafnNVM+HgObP3UnAgCtfeJJUjJslrJ1pIwhRO0G3nDCkDex3DpNlyly6Q==')
, ('b@gmail.com', 'Barb', 'Benson', 2, 'AQAAAAEAACcQAAAAEIFBROJJCnbp8/fdaAGQTDKSHVijtPWoei5Nk+GTn8fBx6hw+xTmi+Xok84MYQPAGA==')
, ('c@gmail.com', 'Cari', 'Carmon', 2, 'AQAAAAEAACcQAAAAEGmeK5BqTcQmxdzK5D0p3FbMqQ500vmv8ILljJoltpn7XqdGunmACAJ7DQGR+WxpQg==')
, ('d@gmail.com', 'David', 'Dickenson', 2, 'AQAAAAEAACcQAAAAEKiVbSqBMYW9tGOOjW3DRG7+r8VNvBzTtSUtB3X3ZYqPXBz3I4s4CVqonU8duySSQg==')
, ('e@gmail.com', 'Erin', 'Erikson', 2, 'AQAAAAEAACcQAAAAEKZPQTrrcfnseR++K0iGPUrjAPlyVGAN1+q8RdQoJ0S/DtyND819CiaA3SACRk384Q==')
, ('f@gmail.com', 'Fred', 'Franklin', 2, 'AQAAAAEAACcQAAAAENG0CQQKQj0b6AkS1xWL7dGQ2NTFtTKHSwaAyO6yHXaLPpF0s2uswSwZ3/WX5qSWMw==')
, ('g@gmail.com', 'Gary', 'Grearson', 2, 'AQAAAAEAACcQAAAAEBKjP3SxEf2VMLUpZNBWRdmnyVfIBknfRS9UVyMH1fxzK63ksfLmIBMhhuG5FmGY7g==')
, ('h@gmail.com', 'Helen', 'Holden', 2, 'AQAAAAEAACcQAAAAELjcTZeeo9JgixicO6/jpW2dTs9wWn6tGPJufPhdEccePlxW1GyNlC6buAOgImA//g==')
, ('i@gmail.com', 'Irene', 'Ingle', 2, 'AQAAAAEAACcQAAAAEA7BY2bkYxnRf5Cy8Hwsyqt6+KrE+SSpInE0IR70DGsrci2m3aWVNbdhBhR9Ai3lyA==')
, ('j@gmail.com', 'Jeff', 'Jefferson', 2, 'AQAAAAEAACcQAAAAEK8vaX1FZyd+xuN9hL/HMRqoTEUWrxqu0fs+7s2AsBlsS2ckFK8ffgHdcR5qp8Eshw==')
, ('k@gmail.com', 'Kendra', 'Kile', 2, 'AQAAAAEAACcQAAAAEEA8N6Yh8KCCNJaYfnGsADDVZK9LZHCfl/1IvdAzYQTXc/AdTFscpcWYvW/cz8ECTw==')
, ('l@gmail.com', 'Larry', 'Lind', 2, 'AQAAAAEAACcQAAAAEH4UUbVx5WA+NBxXuaggv3zgP2NoNWHubcD9YxM321cs1AOxaKn/ZVQiWpofIp0PCg==')
, ('m@gmail.com', 'Mary', 'Meadows', 2, 'AQAAAAEAACcQAAAAEDqeRYtOCGI2oNmIgZb1DtGc73QfcAC/0SRrcj2XEO4DWDGAqn4hxmjRY92ymYcxew==')
, ('n@gmail.com', 'Neal', 'Nickerson', 2, 'AQAAAAEAACcQAAAAEDzGVEiLFpDPt6ZCyL8S4QboeAWw4959r3MPHJRWhenMOOiaQdBsDwP/RK3I9NYnlw==')
, ('o@gmail.com', 'Orly', 'Olson', 2, 'AQAAAAEAACcQAAAAEDQ6k3itNzOWjwgaa5rTsj0Kf5parT+XYI/J53GSU0Y5MRhJ/Zk7SCwqIM4bnf5eUw==')
, ('p@gmail.com', 'Pam', 'Paulson', 2, 'AQAAAAEAACcQAAAAEBtGYe9V1DNwD1jy6kEWxz/BtOZmHNJTfYk45YlEECYhn7n5rchCi4Hwnt7nt0GlRw==')
, ('q@gmail.com', 'Quig', 'Quittle', 2, 'AQAAAAEAACcQAAAAEC2HX0KUBER/MreQkTgE1cADICnaLc6FroKVi4V0YqTpaHsA96KpgXWRcCnYW6h6DQ==')
, ('r@gmail.com', 'Neal', 'Nickerson', 2, 'AQAAAAEAACcQAAAAEF0afGmS4KDOdB3x0v8h8YYgzraLjUmpI09foxul8lnshXndRY9R4ix0KBzVfn4+eA==')
, ('s@gmail.com', 'Neal', 'Nickerson', 2, 'AQAAAAEAACcQAAAAEAemkMxE0uFtUrFTbUKBsbpk+x6RDychL5BaBbXASuddsi1le1ZJ0ykfKK2CIo4a6A==')
, ('t@gmail.com', 'Neal', 'Nickerson', 2, 'AQAAAAEAACcQAAAAEH+Rf6Zjr2JaHqPvAeVD7IqpUpBdaYSosn2WqrfFFkh54i4cYH+vgY7XL7fA5Iqp+Q==')
, ('u@gmail.com', 'Neal', 'Nickerson', 2, 'AQAAAAEAACcQAAAAEM56cHGIG7q95hKJR/sBUju01w45Nahfv8DR6lZtTsxgQuNBLwipFaZNV0Rdh/cMEA==')
, ('v@gmail.com', 'Neal', 'Nickerson', 2, 'AQAAAAEAACcQAAAAEP4aGSuvGPpI8tvqOxizQ2r/PdfZ2RitbHaGFaezR2PEt3TCnvVc9q959Ko21dstkQ==')
, ('w@gmail.com', 'Neal', 'Nickerson', 2, 'AQAAAAEAACcQAAAAEF9XpNPC/Z8i6eZ5+5AbrKkA2uz8rwH4yt3fbAUG1jIYopdSGy8CjpYnuPaX/MwEWQ==')
, ('y@gmail.com', 'Neal', 'Nickerson', 2, 'AQAAAAEAACcQAAAAEOLSpXB0zAm4iOHNCK8MQQVZCFoX36Ex78Dp80AGs6uqj/ONc65volRuIBQde15k2A==')
, ('z@gmail.com', 'Neal', 'Nickerson', 2, 'AQAAAAEAACcQAAAAEIf7J3VysBxROL4adqmIe/KQhOstIszltDjC4I1EXP1oFupminYjCk7OEY2YrhTVoQ==')
, ('admin@gmail.com', 'Admin', 'Andy', 1, 'AQAAAAEAACcQAAAAEM9t/RPHGngVhGSWOfBam1QGR9geUp5QuFw0hFiGPP3mvC/dFFb3t+oaNyGeA3t9HA==');


-- INSERT INTO uomtypes (UomTypeName) VALUES
--   ('Unit')    -- 1=Unit, 2=Weight, 3=Volume, 4=Length, 5=Time
-- , ('Weight')
-- , ('Volume')
-- , ('Length')
-- , ('Time');


-- INSERT INTO uoms (UomTypeSk, UomId, UomName) VALUES
--   (2, 'lb', 'Pounds')       -- 1=Unit, 2=Weight, 3=Volume, 4=Length, 5=Time
-- , (2, 'ton', 'Ton')
-- , (2, 'g', 'Gram')
-- , (2, 'kg', 'Kilograms')
-- , (2, 'mt', 'Metric Ton');


-- INSERT INTO uomconversions (UomTypeSk, ConvertFromUomSk, ConvertToUomSk, ConversionFactor) VALUES
--   (2, 1, 1, (1))             -- lb -> lb
-- , (2, 1, 2, (0.0005))        -- lb -> ton
-- , (2, 1, 3, (453.592))       -- lb -> g
-- , (2, 1, 4, (0.453592))      -- lb -> kg
-- , (2, 1, 5, (0.000453592))   -- lb -> mt

-- , (2, 2, 1, (2000))          -- ton -> lb
-- , (2, 2, 2, (1))             -- ton -> ton
-- , (2, 2, 3, (907185))        -- ton -> g
-- , (2, 2, 4, (907.185))       -- ton -> kg
-- , (2, 2, 5, (0.907185))      -- ton -> mt

-- , (2, 3, 1, (0.00220462))    -- g -> lb
-- , (2, 3, 2, (907184.74))     -- g -> ton
-- , (2, 3, 3, (1))             -- g -> g
-- , (2, 3, 4, (0.001))         -- g -> kg
-- , (2, 3, 5, (0.000001))      -- g -> mt

-- , (2, 4, 1, (2.20462))       -- kg -> lb
-- , (2, 4, 2, (0.00110231))    -- kg -> ton
-- , (2, 4, 3, (1000))          -- kg -> g
-- , (2, 4, 4, (1))             -- kg -> kg
-- , (2, 4, 5, (0.001))         -- kg -> mt

-- , (2, 5, 1, (2204.62))       -- mt -> lb
-- , (2, 5, 2, (1.10231))       -- mt -> ton
-- , (2, 5, 3, (1000000))       -- mt -> g
-- , (2, 5, 4, (1000))          -- mt -> kg
-- , (2, 5, 5, (1));            -- mt -> mt


INSERT INTO paymentterms (Name, DiscountDays, DueDays, DiscountRate, DiscountDateOption) VALUES
  ('2 10 net 30', 10, 30, 2.00, 1)
, ('due in 30', 0, 30, 0.00, 1);


INSERT INTO settings (LastOrderId, LastProductId, PriceListSk, TaxRate) VALUES
  (999, 9999, NULL, 10.0);


INSERT INTO usersettings (UserSk, InitialView) VALUES
  ( 1,  1)
, ( 2,  1)
, ( 3,  1)
, ( 4,  1)
, ( 5,  1)
, ( 6,  1)
, ( 7,  1)
, ( 8,  1)
, ( 9,  1)
, (10,  1)
, (11,  1)
, (12,  1)
, (13,  1)
, (14,  1)
, (15,  1)
, (16,  1)
, (17,  1)
, (18,  1)
, (19,  1)
, (20,  1)
, (21,  1)
, (22,  1)
, (23,  1)
, (24,  1)
, (25,  1)
, (26,  1);


SELECT * FROM users;

SELECT
    uomtypes.UomTypeName
  , fromuoms.UomId AS FromUomId
  , touoms.UomId AS ToUomId
  , uomconversions.ConversionFactor
  FROM uomconversions
  INNER JOIN uomtypes ON uomtypes.UomTypeSk = uomconversions.UomTypeSk
  INNER JOIN uoms fromuoms ON fromuoms.UomSK = uomconversions.ConvertFromUomSk
  INNER JOIN uoms touoms   ON touoms.UomSk = uomconversions.ConvertToUomSk
  ORDER BY
    fromuoms.UomId
  , touoms.UomId;
  

SELECT * FROM paymentterms;

SELECT * FROM settings;

SELECT
    users.Email
  , usersettings.InitialView
  FROM usersettings
  INNER JOIN users ON users.UserSK = usersettings.UserSK;
