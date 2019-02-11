USE DotNetFinalProject;

DROP VIEW IF EXISTS DVW_Uom;

CREATE VIEW DVW_Uom AS
SELECT
    uoms.UomSk
  , uomtypes.UomTypeName
  , uoms.UomTypeSk
  , uoms.UomId
  , uoms.UomName
  , uoms.CreatedAt
  , uoms.UpdatedAt
  , COALESCE( COUNT( fromuomconversions.UomConversionSK), 0) AS FromConversionCount
  , COALESCE( COUNT( touomconversions.UomConversionSK), 0) AS ToConversionCount
  FROM uoms
  INNER JOIN uomtypes ON uomtypes.UomTypeSk = uoms.UomTypeSk
  INNER JOIN uomconversions fromuomconversions ON fromuomconversions.ConvertFromUomSK = uoms.UomSk
  INNER JOIN uomconversions touomconversions ON touomconversions.ConvertToUomSK = uoms.UomSk
  GROUP BY
    uoms.UomSk
  , uoms.UomTypeSk
  , uoms.UomId
  , uoms.UomName
  , uoms.CreatedAt
  , uoms.UpdatedAt;
  
--  SELECT * FROM DVW_Uom;
  
  
  