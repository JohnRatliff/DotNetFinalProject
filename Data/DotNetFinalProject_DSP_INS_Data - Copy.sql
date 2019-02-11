USE DotNetFinalProject;

DROP PROCEDURE IF EXISTS DSP_INS_Data;


/*------------------------------------------------------------------
$Name:
  PSP_INS_FPTally.sql

$Purpose:
  This stored procedure will create a new FP Tally based on supplied 
  parameters. 
  
$Parameters:
    -#1  @pTallyID....................: FPTally ID of the new tally. Required.
  
$Return:
  Returns 0 if successful, and -1 if an error occurred.

$Use:
  -#1
      
      USE DotNetFinalProject;
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
, (2, 'mt', 'Metric Ton');


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
, (2, 5, 5, (1));            -- mt -> mt

END$$

-- Reset DELIMTER to its default value
DELIMITER ;
