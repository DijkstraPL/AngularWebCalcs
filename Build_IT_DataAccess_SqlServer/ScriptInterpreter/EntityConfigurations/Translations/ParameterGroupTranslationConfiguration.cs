using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;
using System;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations.Translations
{
    public class ParameterGroupTranslationConfiguration : IEntityTypeConfiguration<ParameterGroupTranslation>
    {
        public void Configure(EntityTypeBuilder<ParameterGroupTranslation> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ParameterGroupTranslations, ScriptInterpreterConstants.SchemaName);

            builder.HasKey(pgt
                => new { pgt.GroupId, pgt.Language });

            builder.Property(p => p.Language)
                .HasConversion(l => l.ToString(), lt => Enum.Parse<Language>(lt))
                .HasMaxLength(64);
        }
    }
}
