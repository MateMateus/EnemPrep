# 09 — Cronograma e Plano de Estudo (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapa 08 concluída e com dados de progresso utilizáveis.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Introduzir organização de rotina no produto por meio de metas e plano semanal simples, sem cair em complexidade excessiva.

## Escopo obrigatório
- Criar plano de estudo simples por semana/dia.
- Permitir metas semanais e/ou diárias coerentes com o MVP.
- Exibir uma visão de cronograma ou calendário simples.
- Relacionar plano/metas com conteúdos ou áreas do sistema quando isso já estiver preparado.

## Fora de escopo / proibido nesta etapa
- Não começar por calendário ultra complexo.
- Não transformar a etapa em sistema completo de agenda.
- Não criar lógica desconectada do restante da plataforma.

## Regra de bloqueio desta etapa
Se plano e metas não estiverem claros, a gamificação posterior fica arbitrária. O agente deve parar e resolver a estrutura antes de avançar.

## Regras técnicas específicas
- Manter simplicidade funcional no MVP.
- Metas e cronograma devem conversar com dados reais do usuário sempre que possível.
- Não criar mecanismos de planejamento impossíveis de manter.

## Entregas esperadas
- Telas/componentes de plano e cronograma.
- Integração com API.
- Fluxo mínimo de criação/edição/visualização, conforme o escopo do projeto.

## Critérios de pronto
- O aluno consegue visualizar e manter um plano de estudo simples.
- As metas ficam claras na experiência.
- A função de organização complementa os módulos de conteúdo e prática.
- A etapa prepara insumos para gamificação e retenção.

## Validação obrigatória antes de aceitar a etapa
- Verificar se a rotina de estudo ficou utilizável e não só decorativa.
- Verificar se há coerência entre metas, dashboard e prática.
- Verificar se a etapa não inchou o MVP além do necessário.

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
10 — Engajamento e gamificação

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
09 — Cronograma e Plano de Estudo

Dependência obrigatória para iniciar:
Etapa 08 concluída e com dados de progresso utilizáveis.

Regra de bloqueio:
Se plano e metas não estiverem claros, a gamificação posterior fica arbitrária. O agente deve parar e resolver a estrutura antes de avançar.

Objetivo da etapa:
introduzir organização de rotina no produto por meio de metas e plano semanal simples, sem cair em complexidade excessiva

Escopo obrigatório:
- Criar plano de estudo simples por semana/dia.
- Permitir metas semanais e/ou diárias coerentes com o MVP.
- Exibir uma visão de cronograma ou calendário simples.
- Relacionar plano/metas com conteúdos ou áreas do sistema quando isso já estiver preparado.

Não faça nesta etapa:
- Não começar por calendário ultra complexo.
- Não transformar a etapa em sistema completo de agenda.
- Não criar lógica desconectada do restante da plataforma.

Regras técnicas específicas desta etapa:
- Manter simplicidade funcional no MVP.
- Metas e cronograma devem conversar com dados reais do usuário sempre que possível.
- Não criar mecanismos de planejamento impossíveis de manter.

Entregas esperadas:
- Telas/componentes de plano e cronograma.
- Integração com API.
- Fluxo mínimo de criação/edição/visualização, conforme o escopo do projeto.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- O aluno consegue visualizar e manter um plano de estudo simples.
- As metas ficam claras na experiência.
- A função de organização complementa os módulos de conteúdo e prática.
- A etapa prepara insumos para gamificação e retenção.

Checklist de validação antes de encerrar:
- Verificar se a rotina de estudo ficou utilizável e não só decorativa.
- Verificar se há coerência entre metas, dashboard e prática.
- Verificar se a etapa não inchou o MVP além do necessário.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
