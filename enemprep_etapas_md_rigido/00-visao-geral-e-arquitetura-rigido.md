# 00 — Visão Geral e Arquitetura (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Nenhuma. Esta é a etapa fundadora.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Congelar a estrutura da solução, referências entre projetos, convenções e limites arquiteturais antes de qualquer implementação funcional.

## Escopo obrigatório
- Criar a solution e os projetos separados: Domain, Application, Infrastructure, API e Web.
- Definir referências corretas entre projetos, sem dependências indevidas.
- Estabelecer estrutura de pastas, namespaces, convenções de nomes e responsabilidade de cada camada.
- Preparar o esqueleto mínimo para compilação da solução.
- Documentar a direção arquitetural em arquivos de apoio, se necessário.

## Fora de escopo / proibido nesta etapa
- Não implementar entidades de negócio completas.
- Não criar DTOs, repositórios, DbContext, controllers finais ou telas finais.
- Não criar lógica de negócio real.
- Não improvisar uma arquitetura diferente da definida no PRD.

## Regra de bloqueio desta etapa
Se a solução não estiver organizada e compilando, nenhuma etapa posterior é autorizada.

## Regras técnicas específicas
- A Web deve existir como projeto separado e já ficar marcada como consumidora da API, não das outras camadas.
- A API pode referenciar Application e Infrastructure conforme a estratégia adotada, mas o domínio de responsabilidades deve ficar claro.
- Não mascarar ausência de arquitetura com comentários vagos; a estrutura precisa aparecer no código e nas referências.

## Entregas esperadas
- Arquivo de solution e projetos.
- Estrutura de pastas inicial.
- Arquivos mínimos de bootstrap/Program se necessários para compilar.
- Documento curto de convenções, se útil.

## Critérios de pronto
- A solution compila com os projetos criados.
- As referências entre projetos estão corretas.
- A arquitetura está explícita e pronta para sustentar as próximas etapas.
- Não existem dependências proibidas.

## Validação obrigatória antes de aceitar a etapa
- Verificar dependências de projeto uma a uma.
- Verificar se nenhum projeto referencia indevidamente a Web ou Infrastructure para cima da pilha.
- Verificar se a nomenclatura já prepara a evolução do sistema.

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
01 — Modelagem do Domain

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
00 — Visão Geral e Arquitetura

Dependência obrigatória para iniciar:
Nenhuma. Esta é a etapa fundadora.

Regra de bloqueio:
Se a solução não estiver organizada e compilando, nenhuma etapa posterior é autorizada.

Objetivo da etapa:
congelar a estrutura da solução, referências entre projetos, convenções e limites arquiteturais antes de qualquer implementação funcional

Escopo obrigatório:
- Criar a solution e os projetos separados: Domain, Application, Infrastructure, API e Web.
- Definir referências corretas entre projetos, sem dependências indevidas.
- Estabelecer estrutura de pastas, namespaces, convenções de nomes e responsabilidade de cada camada.
- Preparar o esqueleto mínimo para compilação da solução.
- Documentar a direção arquitetural em arquivos de apoio, se necessário.

Não faça nesta etapa:
- Não implementar entidades de negócio completas.
- Não criar DTOs, repositórios, DbContext, controllers finais ou telas finais.
- Não criar lógica de negócio real.
- Não improvisar uma arquitetura diferente da definida no PRD.

Regras técnicas específicas desta etapa:
- A Web deve existir como projeto separado e já ficar marcada como consumidora da API, não das outras camadas.
- A API pode referenciar Application e Infrastructure conforme a estratégia adotada, mas o domínio de responsabilidades deve ficar claro.
- Não mascarar ausência de arquitetura com comentários vagos; a estrutura precisa aparecer no código e nas referências.

Entregas esperadas:
- Arquivo de solution e projetos.
- Estrutura de pastas inicial.
- Arquivos mínimos de bootstrap/Program se necessários para compilar.
- Documento curto de convenções, se útil.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- A solution compila com os projetos criados.
- As referências entre projetos estão corretas.
- A arquitetura está explícita e pronta para sustentar as próximas etapas.
- Não existem dependências proibidas.

Checklist de validação antes de encerrar:
- Verificar dependências de projeto uma a uma.
- Verificar se nenhum projeto referencia indevidamente a Web ou Infrastructure para cima da pilha.
- Verificar se a nomenclatura já prepara a evolução do sistema.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
