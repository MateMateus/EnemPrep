# 12 — Pós-MVP (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapa 11 concluída e MVP estabilizado.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Planejar e/ou iniciar evoluções avançadas somente depois que o núcleo do produto estiver consolidado.

## Escopo obrigatório
- Ranking mais robusto.
- Recomendações personalizadas.
- Simulados avançados.
- Planos premium, notificações e recursos de expansão, se fizerem parte da estratégia.
- Evoluções mobile ou inteligência adicional, se aprovadas.

## Fora de escopo / proibido nesta etapa
- Não trazer recursos pós-MVP para antes da consolidação do núcleo.
- Não comprometer a estabilidade do MVP em troca de features chamativas.
- Não expandir sem reavaliar impacto arquitetural.

## Regra de bloqueio desta etapa
Se o MVP ainda tiver dívida funcional importante, o agente deve interromper e recomendar correção antes de expansão.

## Regras técnicas específicas
- Cada evolução deve passar por análise de impacto.
- Não quebrar contratos existentes sem plano de migração.
- Crescimento deve ser incremental e controlado.

## Entregas esperadas
- Especificação e/ou implementação da evolução escolhida.
- Análise de impacto técnico e dependências.
- Plano de rollout compatível com o estado do projeto.

## Critérios de pronto
- As evoluções propostas têm base técnica real.
- O MVP permanece estável.
- Cada nova feature avançada é justificada por valor de produto.

## Validação obrigatória antes de aceitar a etapa
- Verificar se a evolução respeita a base existente.
- Verificar se a prioridade faz sentido para o momento do produto.
- Verificar se o custo de manutenção continua aceitável.

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
Nenhuma etapa obrigatória após esta; daqui em diante entra roadmap evolutivo.

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
12 — Pós-MVP

Dependência obrigatória para iniciar:
Etapa 11 concluída e MVP estabilizado.

Regra de bloqueio:
Se o MVP ainda tiver dívida funcional importante, o agente deve interromper e recomendar correção antes de expansão.

Objetivo da etapa:
planejar e/ou iniciar evoluções avançadas somente depois que o núcleo do produto estiver consolidado

Escopo obrigatório:
- Ranking mais robusto.
- Recomendações personalizadas.
- Simulados avançados.
- Planos premium, notificações e recursos de expansão, se fizerem parte da estratégia.
- Evoluções mobile ou inteligência adicional, se aprovadas.

Não faça nesta etapa:
- Não trazer recursos pós-MVP para antes da consolidação do núcleo.
- Não comprometer a estabilidade do MVP em troca de features chamativas.
- Não expandir sem reavaliar impacto arquitetural.

Regras técnicas específicas desta etapa:
- Cada evolução deve passar por análise de impacto.
- Não quebrar contratos existentes sem plano de migração.
- Crescimento deve ser incremental e controlado.

Entregas esperadas:
- Especificação e/ou implementação da evolução escolhida.
- Análise de impacto técnico e dependências.
- Plano de rollout compatível com o estado do projeto.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- As evoluções propostas têm base técnica real.
- O MVP permanece estável.
- Cada nova feature avançada é justificada por valor de produto.

Checklist de validação antes de encerrar:
- Verificar se a evolução respeita a base existente.
- Verificar se a prioridade faz sentido para o momento do produto.
- Verificar se o custo de manutenção continua aceitável.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
