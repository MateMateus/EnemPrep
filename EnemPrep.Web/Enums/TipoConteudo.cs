using System.Text.Json.Serialization;

namespace EnemPrep.Web.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoConteudo
{
    Livro = 1,
    Slide = 2,
    Resumo = 3
}
