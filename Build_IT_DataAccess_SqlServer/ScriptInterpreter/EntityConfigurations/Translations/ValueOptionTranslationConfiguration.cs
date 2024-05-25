using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations.Translations
{
    public class ValueOptionTranslationConfiguration : IEntityTypeConfiguration<ValueOptionTranslation>
    {
        public void Configure(EntityTypeBuilder<ValueOptionTranslation> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ValueOptionsTranslations, ScriptInterpreterConstants.SchemaName);

            builder.Property(s => s.Name)
                .HasMaxLength(255);
            builder.Property(s => s.Description)
                .HasMaxLength(1024);

            builder.HasKey(vot
                => new { vot.ValueOptionId, vot.Language });

            builder.Property(p => p.Language)
                .HasConversion(l => l.ToString(), lt => Enum.Parse<Language>(lt))
                .HasMaxLength(64);
        }
    }
}
