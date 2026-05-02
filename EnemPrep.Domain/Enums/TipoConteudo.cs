using System.Text.Json.Serialization;

namespace EnemPrep.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoConteudo
{
    Livro = 1,
    Slide = 2,
    Resumo = 3
}
