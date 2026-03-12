# 11 — Refinamento de UX, Performance e Segurança (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapas 00 a 10 concluídas; o MVP já precisa existir de ponta a ponta.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Polir a experiência, estabilizar a navegação e reduzir riscos técnicos sem refatorar desnecessariamente o que já funciona.

## Escopo obrigatório
- Melhorar responsividade e consistência visual.
- Revisar loading states, empty states e mensagens de erro.
- Aprimorar proteção de rotas e segurança básica.
- Melhorar performance em listagens e pontos evidentes de gargalo.
- Padronizar componentes e navegação.

## Fora de escopo / proibido nesta etapa
- Não reescrever a arquitetura inteira.
- Não inventar features novas de produto nesta etapa.
- Não fazer refatoração cosmética que gere regressão sem benefício real.

## Regra de bloqueio desta etapa
Se ainda houver buracos graves de fluxo básico, esta etapa não deve ser usada para escondê-los com maquiagem visual. O agente deve parar e apontar os fluxos quebrados.

## Regras técnicas específicas
- Priorizar correções que aumentem confiabilidade e usabilidade real.
- Toda melhoria deve preservar o funcionamento existente.
- Não usar refinamento como desculpa para alterar escopo do produto.

## Entregas esperadas
- Ajustes de UI/UX.
- Ajustes de performance e segurança básica.
- Padronizações necessárias de navegação e comportamento.

## Critérios de pronto
- O MVP fica mais estável, claro e utilizável.
- A UX melhora de forma perceptível.
- Os principais gargalos e riscos básicos de segurança são tratados.
- A solução fica pronta para demonstração, validação ou uso inicial.

## Validação obrigatória antes de aceitar a etapa
- Verificar regressões.
- Verificar consistência visual e técnica.
- Verificar proteção mínima das áreas sensíveis.

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
12 — Pós-MVP

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
11 — Refinamento de UX, Performance e Segurança

Dependência obrigatória para iniciar:
Etapas 00 a 10 concluídas; o MVP já precisa existir de ponta a ponta.

Regra de bloqueio:
Se ainda houver buracos graves de fluxo básico, esta etapa não deve ser usada para escondê-los com maquiagem visual. O agente deve parar e apontar os fluxos quebrados.

Objetivo da etapa:
polir a experiência, estabilizar a navegação e reduzir riscos técnicos sem refatorar desnecessariamente o que já funciona

Escopo obrigatório:
- Melhorar responsividade e consistência visual.
- Revisar loading states, empty states e mensagens de erro.
- Aprimorar proteção de rotas e segurança básica.
- Melhorar performance em listagens e pontos evidentes de gargalo.
- Padronizar componentes e navegação.

Não faça nesta etapa:
- Não reescrever a arquitetura inteira.
- Não inventar features novas de produto nesta etapa.
- Não fazer refatoração cosmética que gere regressão sem benefício real.

Regras técnicas específicas desta etapa:
- Priorizar correções que aumentem confiabilidade e usabilidade real.
- Toda melhoria deve preservar o funcionamento existente.
- Não usar refinamento como desculpa para alterar escopo do produto.

Entregas esperadas:
- Ajustes de UI/UX.
- Ajustes de performance e segurança básica.
- Padronizações necessárias de navegação e comportamento.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- O MVP fica mais estável, claro e utilizável.
- A UX melhora de forma perceptível.
- Os principais gargalos e riscos básicos de segurança são tratados.
- A solução fica pronta para demonstração, validação ou uso inicial.

Checklist de validação antes de encerrar:
- Verificar regressões.
- Verificar consistência visual e técnica.
- Verificar proteção mínima das áreas sensíveis.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
