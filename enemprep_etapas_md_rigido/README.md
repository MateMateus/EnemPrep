# EnemPrep — Etapas Rígidas para Antigravity

Este pacote contém arquivos `.md` separados por etapa, em uma versão **mais rígida e completa**, para instruir um agente do Antigravity especialista em C#.

## Como usar corretamente
1. Envie primeiro o arquivo `contexto-base-do-projeto-rigido.md`.
2. Depois envie **apenas o arquivo da etapa atual**.
3. Só avance quando a etapa anterior:
   - compilar;
   - estiver coerente com a arquitetura;
   - não tiver pendência estrutural bloqueante.
4. Se o agente tentar adiantar etapas, corrija e volte para o escopo do arquivo atual.

## Regra central
Cada etapa **depende da anterior**.
Os arquivos já deixam isso explícito para o agente, inclusive com:
- dependência obrigatória;
- regra de bloqueio;
- critérios de pronto;
- validação obrigatória;
- confirmação explícita de que a próxima etapa não foi adiantada.

## Ordem rígida das etapas
- 00 — Visão Geral e Arquitetura
- 01 — Modelagem do Domain
- 02 — Contratos da Application
- 03 — Infrastructure e Persistência
- 04 — API Base
- 05 — Admin MVP
- 06 — Web do Aluno: Estrutura Inicial
- 07 — Fluxo Real de Estudo com Questões
- 08 — Dashboard e Progresso
- 09 — Cronograma e Plano de Estudo
- 10 — Engajamento e Gamificação
- 11 — Refinamento de UX, Performance e Segurança
- 12 — Pós-MVP

## Arquivos incluídos
- `contexto-base-do-projeto-rigido.md`
- `00-visao-geral-e-arquitetura-rigido.md`
- `01-modelagem-do-domain-rigido.md`
- `02-contratos-da-application-rigido.md`
- `03-infrastructure-e-persistencia-rigido.md`
- `04-api-base-rigido.md`
- `05-admin-mvp-rigido.md`
- `06-web-do-aluno-estrutura-inicial-rigido.md`
- `07-fluxo-real-de-estudo-com-questoes-rigido.md`
- `08-dashboard-e-progresso-rigido.md`
- `09-cronograma-e-plano-de-estudo-rigido.md`
- `10-engajamento-e-gamificacao-rigido.md`
- `11-refinamento-ux-performance-e-seguranca-rigido.md`
- `12-pos-mvp-rigido.md`

## Estratégia recomendada
Use cada arquivo como um prompt-base.
Se quiser mais controle, envie:
1. o contexto base;
2. o arquivo da etapa;
3. a instrução adicional: “não avance para a próxima etapa sem minha autorização”.

## Observação
Esta versão foi escrita para **reduzir fuga de escopo, acoplamento indevido e adiantamento de etapas**.
