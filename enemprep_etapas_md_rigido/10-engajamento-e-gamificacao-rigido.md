# 10 — Engajamento e Gamificação (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapas 08 e 09 concluídas; progresso e metas precisam existir de verdade.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Aumentar retenção e frequência de uso com mecanismos de motivação ancorados em ações reais do usuário.

## Escopo obrigatório
- Implementar desafio diário.
- Consolidar streak.
- Implementar conquistas básicas.
- Adicionar reforços visuais/mensagens de progresso onde fizer sentido.

## Fora de escopo / proibido nesta etapa
- Não criar gamificação desligada de comportamento real.
- Não usar pontos, badges ou streak sem regra clara.
- Não mascarar ausência de progresso real com efeitos visuais.

## Regra de bloqueio desta etapa
Se as regras de streak, desafio e conquista não estiverem amarradas ao uso real, o sistema fica incoerente. O agente deve parar e sinalizar isso.

## Regras técnicas específicas
- Gamificação é consequência de ações do usuário, não decoração isolada.
- Regras devem ser simples, verificáveis e transparentes.
- Evitar exagero que complique o MVP sem retorno.

## Entregas esperadas
- Fluxos e componentes de desafio/streak/conquista.
- Ajustes de backend/API necessários para suportar a lógica, se previstos.
- Mensagens e estados visuais coerentes.

## Critérios de pronto
- A gamificação está conectada a tentativas, metas e progresso reais.
- O usuário percebe estímulos concretos para continuar estudando.
- A etapa melhora retenção sem comprometer clareza do produto.

## Validação obrigatória antes de aceitar a etapa
- Verificar que a regra de engajamento usa dados reais.
- Verificar que não há comportamento aleatório ou difícil de explicar.
- Verificar que a gamificação reforça, não atrapalha, o estudo.

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
11 — Refinamento de UX, performance e segurança

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
10 — Engajamento e Gamificação

Dependência obrigatória para iniciar:
Etapas 08 e 09 concluídas; progresso e metas precisam existir de verdade.

Regra de bloqueio:
Se as regras de streak, desafio e conquista não estiverem amarradas ao uso real, o sistema fica incoerente. O agente deve parar e sinalizar isso.

Objetivo da etapa:
aumentar retenção e frequência de uso com mecanismos de motivação ancorados em ações reais do usuário

Escopo obrigatório:
- Implementar desafio diário.
- Consolidar streak.
- Implementar conquistas básicas.
- Adicionar reforços visuais/mensagens de progresso onde fizer sentido.

Não faça nesta etapa:
- Não criar gamificação desligada de comportamento real.
- Não usar pontos, badges ou streak sem regra clara.
- Não mascarar ausência de progresso real com efeitos visuais.

Regras técnicas específicas desta etapa:
- Gamificação é consequência de ações do usuário, não decoração isolada.
- Regras devem ser simples, verificáveis e transparentes.
- Evitar exagero que complique o MVP sem retorno.

Entregas esperadas:
- Fluxos e componentes de desafio/streak/conquista.
- Ajustes de backend/API necessários para suportar a lógica, se previstos.
- Mensagens e estados visuais coerentes.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- A gamificação está conectada a tentativas, metas e progresso reais.
- O usuário percebe estímulos concretos para continuar estudando.
- A etapa melhora retenção sem comprometer clareza do produto.

Checklist de validação antes de encerrar:
- Verificar que a regra de engajamento usa dados reais.
- Verificar que não há comportamento aleatório ou difícil de explicar.
- Verificar que a gamificação reforça, não atrapalha, o estudo.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
