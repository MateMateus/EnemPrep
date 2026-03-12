# 02 — Contratos da Application (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapa 01 concluída e o Domain precisa estar estável.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Definir os casos de uso e contratos da aplicação sem implementar persistência, ui ou detalhes de framework além do necessário.

## Escopo obrigatório
- Criar DTOs de entrada e saída por módulo do MVP.
- Criar interfaces de serviços/casos de uso para autenticação, perfil, matérias, questões, videoaulas, materiais, dashboard e plano de estudo inicial.
- Criar interfaces de repositório ou gateways necessários para a orquestração da aplicação.
- Definir objetos de resultado, paginação e respostas padronizadas, se fizer sentido.
- Adicionar validações de entrada no nível apropriado da Application.

## Fora de escopo / proibido nesta etapa
- Não implementar EF Core, banco, repositórios concretos ou controllers.
- Não colocar regra de persistência dentro da Application.
- Não criar telas ou chamadas HTTP reais aqui.
- Não duplicar entidades do Domain fingindo que são DTOs sem propósito.

## Regra de bloqueio desta etapa
Se os contratos estiverem vagos ou contraditórios, a Infrastructure e a API serão implementadas de forma errada. O agente deve parar e apontar as lacunas.

## Regras técnicas específicas
- Manter a Application orientada a casos de uso, não a tabelas.
- Interfaces devem representar necessidades reais do produto, não abstrações genéricas vazias.
- Evitar sobreengenharia desnecessária, mas sem sacrificar a clareza das dependências.

## Entregas esperadas
- DTOs por módulo.
- Interfaces de serviços/casos de uso.
- Interfaces de repositórios/gateways.
- Modelos auxiliares de resultado, paginação e validação, se aplicável.

## Critérios de pronto
- A Application compila contra o Domain.
- Os contratos cobrem os fluxos necessários das próximas etapas imediatas.
- Há clareza suficiente para a Infrastructure implementar persistência e para a API expor endpoints depois.
- Os DTOs não vazam entidades de domínio para a borda sem necessidade.

## Validação obrigatória antes de aceitar a etapa
- Verificar se a Application depende apenas do Domain e bibliotecas necessárias.
- Verificar se os contratos da etapa seguinte podem ser implementados sem improviso.
- Verificar se não há detalhes de infraestrutura infiltrados.

## Formato obrigatório da resposta do agente
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

## Perguntas que o agente deve responder no fim
- O que foi implementado?
- O que não foi implementado?
- Existe algo que bloqueia a próxima etapa?
- Qual é a próxima etapa permitida do roadmap?
- Você confirma explicitamente que **não** adiantou trabalho da próxima etapa?

## Próxima etapa permitida
03 — Infrastructure e persistência

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
02 — Contratos da Application

Dependência obrigatória para iniciar:
Etapa 01 concluída e o Domain precisa estar estável.

Regra de bloqueio:
Se os contratos estiverem vagos ou contraditórios, a Infrastructure e a API serão implementadas de forma errada. O agente deve parar e apontar as lacunas.

Objetivo da etapa:
definir os casos de uso e contratos da aplicação sem implementar persistência, UI ou detalhes de framework além do necessário

Escopo obrigatório:
- Criar DTOs de entrada e saída por módulo do MVP.
- Criar interfaces de serviços/casos de uso para autenticação, perfil, matérias, questões, videoaulas, materiais, dashboard e plano de estudo inicial.
- Criar interfaces de repositório ou gateways necessários para a orquestração da aplicação.
- Definir objetos de resultado, paginação e respostas padronizadas, se fizer sentido.
- Adicionar validações de entrada no nível apropriado da Application.

Não faça nesta etapa:
- Não implementar EF Core, banco, repositórios concretos ou controllers.
- Não colocar regra de persistência dentro da Application.
- Não criar telas ou chamadas HTTP reais aqui.
- Não duplicar entidades do Domain fingindo que são DTOs sem propósito.

Regras técnicas específicas desta etapa:
- Manter a Application orientada a casos de uso, não a tabelas.
- Interfaces devem representar necessidades reais do produto, não abstrações genéricas vazias.
- Evitar sobreengenharia desnecessária, mas sem sacrificar a clareza das dependências.

Entregas esperadas:
- DTOs por módulo.
- Interfaces de serviços/casos de uso.
- Interfaces de repositórios/gateways.
- Modelos auxiliares de resultado, paginação e validação, se aplicável.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- A Application compila contra o Domain.
- Os contratos cobrem os fluxos necessários das próximas etapas imediatas.
- Há clareza suficiente para a Infrastructure implementar persistência e para a API expor endpoints depois.
- Os DTOs não vazam entidades de domínio para a borda sem necessidade.

Checklist de validação antes de encerrar:
- Verificar se a Application depende apenas do Domain e bibliotecas necessárias.
- Verificar se os contratos da etapa seguinte podem ser implementados sem improviso.
- Verificar se não há detalhes de infraestrutura infiltrados.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
