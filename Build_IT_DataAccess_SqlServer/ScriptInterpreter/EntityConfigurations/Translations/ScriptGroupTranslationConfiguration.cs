using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;
using System;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations.Translations
{
    public class ScriptGroupTranslationConfiguration : IEntityTypeConfiguration<ScriptGroupTranslation>
    {
        public void Configure(EntityTypeBuilder<ScriptGroupTranslation> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ScriptGroupsTranslations, ScriptInterpreterConstants.SchemaName);

            builder.Property(s => s.Name)
                .HasMaxLength(255);

            builder.HasKey(sgt
                => new { sgt.GroupId, sgt.Language });

            builder.Property(p => p.Language)
                .HasConversion(l => l.ToString(), lt => Enum.Parse<Language>(lt))
                .HasMaxLength(64);
        }
    }
}
