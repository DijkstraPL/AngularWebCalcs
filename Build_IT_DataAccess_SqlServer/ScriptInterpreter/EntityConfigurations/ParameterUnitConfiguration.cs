using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ParameterUnitConfiguration : IEntityTypeConfiguration<ParameterUnit>
    {
        public void Configure(EntityTypeBuilder<ParameterUnit> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ParameterUnits, ScriptInterpreterConstants.SchemaName);

            builder.HasKey(pf => new { pf.ParameterId, pf.UnitId });

            builder.Property(p => p.Power)
                .HasDefaultValue(1d);

        }
    }
}
