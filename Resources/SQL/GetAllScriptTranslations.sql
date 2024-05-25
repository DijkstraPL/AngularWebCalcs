SELECT TOP (100) PERCENT dbo.Scripts_Parameters.Id, 
						 dbo.Scripts_Parameters.Name, 
						 dbo.Scripts_Parameters.Description, 
						 dbo.Scripts_Parameters.Notes, 
						 dbo.Scripts_ParametersTranslations.Language, 
                         dbo.Scripts_ParametersTranslations.Description AS TranslatedDescription, 
						 dbo.Scripts_ParametersTranslations.Notes AS TranslatedNotes, 
						 dbo.Scripts_ParametersTranslations.ParameterId
FROM dbo.Scripts_Parameters LEFT OUTER JOIN
     dbo.Scripts_ParametersTranslations 
ON dbo.Scripts_Parameters.Id = dbo.Scripts_ParametersTranslations.ParameterId