using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ParameterFigureConfiguration : IEntityTypeConfiguration<ParameterFigure>
    {
        public void Configure(EntityTypeBuilder<ParameterFigure> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ParameterFigures, ScriptInterpreterConstants.SchemaName);

            builder.HasKey(pf
                => new { pf.ParameterId, pf.FigureId });
        }
    }
}
