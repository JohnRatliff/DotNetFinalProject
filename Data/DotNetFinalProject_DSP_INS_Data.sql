USE DotNetFinalProject;

DROP PROCEDURE IF EXISTS DSP_INS_Data;


/*------------------------------------------------------------------
$Name:
  DSP_INS_Data.sql

$Purpose:
  This stored procedure will insert new data into the following
  tables:  uomtypes, uoms, uomconversions.  If the parameter,
  pTruncateData is true, then existing data will be deleted
  and the PK seed value reset to 0.
  
$Parameters:
    -#1  pTruncateData: Tuncate existing data prior to insert (0=No, 1=Yes)
  
$Return:
  No return value

$Use:
  -#1
      
      CALL DSP_INS_Data (1);
      SELECT * FROM uomtypes;
      SELECT * FROM uoms;
      SELECT * FROM uomconversions;

$Revisions (# Date Initials Description):
  -#1  12/19/2018  JR  Created
-------------------------------------------------------------------*/
DELIMITER $$
CREATE PROCEDURE DSP_INS_Data (
  IN pTruncateData BIT
)

BEGIN


IF( pTruncateData = 1) THEN

  SET FOREIGN_KEY_CHECKS = 0;
  TRUNCATE TABLE uoms;
  TRUNCATE TABLE uomconversions;
  TRUNCATE TABLE uomtypes;
  SET FOREIGN_KEY_CHECKS = 1;

END IF;


INSERT INTO uomtypes (UomTypeName) VALUES
  ('Unit')    -- 1=Unit, 2=Weight, 3=Volume, 4=Length, 5=Time
, ('Weight')
, ('Volume')
, ('Length')
, ('Time');


INSERT INTO uoms (UomTypeSk, UomId, UomName) VALUES
  (2, 'lb', 'Pounds')       -- 1=Unit, 2=Weight, 3=Volume, 4=Length, 5=Time
, (2, 'ton', 'Ton')
, (2, 'g', 'Gram')
, (2, 'kg', 'Kilograms')
, (2, 'mt', 'Metric Ton')

, (3, 'cup', 'Cup')
, (3, 'pt', 'Pint')
, (3, 'qt', 'Quart')
, (3, 'gal', 'Gallon')

, (4, 'mm', 'Millimeter')
, (4, 'cm', 'Centimeter')
, (4, 'm', 'Meter')
, (4, 'km', 'Kilometer')

, (5, 'min', 'Minute')
, (5, 'hr', 'Hour')
, (5, 'day', 'Day');


INSERT INTO uomconversions (UomTypeSk, ConvertFromUomSk, ConvertToUomSk, ConversionFactor) VALUES
  (2, 1, 1, (1))             -- lb -> lb
, (2, 1, 2, (0.0005))        -- lb -> ton
, (2, 1, 3, (453.592))       -- lb -> g
, (2, 1, 4, (0.453592))      -- lb -> kg
, (2, 1, 5, (0.000453592))   -- lb -> mt

, (2, 2, 1, (2000))          -- ton -> lb
, (2, 2, 2, (1))             -- ton -> ton
, (2, 2, 3, (907185))        -- ton -> g
, (2, 2, 4, (907.185))       -- ton -> kg
, (2, 2, 5, (0.907185))      -- ton -> mt

, (2, 3, 1, (0.00220462))    -- g -> lb
, (2, 3, 2, (907184.74))     -- g -> ton
, (2, 3, 3, (1))             -- g -> g
, (2, 3, 4, (0.001))         -- g -> kg
, (2, 3, 5, (0.000001))      -- g -> mt

, (2, 4, 1, (2.20462))       -- kg -> lb
, (2, 4, 2, (0.00110231))    -- kg -> ton
, (2, 4, 3, (1000))          -- kg -> g
, (2, 4, 4, (1))             -- kg -> kg
, (2, 4, 5, (0.001))         -- kg -> mt

, (2, 5, 1, (2204.62))       -- mt -> lb
, (2, 5, 2, (1.10231))       -- mt -> ton
, (2, 5, 3, (1000000))       -- mt -> g
, (2, 5, 4, (1000))          -- mt -> kg
, (2, 5, 5, (1))             -- mt -> mt

, (3, 6, 6, (1))             -- cup -> cup
, (3, 6, 7, (0.5))           -- cup -> pint
, (3, 6, 8, (0.25))          -- cup -> quart
, (3, 6, 9, (0.625))         -- cup -> gal

, (3, 7, 6, (2))             -- pint -> cup
, (3, 7, 7, (1))             -- pint -> pint
, (3, 7, 8, (0.5))           -- pint -> quart
, (3, 7, 9, (0.125))         -- pint -> gal

, (3, 8, 6, (4))             -- quart -> cup
, (3, 8, 7, (2))             -- quart -> pint
, (3, 8, 8, (1))             -- quart -> quart
, (3, 8, 9, (0.25))          -- quart -> gal

, (3, 9, 6, (16))            -- gal -> cup
, (3, 9, 7, (8))             -- gal -> pint
, (3, 9, 8, (4))             -- gal -> quart
, (3, 9, 9, (1))             -- gal -> gal 

, (4, 10, 10, (1))           -- millimeter -> millimeter
, (4, 10, 11, (0.1))         -- millimeter -> centimeter
, (4, 10, 12, (0.001))       -- millimeter -> meter
, (4, 10, 13, (0.000001))    -- millimeter -> kilometer

, (4, 11, 10, (10))          -- centimeter -> millimeter
, (4, 11, 11, (1))           -- centimeter -> centimeter
, (4, 11, 12, (0.01))        -- centimeter -> meter
, (4, 11, 13, (0.00001))     -- centimeter -> kilometer

, (4, 12, 10, (1000))        -- meter -> millimeter
, (4, 12, 11, (100))         -- meter -> centimeter
, (4, 12, 12, (0.1))         -- meter -> meter
, (4, 12, 13, (0.001))       -- meter -> kilometer

, (4, 13, 10, (1000000))     -- kilometer -> millimeter
, (4, 13, 11, (10000))       -- kilometer -> centimeter
, (4, 13, 12, (1000))        -- kilometer -> meter
, (4, 13, 13, (1))           -- kilometer -> kilometer

, (5, 14, 14, (1))           -- minute -> minute
, (5, 14, 15, (0.0166667))   -- minute -> hour
, (5, 14, 16, (0.000694444)) -- minute -> day

, (5, 15, 14, (60))          -- hour -> minute
, (5, 15, 15, (1))           -- hour -> hour
, (5, 15, 16, (0.0416667))   -- hour -> day

, (5, 16, 14, (1440))         -- day -> minute
, (5, 16, 15, (24))           -- day -> hour
, (5, 16, 16, (1));            -- day -> day


END$$

-- Reset DELIMTER to its default value
DELIMITER ;
