SELECT TOP (100) PERCENT dbo.Scripts_Scripts.Id, 
						 dbo.Scripts_Scripts.Name AS ScriptName, 
						 dbo.Scripts_Scripts.Description AS ScriptDescription, 
						 dbo.Scripts_Parameters.Id, 
						 dbo.Scripts_Parameters.Number, 
						 dbo.Scripts_Parameters.Name, 
						 dbo.Scripts_Parameters.Description, 
                         dbo.Scripts_Parameters.Value, 
						 dbo.Scripts_Parameters.Unit,
						 dbo.Scripts_Parameters.VisibilityValidator, 
						 dbo.Scripts_Parameters.DataValidator,
						 dbo.Scripts_Parameters.ValueType, 
						 dbo.Scripts_Parameters.ValueOptionSetting, 
						 dbo.Scripts_Parameters.Context, 
						 dbo.Scripts_Parameters.AccordingTo, 
						 dbo.Scripts_Parameters.Notes, 
						 dbo.Scripts_Parameters.GroupId, 
						 dbo.Scripts_Parameters.VersionId
FROM dbo.Scripts_Scripts LEFT OUTER JOIN
     dbo.Scripts_Parameters
ON dbo.Scripts_Scripts.Id = dbo.Scripts_Parameters.ScriptId
ORDER BY ScriptName, dbo.Scripts_Parameters.Number