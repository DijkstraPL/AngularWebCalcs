using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;
using System;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations.Translations
{
    public class UnitGroupTranslationConfiguration : IEntityTypeConfiguration<UnitGroupTranslation>
    {
        public void Configure(EntityTypeBuilder<UnitGroupTranslation> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.UnitGroupTranslations, ScriptInterpreterConstants.SchemaName);

            builder.Property(s => s.Name)
                .HasMaxLength(255);

            builder.HasKey(ut
                => new { ut.UnitGroupId, ut.Language });

            builder.Property(p => p.Language)
                .HasConversion(l => l.ToString(), lt => Enum.Parse<Language>(lt))
                .HasMaxLength(64);
        }
    }
}
