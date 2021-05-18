using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Normas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Normas.Api.Data.Configuration
{
    public class TipoRequisitoConfiguration
    {
        public TipoRequisitoConfiguration(EntityTypeBuilder<TipoRequisito>entityBuilder)
        {
            entityBuilder.HasKey(x => x.TipoRequisitoId);
            entityBuilder.Property(x => x.Descripcion).IsRequired().HasMaxLength(50);
            string[] listado = new[]
            {
                "Norma Oficial Mexicana (NOM)", "Especificacion CFE", "Reglamento Federal", "Reglamento Estatal",
                "Reglamento Municipal", "Estandar Internacional", "Norma de Referencia", "Norma Mexicana"
            };

            var requisitos = new List<TipoRequisito>();
            for (int nx=0; nx<listado.Length; nx++)
            {
                requisitos.Add(new TipoRequisito
                {
                    TipoRequisitoId = nx + 1,
                    Descripcion = listado[nx]
                });
            }
            entityBuilder.HasData(requisitos);

        }
    }
}
