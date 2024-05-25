using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;
using System;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations.Translations
{
    public class ParameterTranslationConfiguration : IEntityTypeConfiguration<ParameterTranslation>
    {
        public void Configure(EntityTypeBuilder<ParameterTranslation> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ParametersTranslations, ScriptInterpreterConstants.SchemaName);

            builder.Property(s => s.Description)
                .HasMaxLength(1024);
            builder.Property(s => s.Notes)
                .HasMaxLength(1024);

            builder.HasKey(pt
                => new { pt.ParameterId, pt.Language });

            builder.Property(p => p.Language)
                .HasConversion(l => l.ToString(), lt => Enum.Parse<Language>(lt))
                .HasMaxLength(64);
        }
    }
}
