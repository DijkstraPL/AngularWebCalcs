using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.Parameters, ScriptInterpreterConstants.SchemaName);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Number)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(1024);
            builder.Property(p => p.Notes)
                .HasMaxLength(1024);
            builder.Property(p => p.AccordingTo)
                .HasMaxLength(256);
            builder.Property(p => p.DataValidator)
                .HasMaxLength(512);
            builder.Property(p => p.VisibilityValidator)
                .HasMaxLength(512);
            builder.Property(p => p.Value)
                .HasMaxLength(2048);

            builder.Property(p => p.ValueType)
                .IsRequired()
                .HasConversion(l => l.ToString(), lt => Enum.Parse<ValueTypes>(lt))
                .HasDefaultValue(ValueTypes.Number)
                .HasMaxLength(16);

            builder.Property(p => p.ValueOptionSetting)
                .HasConversion(p => p.ToString(), p => Enum.Parse<ValueOptionSettings>(p))
                .HasDefaultValue(ValueOptionSettings.None);

            builder.Property(p => p.ParameterOptions)
                .HasConversion(p => p.ToString(), p => Enum.Parse<ParameterOptions>(p))
                .HasDefaultValue(ParameterOptions.None);

            builder.HasMany<ValueOption>(p => p.ValueOptions)
                .WithOne(vo => vo.Parameter)
                .HasForeignKey(vo => vo.ParameterId);

            builder.HasMany<ParameterFigure>(p => p.ParameterFigures)
                .WithOne(pp => pp.Parameter)
                .HasForeignKey(pp => pp.ParameterId);

            builder.HasMany<ParameterUnit>(p => p.ParameterUnits)
                .WithOne(vo => vo.Parameter)
                .HasForeignKey(vo => vo.ParameterId);

        }
    }
}
