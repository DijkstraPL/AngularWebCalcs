using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ScriptTagConfiguration : IEntityTypeConfiguration<ScriptTag>
    {
        public void Configure(EntityTypeBuilder<ScriptTag> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ScriptTags, ScriptInterpreterConstants.SchemaName);

            builder.HasKey(st
                => new { st.ScriptId, st.TagId });
        }
    }
}
