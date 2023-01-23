using JS.Clientes.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JS.Clientes.Infra.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Logradouro)
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Numero)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Cep)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Complemento)
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Bairro)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Cidade)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Estado)
                .HasColumnType("varchar(50)");

            builder.ToTable("Enderecos");
        }              
    }
}
